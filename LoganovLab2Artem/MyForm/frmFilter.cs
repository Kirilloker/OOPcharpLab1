using LoganovLab2Artem.Controller;
using LoganovLab2Artem.Expressions;
using LoganovLab2Artem.FieldSpace;
using LoganovLab2Artem.FirmSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.MyForm
{
    public partial class frmFilter : Form
    {
        private FilterContr _filterContr;
        private FirmVw _firmVw;
        private Button btnApply;
        private Button btnCancel;

        // Для примера создадим простую логику:
        // На форме будет список полей и для каждого поля - текстбокс для значения, комбо бокс для операции
        private List<Field> _fields;
        private List<ComboBox> _combos;
        private List<TextBox> _texts;

        public frmFilter(FilterContr filterContr, FirmVw firmVw)
        {
            _filterContr = filterContr;
            _firmVw = firmVw;

            _fields = _firmVw.GetFields().ToList();
            _combos = new List<ComboBox>();
            _texts = new List<TextBox>();

            this.Text = "Настройка фильтра";
            this.Size = new Size(600, 400);

            var panel = new Panel { Dock = DockStyle.Fill, AutoScroll = true };
            this.Controls.Add(panel);

            int y = 10;
            foreach (var field in _fields)
            {
                var lbl = new Label { Text = field.GetType().Name, Location = new Point(10, y), AutoSize = true };
                panel.Controls.Add(lbl);

                var combo = new ComboBox { Location = new Point(150, y), Width = 100, DropDownStyle = ComboBoxStyle.DropDownList };
                combo.Items.AddRange(new string[] { ">", "<", "=", "!=", ">=", "<=", "содержит", "не содежит" }); // Символы вместо Enum значений
                panel.Controls.Add(combo);
                _combos.Add(combo);

                var txt = new TextBox { Location = new Point(260, y), Width = 100 };
                panel.Controls.Add(txt);
                _texts.Add(txt);

                y += 30;
            }

            btnApply = new Button { Text = "Применить", Location = new Point(10, y) };
            btnApply.Click += BtnApply_Click;
            panel.Controls.Add(btnApply);

            btnCancel = new Button { Text = "Отмена", Location = new Point(100, y) };
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            panel.Controls.Add(btnCancel);
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            // Собираем правила
            for (int i = 0; i < _fields.Count; i++)
            {
                var opStr = _combos[i].SelectedItem as string;
                var valStr = _texts[i].Text;
                if (!string.IsNullOrEmpty(opStr))
                {
                    LogExpEnum op = opStr switch
                    {
                        ">" => LogExpEnum.GT,
                        "<" => LogExpEnum.LT,
                        "=" => LogExpEnum.EQ,
                        "!=" => LogExpEnum.NoEQ,
                        ">=" => LogExpEnum.GE,
                        "<=" => LogExpEnum.LE,
                        "содержит" => LogExpEnum.Contains,
                        "не содержит" => LogExpEnum.NoContains,
                        _ => throw new InvalidOperationException("Неизвестный оператор")
                    };

                    var rule = _fields[i].CreateRule();
                    var baseExp = LogExpFactory.CreateExp(op, valStr);
                    var fullExp = rule.CreateFieldExpr(baseExp);
                    rule.SetExpression(fullExp);
                    _filterContr.AddRule(rule);
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

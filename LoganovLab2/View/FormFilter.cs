using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LoganovLab2.Filtering;
using LoganovLab2.Firms;
using LoganovLab2.Rules;

namespace LoganovLab2.View
{
    public class FormFilter : Form
    {
        private FirmView _firmVw;
        private Button _btnApply;
        private Button _btnCancel;
        private TableLayoutPanel _tableLayoutPanel;
        private Panel _panelFields;
        private List<Field> _fields;
        private ToolStrip _toolStrip;
        private Dictionary<Field, (CheckBox chk, TextBox txtValue, ComboBox cbCondition)> _fieldControls
            = new Dictionary<Field, (CheckBox, TextBox, ComboBox)>();

        public FormFilter(FirmView firmVw)
        {
            _firmVw = firmVw;
            _fields = _firmVw.GetFields().ToList();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            // Настройки формы
            this.Text = "Настройка фильтра";
            this.Width = 700;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            // ToolStrip для кнопок
            _toolStrip = new ToolStrip
            {
                Dock = DockStyle.Top,
                GripStyle = ToolStripGripStyle.Hidden,
                BackColor = SystemColors.ControlLight
            };

            var btnApply = new ToolStripButton("Применить", null, BtnApply_Click)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Black
            };

            var btnCancel = new ToolStripButton("Отмена", null, BtnCancel_Click)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Black
            };

            _toolStrip.Items.AddRange(new ToolStripItem[] { btnApply, btnCancel });
            this.Controls.Add(_toolStrip);

            // Panel для полей с прокруткой
            _panelFields = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10),
                BackColor = Color.White
            };

            // TableLayoutPanel для фильтров
            _tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 3,
                AutoSize = true,
                Padding = new Padding(5)
            };
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));

            // Заполнение TableLayoutPanel
            int row = 0;
            foreach (var field in _fields)
            {
                var chk = new CheckBox
                {
                    Text = field.FieldName,
                    Font = new Font("Segoe UI", 10),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5)
                };

                var txt = new TextBox
                {
                    Font = new Font("Segoe UI", 10),
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5)
                };

                var cb = new ComboBox
                {
                    Font = new Font("Segoe UI", 10),
                    Dock = DockStyle.Fill,
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Margin = new Padding(5)
                };

                cb.Items.AddRange(new string[]
                {
                    "Равно",
                    "Не равно",
                    "Содержит",
                    "Не содержит",
                    "Больше",
                    "Меньше",
                    "Больше или равно",
                    "Меньше или равно"
                });
                cb.SelectedIndex = 0;

                _tableLayoutPanel.Controls.Add(chk, 0, row);
                _tableLayoutPanel.Controls.Add(txt, 1, row);
                _tableLayoutPanel.Controls.Add(cb, 2, row);

                _fieldControls[field] = (chk, txt, cb);
                row++;
            }

            _panelFields.Controls.Add(_tableLayoutPanel);
            this.Controls.Add(_panelFields);
        }

        private void BtnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private static readonly Dictionary<string, LogExpEnum> TextToEnumMap = new Dictionary<string, LogExpEnum>
        {
            { "Равно", LogExpEnum.EQ },
            { "Не равно", LogExpEnum.NoEQ },
            { "Содержит", LogExpEnum.Contains },
            { "Не содержит", LogExpEnum.NoContains },
            { "Больше", LogExpEnum.GT },
            { "Меньше", LogExpEnum.LT },
            { "Больше или равно", LogExpEnum.GE },
            { "Меньше или равно", LogExpEnum.LE }
        };

        public List<FilterRule> GetSelectedRules()
        {
            var result = new List<FilterRule>();

            foreach (var kvp in _fieldControls)
            {
                var field = kvp.Key;
                var (chk, txtValue, cbCondition) = kvp.Value;
                if (chk.Checked)
                {
                    var rule = field.CreateRule();
                    var expName = cbCondition.SelectedItem.ToString();

                    if (TextToEnumMap.TryGetValue(expName, out var expType))
                    {
                        var exp = LogExpFactory.Create(expType);
                        rule.SetExpression(exp);

                        object condVal = txtValue.Text;
                        switch (field.GetFieldDataType())
                        {
                            case FieldDataType.Int:
                                if (int.TryParse(txtValue.Text, out int iv)) condVal = iv;
                                break;
                            case FieldDataType.DateTime:
                                if (DateTime.TryParse(txtValue.Text, out DateTime dv)) condVal = dv;
                                break;
                        }

                        rule.SetConditionValue(condVal);
                        result.Add(rule);
                    }
                }
            }

            return result;
        }
    }
}

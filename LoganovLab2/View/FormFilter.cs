using LoganovLab2.Filtering;
using LoganovLab2.Firms;
using LoganovLab2.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2.View
{
    public class FormFilter : Form
    {
        private FirmView _firmVw;
        private Button btnApply;
        private Button btnCancel;
        private Panel panelFields;
        private List<Field> _fields;
        // Словари для хранения настроек фильтра по каждому полю
        // Выберем для примера: CheckBox для "включить фильтр", TextBox для значения, ComboBox для типа условия
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
            Text = "Настройка фильтра";
            Width = 600;
            Height = 400;

            panelFields = new Panel();
            panelFields.Dock = DockStyle.Top;
            panelFields.AutoScroll = true;
            panelFields.Height = 300;
            Controls.Add(panelFields);

            btnApply = new Button();
            btnApply.Text = "Применить";
            btnApply.Dock = DockStyle.Bottom;
            btnApply.Click += BtnApply_Click;
            Controls.Add(btnApply);

            btnCancel = new Button();
            btnCancel.Text = "Отмена";
            btnCancel.Dock = DockStyle.Bottom;
            btnCancel.Click += BtnCancel_Click;
            Controls.Add(btnCancel);

            int y = 10;
            foreach (var field in _fields)
            {
                var chk = new CheckBox();
                chk.Text = field.FieldName;
                chk.Left = 10;
                chk.Top = y;

                var txt = new TextBox();
                txt.Left = 150;
                txt.Top = y;

                var cb = new ComboBox();
                cb.Left = 300;
                cb.Top = y;
                cb.Width = 100;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                // Наполним ComboBox значениями из LogExpEnum
                cb.Items.AddRange(Enum.GetNames(typeof(LogExpEnum)));
                cb.SelectedIndex = 0;

                panelFields.Controls.Add(chk);
                panelFields.Controls.Add(txt);
                panelFields.Controls.Add(cb);

                _fieldControls[field] = (chk, txt, cb);

                y += 30;
            }
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
                    if (Enum.TryParse<LogExpEnum>(expName, out var expType))
                    {
                        var exp = LogExpFactory.Create(expType);
                        rule.SetExpression(exp);

                        // В зависимости от типа поля преобразуем входящее значение
                        object condVal = txtValue.Text;
                        // Для дат и чисел можно сделать попытку преобразования
                        switch (field.GetFieldDataType())
                        {
                            case FieldDataType.Int:
                                if (int.TryParse(txtValue.Text, out int iv)) condVal = iv;
                                else condVal = null;
                                break;
                            case FieldDataType.DateTime:
                                if (DateTime.TryParse(txtValue.Text, out DateTime dv)) condVal = dv;
                                else condVal = null;
                                break;
                            case FieldDataType.String:
                            default:
                                // Уже строка
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

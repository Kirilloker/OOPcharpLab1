using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LoganovLab2Artem.FieldSpace;
using System.ComponentModel;

namespace LoganovLab2Artem.MyForm
{
    public class FormSelectFields : Form
    {
        private CheckedListBox _clbFields; 
        private Button _btnOk;
        private Button _btnCancel;
        private Button _btnSelectAll;
        private Button _btnDeselectAll;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<Field> SelectedFields { get; private set; }

        public FormSelectFields(IEnumerable<Field> allFields, IEnumerable<Field> currentFields)
        {
            InitializeComponent(allFields, currentFields);
        }

        private void InitializeComponent(IEnumerable<Field> allFields, IEnumerable<Field> currentFields)
        {
            this.Text = "Выбор полей для отображения";
            this.Width = 350;
            this.Height = 500;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            _clbFields = new CheckedListBox
            {
                Dock = DockStyle.Top,
                Height = 350,
                CheckOnClick = true
            };

            foreach (var field in allFields)
            {
                bool isChecked = currentFields.Any(cf => cf.GetType() == field.GetType());
                _clbFields.Items.Add(field, isChecked);
            }

            var panelButtons = new Panel { Dock = DockStyle.Bottom, Height = 50 };

            _btnOk = new Button
            {
                Text = "OK",
                Dock = DockStyle.Right,
                Width = 100
            };
            _btnOk.Click += BtnOk_Click;

            _btnCancel = new Button
            {
                Text = "Отмена",
                Dock = DockStyle.Left,
                Width = 100
            };
            _btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            _btnSelectAll = new Button
            {
                Text = "Выбрать все",
                Dock = DockStyle.Left,
                Width = 100
            };
            _btnSelectAll.Click += BtnSelectAll_Click;

            _btnDeselectAll = new Button
            {
                Text = "Снять выделение",
                Dock = DockStyle.Left,
                Width = 120
            };
            _btnDeselectAll.Click += BtnDeselectAll_Click;

            panelButtons.Controls.Add(_btnOk);
            panelButtons.Controls.Add(_btnCancel);
            panelButtons.Controls.Add(_btnSelectAll);
            panelButtons.Controls.Add(_btnDeselectAll);

            this.Controls.Add(_clbFields);
            this.Controls.Add(panelButtons);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            SelectedFields = _clbFields.CheckedItems.Cast<Field>().ToList();
            this.DialogResult = DialogResult.OK;
        }

        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _clbFields.Items.Count; i++)
            {
                _clbFields.SetItemChecked(i, true);
            }
        }

        private void BtnDeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < _clbFields.Items.Count; i++)
            {
                _clbFields.SetItemChecked(i, false);
            }
        }
    }
}

using System;
using System.Windows.Forms;
using LoganovLab1Artem.ContactSpace; 
using System.ComponentModel;

namespace LoganovLab2Artem.MyForm
{
    public class FormAddContact : Form
    {
        private ComboBox _cmbContactType;
        private DateTimePicker _dtpBeginDate;
        private DateTimePicker _dtpEndDate;
        private TextBox _txtDescription;
        private TextBox _txtDataInfo;
        private Button _btnSave;
        private Button _btnCancel;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Contact NewContact { get; private set; }

        public FormAddContact(ContType[] availableTypes)
        {
            InitializeComponent(availableTypes);
        }

        private void InitializeComponent(ContType[] availableTypes)
        {
            this.Text = "Добавить контакт";
            this.Width = 450;
            this.Height = 400;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblType = new Label
            {
                Text = "Тип контакта:",
                Dock = DockStyle.Top,
                Height = 20,
                Padding = new Padding(5)
            };
            _cmbContactType = new ComboBox
            {
                Dock = DockStyle.Top,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            _cmbContactType.Items.AddRange(availableTypes);

            var lblBeginDate = new Label
            {
                Text = "Дата начала:",
                Dock = DockStyle.Top,
                Height = 20,
                Padding = new Padding(5)
            };
            _dtpBeginDate = new DateTimePicker
            {
                Dock = DockStyle.Top,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm"
            };

            var lblEndDate = new Label
            {
                Text = "Дата окончания:",
                Dock = DockStyle.Top,
                Height = 20,
                Padding = new Padding(5)
            };
            _dtpEndDate = new DateTimePicker
            {
                Dock = DockStyle.Top,
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm"
            };

            var lblDescription = new Label
            {
                Text = "Описание:",
                Dock = DockStyle.Top,
                Height = 20,
                Padding = new Padding(5)
            };
            _txtDescription = new TextBox
            {
                Dock = DockStyle.Top,
                Multiline = true,
                Height = 60
            };

            var lblDataInfo = new Label
            {
                Text = "Информационные данные:",
                Dock = DockStyle.Top,
                Height = 20,
                Padding = new Padding(5)
            };
            _txtDataInfo = new TextBox
            {
                Dock = DockStyle.Top,
                Multiline = true,
                Height = 40
            };

            var panelButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };
            _btnSave = new Button
            {
                Text = "Сохранить",
                Dock = DockStyle.Right,
                Width = 100
            };
            _btnSave.Click += BtnSave_Click;

            _btnCancel = new Button
            {
                Text = "Отмена",
                Dock = DockStyle.Left,
                Width = 100
            };
            _btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            panelButtons.Controls.Add(_btnSave);
            panelButtons.Controls.Add(_btnCancel);

            this.Controls.Add(_txtDataInfo);
            this.Controls.Add(lblDataInfo);
            this.Controls.Add(_txtDescription);
            this.Controls.Add(lblDescription);
            this.Controls.Add(_dtpEndDate);
            this.Controls.Add(lblEndDate);
            this.Controls.Add(_dtpBeginDate);
            this.Controls.Add(lblBeginDate);
            this.Controls.Add(_cmbContactType);
            this.Controls.Add(lblType);
            this.Controls.Add(panelButtons);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_cmbContactType.SelectedItem is ContType selectedType)
            {
                if (_dtpEndDate.Value < _dtpBeginDate.Value)
                {
                    MessageBox.Show("Дата окончания не может быть раньше даты начала.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrWhiteSpace(_txtDescription.Text))
                {
                    MessageBox.Show("Поле 'Описание' не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                NewContact = new Contact
                {
                    CntType = selectedType,
                    BeginDt = _dtpBeginDate.Value,
                    EndDt = _dtpEndDate.Value,
                    Descr = _txtDescription.Text,
                    DataInfo = _txtDataInfo.Text
                };

                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Выберите тип контакта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

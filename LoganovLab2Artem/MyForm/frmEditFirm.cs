using System;
using System.Windows.Forms;
using LoganovLab1Artem.FirmSpace; 

namespace LoganovLab2Artem.MyForm
{
    public class FormEditFirm : Form
    {
        private Firm _firm;
        private TextBox _txtFullName;
        private TextBox _txtShortName;
        private TextBox _txtRegion;
        private TextBox _txtCity;
        private TextBox _txtCountry;
        private TextBox _txtStreet;
        private TextBox _txtEmail;
        private TextBox _txtWeb;
        private TextBox _txtPostIndex;
        private Button _btnSave;
        private Button _btnCancel;

        public FormEditFirm(Firm firm)
        {
            _firm = firm;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Добавить фирму";
            this.Width = 500;
            this.Height = 600;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblFullName = new Label { Text = "Полное имя:", Dock = DockStyle.Top, Height = 20, Padding = new Padding(5) };
            _txtFullName = new TextBox { Text = _firm.Name, Dock = DockStyle.Top };

            var lblShortName = new Label { Text = "Краткое имя:", Dock = DockStyle.Top, Height = 20, Padding = new Padding(5) };
            _txtShortName = new TextBox { Text = _firm.Name, Dock = DockStyle.Top };

            var lblCountry = new Label { Text = "Страна:", Dock = DockStyle.Top, Height = 20, Padding = new Padding(5) };
            _txtCountry = new TextBox { Text = _firm.Country, Dock = DockStyle.Top };

            var lblRegion = new Label { Text = "Регион:", Dock = DockStyle.Top, Height = 20, Padding = new Padding(5) };
            _txtRegion = new TextBox { Text = _firm.Region, Dock = DockStyle.Top };

            var lblCity = new Label { Text = "Город:", Dock = DockStyle.Top, Height = 20, Padding = new Padding(5) };
            _txtCity = new TextBox { Text = _firm.Town, Dock = DockStyle.Top };

            var lblStreet = new Label { Text = "Улица:", Dock = DockStyle.Top, Height = 20, Padding = new Padding(5) };
            _txtStreet = new TextBox { Text = _firm.Street, Dock = DockStyle.Top };

            var lblPostIndex = new Label { Text = "Почтовый индекс:", Dock = DockStyle.Top, Height = 20, Padding = new Padding(5) };
            _txtPostIndex = new TextBox { Text = _firm.PostInx, Dock = DockStyle.Top };

            var lblEmail = new Label { Text = "Email:", Dock = DockStyle.Top, Height = 20, Padding = new Padding(5) };
            _txtEmail = new TextBox { Text = _firm.Email, Dock = DockStyle.Top };

            var lblWeb = new Label { Text = "Веб-сайт:", Dock = DockStyle.Top, Height = 20, Padding = new Padding(5) };
            _txtWeb = new TextBox { Text = _firm.Web, Dock = DockStyle.Top };

            var panelButtons = new Panel { Dock = DockStyle.Bottom, Height = 50 };

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

            this.Controls.Add(_txtWeb);
            this.Controls.Add(lblWeb);
            this.Controls.Add(_txtEmail);
            this.Controls.Add(lblEmail);
            this.Controls.Add(_txtPostIndex);
            this.Controls.Add(lblPostIndex);
            this.Controls.Add(_txtStreet);
            this.Controls.Add(lblStreet);
            this.Controls.Add(_txtCity);
            this.Controls.Add(lblCity);
            this.Controls.Add(_txtRegion);
            this.Controls.Add(lblRegion);
            this.Controls.Add(_txtCountry);
            this.Controls.Add(lblCountry);
            this.Controls.Add(_txtShortName);
            this.Controls.Add(lblShortName);
            this.Controls.Add(_txtFullName);
            this.Controls.Add(lblFullName);
            this.Controls.Add(panelButtons);
        }
        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txtFullName.Text) || string.IsNullOrWhiteSpace(_txtShortName.Text))
            {
                MessageBox.Show("Поле 'Полное имя' и 'Краткое имя' не могут быть пустыми.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _firm.Name = _txtFullName.Text;
            _firm.Name = _txtShortName.Text;
            _firm.Country = _txtCountry.Text;
            _firm.Region = _txtRegion.Text;
            _firm.Town = _txtCity.Text;
            _firm.Street = _txtStreet.Text;
            _firm.PostInx = _txtPostIndex.Text;
            _firm.Email = _txtEmail.Text;
            _firm.Web = _txtWeb.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

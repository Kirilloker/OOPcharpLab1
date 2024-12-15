using System;
using System.Windows.Forms;
using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.FirmSpace;
using LoganovLab1Artem.SubFirmSpace;
using LoganovLab2Artem.FirmSpace;

namespace LoganovLab2Artem.MyForm
{
    public class FormEditContacts : Form
    {
        private SubFirm _subFirm;
        private ListView _contactListView;
        private Button _btnAddContact;
        private Button _btnEditContact;
        private Button _btnRemoveContact;

        public FormEditContacts(SubFirm subFirm)
        {
            _subFirm = subFirm;
            InitializeComponent();
            UpdateContactList();
        }

        private void InitializeComponent()
        {
            this.Text = "Редактировать контакты";
            this.Width = 600;
            this.Height = 400;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            _contactListView = new ListView
            {
                Dock = DockStyle.Top,
                Height = 250,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true
            };
            _contactListView.Columns.Add("Тип", 100);
            _contactListView.Columns.Add("Начало", 120);
            _contactListView.Columns.Add("Окончание", 120);
            _contactListView.Columns.Add("Описание", 200);
            _contactListView.Columns.Add("Инфо", 150);

            // === Кнопки управления ===
            var panelButtons = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50
            };

            _btnAddContact = new Button
            {
                Text = "Добавить",
                Dock = DockStyle.Left,
                Width = 100
            };
            _btnAddContact.Click += BtnAddContact_Click;

            _btnEditContact = new Button
            {
                Text = "Редактировать",
                Dock = DockStyle.Left,
                Width = 100
            };
            _btnEditContact.Click += BtnEditContact_Click;

            _btnRemoveContact = new Button
            {
                Text = "Удалить",
                Dock = DockStyle.Left,
                Width = 100
            };
            _btnRemoveContact.Click += BtnRemoveContact_Click;

            panelButtons.Controls.Add(_btnRemoveContact);
            panelButtons.Controls.Add(_btnEditContact);
            panelButtons.Controls.Add(_btnAddContact);

            this.Controls.Add(_contactListView);
            this.Controls.Add(panelButtons);
        }

        private void UpdateContactList()
        {
            _contactListView.Items.Clear();
            foreach (var contact in _subFirm.GetContacts())
            {
                var item = new ListViewItem(new[]
                {
                    contact.CntType?.Name ?? "Неизвестно",
                    contact.BeginDt.ToString("dd/MM/yyyy HH:mm"),
                    contact.EndDt.ToString("dd/MM/yyyy HH:mm"),
                    contact.Descr,
                    contact.DataInfo
                });
                _contactListView.Items.Add(item);
            }
        }

        private void BtnAddContact_Click(object sender, EventArgs e)
        {
            var contactTypes = FirmFactory.Instance.ContactTypes.ToArray();
            var addContactForm = new FormAddContact(contactTypes);

            if (addContactForm.ShowDialog() == DialogResult.OK)
            {
                var newContact = addContactForm.NewContact;
                _subFirm.AddCont(newContact);
                UpdateContactList();
            }
        }
        private void BtnEditContact_Click(object sender, EventArgs e)
        {
            if (_contactListView.SelectedItems.Count > 0)
            {
                var index = _contactListView.SelectedItems[0].Index;
                var contact = _subFirm.GetContacts()[index];
                var contactTypes = FirmFactory.Instance.ContactTypes.ToArray();

                var editContactForm = new FormAddContact(contactTypes);
                //editContactForm.NewContact = contact; 

                //editContactForm.SetContactData(contact);

                if (editContactForm.ShowDialog() == DialogResult.OK)
                {
                    _subFirm.GetContacts()[index] = editContactForm.NewContact;
                    UpdateContactList();
                }
            }
            else
            {
                MessageBox.Show("Выберите контакт для редактирования.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRemoveContact_Click(object sender, EventArgs e)
        {
            if (_contactListView.SelectedItems.Count > 0)
            {
                var index = _contactListView.SelectedItems[0].Index;
                var contact = _subFirm.GetContacts()[index];
                //_subFirm.GetContacts().Remove(contact);
                UpdateContactList();
            }
            else
            {
                MessageBox.Show("Выберите контакт для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

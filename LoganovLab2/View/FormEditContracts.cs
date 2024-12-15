using LoganovLab1.Domain;
using LoganovLab1.Factory;
using LoganovLab1.Type;

public class FormEditContacts : Form
{
    private SubFirm _subFirm;
    private ListView _contactListView;
    private Button _btnAddContact;
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
        this.Width = 400;
        this.Height = 300;

        _contactListView = new ListView { Dock = DockStyle.Top, Height = 200, View = View.Details };
        _contactListView.Columns.Add("Тип");
        _contactListView.Columns.Add("Дата");
        _contactListView.Columns.Add("Описание");

        _btnAddContact = new Button { Text = "Добавить", Dock = DockStyle.Left };
        _btnRemoveContact = new Button { Text = "Удалить", Dock = DockStyle.Right };

        _btnAddContact.Click += BtnAddContact_Click;
        _btnRemoveContact.Click += BtnRemoveContact_Click;

        this.Controls.Add(_contactListView);
        this.Controls.Add(_btnAddContact);
        this.Controls.Add(_btnRemoveContact);
    }

    private void UpdateContactList()
    {
        _contactListView.Items.Clear();
        foreach (var contact in _subFirm.GetContacts())
        {
            var item = new ListViewItem(new[] { contact.ContactType.Name, contact.Date.ToString(), contact.Description });
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
            _subFirm.AddContact(newContact);
            UpdateContactList();
        }
    }



    private void BtnRemoveContact_Click(object sender, EventArgs e)
    {
        if (_contactListView.SelectedItems.Count > 0)
        {
            var index = _contactListView.SelectedItems[0].Index;
            var contact = _subFirm.GetContacts()[index];
            _subFirm.RemoveContact(contact);
            UpdateContactList();
        }
    }
}

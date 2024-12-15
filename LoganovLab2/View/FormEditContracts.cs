using System;
using System.Drawing;
using System.Windows.Forms;
using LoganovLab1.Domain;
using LoganovLab1.Factory;
using LoganovLab1.Type;

public class FormEditContacts : Form
{
    private SubFirm _subFirm;
    private ListView _contactListView;

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
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.White;

        // Основной layout
        var mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(10),
            AutoSize = true
        };
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 90));
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));

        // ToolStrip для кнопок
        var toolStrip = new ToolStrip
        {
            Dock = DockStyle.Top,
            GripStyle = ToolStripGripStyle.Hidden,
            BackColor = SystemColors.ControlLight
        };

        var btnAddContact = new ToolStripButton("Добавить", null, BtnAddContact_Click)
        {
            DisplayStyle = ToolStripItemDisplayStyle.Text,
            ForeColor = Color.Black,
            Font = new Font("Segoe UI", 10)
        };

        var btnRemoveContact = new ToolStripButton("Удалить", null, BtnRemoveContact_Click)
        {
            DisplayStyle = ToolStripItemDisplayStyle.Text,
            ForeColor = Color.Black,
            Font = new Font("Segoe UI", 10)
        };

        toolStrip.Items.AddRange(new ToolStripItem[] { btnAddContact, btnRemoveContact });

        // ListView для контактов
        _contactListView = new ListView
        {
            View = View.Details,
            FullRowSelect = true,
            GridLines = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10),
            BackColor = Color.White
        };

        _contactListView.Columns.Add("Тип", 150);
        _contactListView.Columns.Add("Дата", 150);
        _contactListView.Columns.Add("Описание", 250);

        // Добавляем элементы на форму
        mainLayout.Controls.Add(_contactListView, 0, 0);
        mainLayout.Controls.Add(toolStrip, 0, 1);

        this.Controls.Add(mainLayout);
    }

    private void UpdateContactList()
    {
        _contactListView.Items.Clear();
        foreach (var contact in _subFirm.GetContacts())
        {
            var item = new ListViewItem(new[]
            {
                contact.ContactType.Name,
                contact.Date.ToString("dd.MM.yyyy"),
                contact.Description
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
        else
        {
            MessageBox.Show("Выберите контакт для удаления.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

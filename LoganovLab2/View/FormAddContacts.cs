using System;
using System.Windows.Forms;
using LoganovLab1.Domain; // Обновите namespace в соответствии с вашим проектом
using LoganovLab1.Type;
using System.ComponentModel;

public class FormAddContact : Form
{
    private ComboBox _cmbContactType;
    private DateTimePicker _dtpDate;
    private TextBox _txtDescription;
    private Button _btnSave;
    private Button _btnCancel;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Contact NewContact { get; private set; }

    public FormAddContact(ContactType[] availableTypes)
    {
        InitializeComponent(availableTypes);
    }

    private void InitializeComponent(ContactType[] availableTypes)
    {
        this.Text = "Добавить контакт";
        this.Width = 400;
        this.Height = 250;

        var lblType = new Label { Text = "Тип контакта:", Dock = DockStyle.Top };
        _cmbContactType = new ComboBox { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList };
        _cmbContactType.Items.AddRange(availableTypes);

        var lblDate = new Label { Text = "Дата контакта:", Dock = DockStyle.Top };
        _dtpDate = new DateTimePicker { Dock = DockStyle.Top };

        var lblDescription = new Label { Text = "Описание:", Dock = DockStyle.Top };
        _txtDescription = new TextBox { Dock = DockStyle.Top };

        var panelButtons = new Panel { Dock = DockStyle.Bottom, Height = 50 };
        _btnSave = new Button { Text = "Сохранить", Dock = DockStyle.Right };
        _btnSave.Click += BtnSave_Click;
        _btnCancel = new Button { Text = "Отмена", Dock = DockStyle.Left };
        _btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

        panelButtons.Controls.Add(_btnSave);
        panelButtons.Controls.Add(_btnCancel);

        this.Controls.Add(_txtDescription);
        this.Controls.Add(lblDescription);
        this.Controls.Add(_dtpDate);
        this.Controls.Add(lblDate);
        this.Controls.Add(_cmbContactType);
        this.Controls.Add(lblType);
        this.Controls.Add(panelButtons);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        if (_cmbContactType.SelectedItem is ContactType selectedType)
        {
            NewContact = new Contact
            {
                ContactType = selectedType,
                Date = _dtpDate.Value,
                Description = _txtDescription.Text
            };
            this.DialogResult = DialogResult.OK;
        }
        else
        {
            MessageBox.Show("Выберите тип контакта.");
        }
    }
}

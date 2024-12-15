using System;
using System.Drawing;
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
        this.Width = 500;
        this.Height = 350;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.White;

        // Создаем общий адаптивный layout
        var mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            Padding = new Padding(10),
            RowStyles = { new RowStyle(SizeType.Absolute, 40), new RowStyle(SizeType.Absolute, 40), new RowStyle(SizeType.Absolute, 100), new RowStyle(SizeType.Absolute, 50) },
            AutoSize = true
        };

        // Метка и ComboBox "Тип контакта"
        var lblType = new Label
        {
            Text = "Тип контакта:",
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10, FontStyle.Regular)
        };
        _cmbContactType = new ComboBox
        {
            Dock = DockStyle.Fill,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 10),
            Margin = new Padding(5)
        };
        _cmbContactType.Items.AddRange(availableTypes);

        // Метка и DateTimePicker "Дата контакта"
        var lblDate = new Label
        {
            Text = "Дата контакта:",
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10, FontStyle.Regular)
        };
        _dtpDate = new DateTimePicker
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10),
            Margin = new Padding(5)
        };

        // Метка и TextBox "Описание"
        var lblDescription = new Label
        {
            Text = "Описание:",
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10, FontStyle.Regular)
        };
        _txtDescription = new TextBox
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10),
            Multiline = true,
            Margin = new Padding(5)
        };

        // Панель с кнопками
        var panelButtons = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft,
            Dock = DockStyle.Fill,
            Padding = new Padding(0, 10, 10, 10)
        };

        _btnSave = new Button
        {
            Text = "Сохранить",
            Font = new Font("Segoe UI", 10),
            BackColor = Color.LightGreen,
            AutoSize = true,
            Padding = new Padding(10)
        };
        _btnSave.Click += BtnSave_Click;

        _btnCancel = new Button
        {
            Text = "Отмена",
            Font = new Font("Segoe UI", 10),
            BackColor = Color.LightCoral,
            AutoSize = true,
            Padding = new Padding(10)
        };
        _btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

        panelButtons.Controls.AddRange(new Control[] { _btnSave, _btnCancel });

        // Добавляем элементы в layout
        mainLayout.Controls.Add(lblType, 0, 0);
        mainLayout.Controls.Add(_cmbContactType, 1, 0);
        mainLayout.Controls.Add(lblDate, 0, 1);
        mainLayout.Controls.Add(_dtpDate, 1, 1);
        mainLayout.Controls.Add(lblDescription, 0, 2);
        mainLayout.Controls.Add(_txtDescription, 1, 2);
        mainLayout.SetColumnSpan(_txtDescription, 2); // Описание занимает обе колонки
        mainLayout.Controls.Add(panelButtons, 0, 3);
        mainLayout.SetColumnSpan(panelButtons, 2); // Кнопки тоже на обе колонки

        // Добавляем layout на форму
        this.Controls.Add(mainLayout);
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
            MessageBox.Show("Выберите тип контакта.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}

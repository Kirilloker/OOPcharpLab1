using System;
using System.Drawing;
using System.Windows.Forms;
using LoganovLab1.Domain;

public class FormEditFirm : Form
{
    private Firm _firm;
    private TextBox _txtFullName;
    private TextBox _txtShortName;
    private TextBox _txtRegion;
    private TextBox _txtCity;
    private Button _btnSave;

    public FormEditFirm(Firm firm)
    {
        _firm = firm;
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        // Основные настройки формы
        this.Text = "Добавить фирму";
        this.Width = 500;
        this.Height = 400;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.BackColor = Color.White;

        // Создаем основной TableLayoutPanel
        var mainLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(15),
            RowCount = 6,
            ColumnCount = 2,
            AutoSize = true
        };
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30));
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70));


        // Поле "Полное имя"
        var lblFullName = CreateLabel("Полное имя:");
        _txtFullName = CreateTextBox(_firm.FullName);


        // Поле "Регион"
        var lblRegion = CreateLabel("Регион:");
        _txtRegion = CreateTextBox(_firm.Region);

        // Поле "Город"
        var lblCity = CreateLabel("Город:");
        _txtCity = CreateTextBox(_firm.City);

        // Кнопка "Сохранить"
        _btnSave = new Button
        {
            Text = "Сохранить",
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            BackColor = Color.LightGreen,
            FlatStyle = FlatStyle.Flat,
            AutoSize = true,
            Padding = new Padding(10),
            Anchor = AnchorStyles.Right
        };
        _btnSave.Click += BtnSave_Click;

        // Добавляем элементы в таблицу
        mainLayout.Controls.Add(lblFullName, 0, 0);
        mainLayout.Controls.Add(_txtFullName, 1, 0);
        mainLayout.Controls.Add(lblRegion, 0, 2);
        mainLayout.Controls.Add(_txtRegion, 1, 2);
        mainLayout.Controls.Add(lblCity, 0, 3);
        mainLayout.Controls.Add(_txtCity, 1, 3);

        // Добавляем кнопку в отдельную строку
        var buttonPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft,
            Dock = DockStyle.Fill,
            Padding = new Padding(5)
        };
        buttonPanel.Controls.Add(_btnSave);

        mainLayout.Controls.Add(buttonPanel, 1, 4);
        this.Controls.Add(mainLayout);
    }

    private Label CreateLabel(string text)
    {
        return new Label
        {
            Text = text,
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Fill,
            Margin = new Padding(5)
        };
    }

    private TextBox CreateTextBox(string text)
    {
        return new TextBox
        {
            Text = text,
            Font = new Font("Segoe UI", 10, FontStyle.Regular),
            Dock = DockStyle.Fill,
            Margin = new Padding(5)
        };
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        _firm.FullName = _txtFullName.Text;
        _firm.Region = _txtRegion.Text;
        _firm.City = _txtCity.Text;

        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}

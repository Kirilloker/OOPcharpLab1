using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using LoganovLab2.Firms;
using System.ComponentModel;

public class FormSelectFields : Form
{
    private CheckedListBox _clbFields; // Список с чекбоксами для выбора полей
    private Button _btnOk;
    private Button _btnCancel;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public List<Field> SelectedFields { get; private set; }

    public FormSelectFields(IEnumerable<Field> allFields, IEnumerable<Field> currentFields)
    {
        InitializeComponent(allFields, currentFields);
    }

    private void InitializeComponent(IEnumerable<Field> allFields, IEnumerable<Field> currentFields)
    {
        this.Text = "Выбор полей для отображения";
        this.Width = 300;
        this.Height = 400;

        _clbFields = new CheckedListBox
        {
            Dock = DockStyle.Top,
            Height = 300,
            CheckOnClick = true
        };

        // Заполняем список полей
        foreach (var field in allFields)
        {
            bool isChecked = currentFields.Contains(field);
            _clbFields.Items.Add(field, isChecked);
        }

        // Кнопки
        var panelButtons = new Panel { Dock = DockStyle.Bottom, Height = 50 };
        _btnOk = new Button { Text = "OK", Dock = DockStyle.Right };
        _btnCancel = new Button { Text = "Отмена", Dock = DockStyle.Left };

        _btnOk.Click += BtnOk_Click;
        _btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

        panelButtons.Controls.Add(_btnOk);
        panelButtons.Controls.Add(_btnCancel);

        this.Controls.Add(_clbFields);
        this.Controls.Add(panelButtons);
    }

    private void BtnOk_Click(object sender, EventArgs e)
    {
        SelectedFields = _clbFields.CheckedItems.Cast<Field>().ToList();
        this.DialogResult = DialogResult.OK;
    }
}

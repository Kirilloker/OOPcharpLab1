using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LoganovLab2.Firms;
using System.ComponentModel;

public class FormSelectFields : Form
{
    private CheckedListBox _clbFields;
    private ToolStrip _toolStrip;
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
        // Настройки формы
        this.Text = "Выбор полей для отображения";
        this.Width = 400;
        this.Height = 500;
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.BackColor = Color.White;

        // ToolStrip для кнопок (всегда сверху)
        _toolStrip = new ToolStrip
        {
            Dock = DockStyle.Top,
            GripStyle = ToolStripGripStyle.Hidden,
            BackColor = SystemColors.ControlLight
        };

        var btnOkTool = new ToolStripButton("OK", null, BtnOk_Click)
        {
            DisplayStyle = ToolStripItemDisplayStyle.Text,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.Black
        };

        var btnCancelTool = new ToolStripButton("Отмена", null, (s, e) => this.DialogResult = DialogResult.Cancel)
        {
            DisplayStyle = ToolStripItemDisplayStyle.Text,
            Font = new Font("Segoe UI", 10),
            ForeColor = Color.Black
        };

        _toolStrip.Items.AddRange(new ToolStripItem[] { btnOkTool, btnCancelTool });
        this.Controls.Add(_toolStrip);

        // Панель-контейнер для создания отступа
        var panelContainer = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(0, 50, 0, 0) // Отступ сверху в 50 пикселей
        };

        // CheckedListBox для выбора полей
        _clbFields = new CheckedListBox
        {
            Dock = DockStyle.Fill,
            CheckOnClick = true,
            Font = new Font("Segoe UI", 10),
            BorderStyle = BorderStyle.FixedSingle
        };

        // Заполняем список полей
        foreach (var field in allFields)
        {
            bool isChecked = currentFields.Contains(field);
            _clbFields.Items.Add(field, isChecked);
        }

        panelContainer.Controls.Add(_clbFields);
        this.Controls.Add(panelContainer);
    }


    private void BtnOk_Click(object sender, EventArgs e)
    {
        SelectedFields = _clbFields.CheckedItems.Cast<Field>().ToList();
        this.DialogResult = DialogResult.OK;
    }
}

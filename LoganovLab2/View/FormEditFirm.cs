using LoganovLab1.Domain;
using System.Windows.Forms;

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
        this.Text = "Редактировать фирму";
        this.Width = 400;
        this.Height = 300;

        var lblFullName = new Label { Text = "Полное имя", Dock = DockStyle.Top };
        _txtFullName = new TextBox { Text = _firm.FullName, Dock = DockStyle.Top };
        var lblShortName = new Label { Text = "Краткое имя", Dock = DockStyle.Top };
        _txtShortName = new TextBox { Text = _firm.ShortName, Dock = DockStyle.Top };
        var lblRegion = new Label { Text = "Регион", Dock = DockStyle.Top };
        _txtRegion = new TextBox { Text = _firm.Region, Dock = DockStyle.Top };
        var lblCity = new Label { Text = "Город", Dock = DockStyle.Top };
        _txtCity = new TextBox { Text = _firm.City, Dock = DockStyle.Top };

        _btnSave = new Button { Text = "Сохранить", Dock = DockStyle.Bottom };
        _btnSave.Click += BtnSave_Click;

        this.Controls.Add(lblCity);
        this.Controls.Add(_txtCity);
        this.Controls.Add(lblRegion);
        this.Controls.Add(_txtRegion);
        this.Controls.Add(lblShortName);
        this.Controls.Add(_txtShortName);
        this.Controls.Add(lblFullName);
        this.Controls.Add(_txtFullName);
        this.Controls.Add(_btnSave);
    }

    private void BtnSave_Click(object sender, EventArgs e)
    {
        _firm.FullName = _txtFullName.Text;
        _firm.ShortName = _txtShortName.Text;
        _firm.Region = _txtRegion.Text;
        _firm.City = _txtCity.Text;

        this.DialogResult = DialogResult.OK;
        this.Close();
    }
}

using LoganovLab1.Factory;
using LoganovLab2.Controller;
using LoganovLab2.Firms;
using LoganovLab2.Rules;
using LoganovLab2.View;

namespace LoganovLab2
{
    public class FormMain : Form
    {
        private MainController _mainContr;
        private ListView _listView;
        private Button _btnFilter;
        private Button _btnAddFirm;
        private Button _btnEditFirm;
        private Button _btnEditContacts;
        private Button _btnSelectFields;

        public FormMain(MainController mainContr)
        {
            _mainContr = mainContr;
            InitializeComponent();
            UpdateFirmList();
        }

        private void InitializeComponent()
        {
            this.Text = "Основная форма";
            this.Width = 1000;
            this.Height = 600;

            _listView = new ListView();
            _listView.Dock = DockStyle.Top;
            _listView.View = System.Windows.Forms.View.Details;
            _listView.FullRowSelect = true;
            _listView.GridLines = true;
            _listView.Height = 500;
            this.Controls.Add(_listView);

            _btnFilter = new Button();
            _btnFilter.Text = "Фильтр";
            _btnFilter.Dock = DockStyle.Bottom;
            _btnFilter.Click += BtnFilter_Click;
            this.Controls.Add(_btnFilter);

            _btnAddFirm = new Button();
            _btnAddFirm.Text = "Добавить фирму";
            _btnAddFirm.Dock = DockStyle.Bottom;
            _btnAddFirm.Click += BtnAddFirm_Click;
            this.Controls.Add(_btnAddFirm);

            _btnEditFirm = new Button();
            _btnEditFirm.Text = "Редактировать фирму";
            _btnEditFirm.Dock = DockStyle.Bottom;
            _btnEditFirm.Click += BtnEditFirm_Click;
            this.Controls.Add(_btnEditFirm);

            _btnSelectFields = new Button();
            _btnSelectFields.Text = "Выбрать поля";
            _btnSelectFields.Dock = DockStyle.Bottom;
            _btnSelectFields.Click += BtnSelectFields_Click;
            this.Controls.Add(_btnSelectFields);

            _btnEditContacts = new Button { Text = "Добавление и редактирование контактов", Dock = DockStyle.Bottom };
            _btnEditContacts.Click += BtnEditContacts_Click;
            this.Controls.Add(_btnEditContacts);
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            _mainContr.StartFilterProcess();
            UpdateFirmList();
        }

        private void UpdateFirmList()
        {
            _listView.Clear();

            var fields = _mainContr.FirmManager.FirmView.GetFields();
            foreach (var field in fields)
            {
                _listView.Columns.Add(field.FieldName);
            }

            var firms = _mainContr.FirmManager.GetAllFirms();
            foreach (var firm in firms)
            {
                var vals = _mainContr.FirmManager.FirmView.GetFieldValues(firm);
                var subItems = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    subItems[i] = vals[fields[i]];
                }
                var lvi = new ListViewItem(subItems);
                _listView.Items.Add(lvi);
            }

            for (int i = 0; i < _listView.Columns.Count; i++)
            {
                _listView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }

        private void BtnAddFirm_Click(object sender, EventArgs e)
        {
            var newFirm = FirmFactory.Instance.CreateFirm(); // Создание новой фирмы
            var editForm = new FormEditFirm(newFirm); // Форма редактирования
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                _mainContr.FirmManager.AddFirm(newFirm);
                UpdateFirmList();
            }
        }

        private void BtnSelectFields_Click(object sender, EventArgs e)
        {
            var allFields = _mainContr.FirmManager.FirmView.GetFields();
            var currentFields = _mainContr.FirmManager.FirmView.GetFields();

            var formSelectFields = new FormSelectFields(allFields, currentFields);

            if (formSelectFields.ShowDialog() == DialogResult.OK)
            {
                // Обновляем список полей в FirmView
                var newFields = formSelectFields.SelectedFields;
                _mainContr.FirmManager.FirmView = new FirmView();

                foreach (var field in newFields)
                {
                    _mainContr.FirmManager.FirmView.AddField(field);
                }

                UpdateFirmList(); // Обновляем таблицу
            }
        }


        private void BtnEditContacts_Click(object sender, EventArgs e)
        {
            var firms = _mainContr.FirmManager.GetAllFirms();
            if (firms.Length > 0)
            {
                var mainOffice = firms[0].GetMainOffice(); // Получаем основное подразделение первой фирмы
                if (mainOffice != null)
                {
                    var contactForm = new FormEditContacts(mainOffice);
                    contactForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Основное подразделение не найдено.");
                }
            }
            else
            {
                MessageBox.Show("Фирма не найдена.");
            }
        }


        private void BtnEditFirm_Click(object sender, EventArgs e)
        {
            if (_listView.SelectedItems.Count > 0)
            {
                var selectedIndex = _listView.SelectedItems[0].Index;
                var selectedFirm = _mainContr.FirmManager.GetAllFirms()[selectedIndex];

                var editForm = new FormEditFirm(selectedFirm); // Форма редактирования
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    UpdateFirmList();
                }
            }
            else
            {
                MessageBox.Show("Выберите фирму для редактирования.");
            }
        }
    }

}

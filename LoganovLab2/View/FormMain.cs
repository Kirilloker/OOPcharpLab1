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
        private TableLayoutPanel _mainLayout;
        private FlowLayoutPanel _buttonPanel;

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

            // Создаем ToolStrip как навигационную панель (всегда сверху)
            var toolStrip = new ToolStrip
            {
                Dock = DockStyle.Top,
                GripStyle = ToolStripGripStyle.Hidden,
                BackColor = SystemColors.ControlLight
            };

            var btnFilter = new ToolStripButton("Фильтр", null, BtnFilter_Click) { DisplayStyle = ToolStripItemDisplayStyle.Text };
            var btnAddFirm = new ToolStripButton("Добавить фирму", null, BtnAddFirm_Click) { DisplayStyle = ToolStripItemDisplayStyle.Text };
            var btnEditFirm = new ToolStripButton("Редактировать фирму", null, BtnEditFirm_Click) { DisplayStyle = ToolStripItemDisplayStyle.Text };
            var btnSelectFields = new ToolStripButton("Выбрать поля", null, BtnSelectFields_Click) { DisplayStyle = ToolStripItemDisplayStyle.Text };
            var btnEditContacts = new ToolStripButton("Редактировать контакты", null, BtnEditContacts_Click) { DisplayStyle = ToolStripItemDisplayStyle.Text };

            toolStrip.Items.AddRange(new ToolStripItem[] { btnFilter, btnAddFirm, btnEditFirm, btnSelectFields, btnEditContacts });
            this.Controls.Add(toolStrip);

            // Главная панель для ListView с отступом
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 50, 0, 0) // Отступ сверху для ListView
            };

            // ListView для отображения данных
            _listView = new ListView
            {
                View = System.Windows.Forms.View.Details,
                FullRowSelect = true,
                GridLines = true,
                Dock = DockStyle.Fill
            };

            mainPanel.Controls.Add(_listView);
            this.Controls.Add(mainPanel);
        }

        private Button CreateButton(string text, EventHandler clickHandler)
        {
            var button = new Button
            {
                Text = text,
                AutoSize = true,
                Margin = new Padding(10),
                Padding = new Padding(10)
            };

            button.Click += clickHandler; 
            return button;
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
            var newFirm = FirmFactory.Instance.CreateFirm();
            var editForm = new FormEditFirm(newFirm);
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
                var newFields = formSelectFields.SelectedFields;
                _mainContr.FirmManager.FirmView = new FirmView();

                foreach (var field in newFields)
                {
                    _mainContr.FirmManager.FirmView.AddField(field);
                }

                UpdateFirmList();
            }
        }

        private void BtnEditContacts_Click(object sender, EventArgs e)
        {
            var firms = _mainContr.FirmManager.GetAllFirms();
            if (firms.Length > 0)
            {
                var mainOffice = firms[0].GetMainOffice();
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

                var editForm = new FormEditFirm(selectedFirm);
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

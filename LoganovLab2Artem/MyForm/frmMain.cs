using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Controller;
using LoganovLab2Artem.FirmSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.MyForm
{
    public class frmMain : Form
    {
        private ListView listViewFirms;
        private MainContr _mainContr;
        private Button _btnFilter;
        private Button _btnAddFirm;
        private Button _btnEditFirm;
        private Button _btnEditContacts;
        private Button _btnSelectFields;

        public frmMain(MainContr mainContr)
        {
            _mainContr = mainContr;
            InitializeComponent();
            RefreshList();
        }

        private void InitializeComponent()
        {
            this.Text = "Главное окно";
            this.Width = 1000;
            this.Height = 600;

            listViewFirms = new ListView
            {
                Dock = DockStyle.Top,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Height = 500
            };
            this.Controls.Add(listViewFirms);

            _btnFilter = new Button
            {
                Text = "Фильтр",
                Dock = DockStyle.Bottom
            };
            _btnFilter.Click += (s, e) => { _mainContr.StartFilter(); RefreshList(); };
            this.Controls.Add(_btnFilter);

            _btnAddFirm = new Button
            {
                Text = "Добавить фирму",
                Dock = DockStyle.Bottom
            };
            _btnAddFirm.Click += BtnAddFirm_Click;
            this.Controls.Add(_btnAddFirm);

            _btnEditFirm = new Button
            {
                Text = "Редактировать фирму",
                Dock = DockStyle.Bottom
            };
            _btnEditFirm.Click += BtnEditFirm_Click;
            this.Controls.Add(_btnEditFirm);

            _btnEditContacts = new Button
            {
                Text = "Добавить и редактировать контакты",
                Dock = DockStyle.Bottom
            };
            _btnEditContacts.Click += BtnEditContacts_Click;
            this.Controls.Add(_btnEditContacts);

            _btnSelectFields = new Button
            {
                Text = "Выбрать поля",
                Dock = DockStyle.Bottom
            };
            _btnSelectFields.Click += BtnSelectFields_Click;
            this.Controls.Add(_btnSelectFields);
        }

        public void DisplayFirms(FirmMngr mngr, FirmVw vw)
        {
            listViewFirms.Clear();
            var fields = vw.GetFields();
            foreach (var field in fields)
            {
                listViewFirms.Columns.Add(field.GetType().Name.Replace("Field", ""));
            }

            foreach (var firm in mngr.GetAllFirms())
            {
                var values = vw.GetFirmValues(firm);
                var lvi = new ListViewItem(values[0]);
                for (int i = 1; i < values.Length; i++)
                {
                    lvi.SubItems.Add(values[i]);
                }
                listViewFirms.Items.Add(lvi);
            }
        }

        private void RefreshList()
        {
            DisplayFirms(_mainContr.CurrentFirmMngr, _mainContr.CurrentFirmMngr.CurrentView);
        }

        private void BtnAddFirm_Click(object sender, EventArgs e)
        {
            //var newFirm = new Firm(_mainContr.CurrentFirmMngr.CurrentView.GetFieldNames());
            //var editForm = new FormEditFirm(newFirm);
            //if (editForm.ShowDialog() == DialogResult.OK)
            //{
            //    //_mainContr.CurrentFirmMngr.AddFirm(newFirm);
            //    RefreshList();
            //}
        }

        private void BtnEditFirm_Click(object sender, EventArgs e)
        {
            if (listViewFirms.SelectedItems.Count > 0)
            {
                var selectedIndex = listViewFirms.SelectedItems[0].Index;
                var selectedFirm = _mainContr.CurrentFirmMngr.GetAllFirms()[selectedIndex];
                var editForm = new FormEditFirm(selectedFirm);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    RefreshList();
                }
            }
            else
            {
                MessageBox.Show("Выберите фирму для редактирования.");
            }
        }

        private void BtnEditContacts_Click(object sender, EventArgs e)
        {
            if (listViewFirms.SelectedItems.Count > 0)
            {
                var selectedIndex = listViewFirms.SelectedItems[0].Index;
                var selectedFirm = _mainContr.CurrentFirmMngr.GetAllFirms()[selectedIndex];
                var mainOffice = selectedFirm.GetAllSubFirms().FirstOrDefault(sf => sf.IsMain);
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
                MessageBox.Show("Выберите фирму для редактирования контактов.");
            }
        }

        private void BtnSelectFields_Click(object sender, EventArgs e)
        {
            var currentFields = _mainContr.CurrentFirmMngr.CurrentView.GetFields();
            //var formSelectFields = new FormSelectFields(currentFields);
            //if (formSelectFields.ShowDialog() == DialogResult.OK)
            //{
            //    var newFields = formSelectFields.SelectedFields;
            //    _mainContr.CurrentFirmMngr.CurrentView = new FirmVw();
            //    foreach (var field in newFields)
            //    {
            //        _mainContr.CurrentFirmMngr.CurrentView.AddField(field);
            //    }
            //    RefreshList();
            //}
        }
    }
}

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
    }

    public class MainController
    {
        private FirmManager _firmManager;
        private FirmManager _originalFirmManager;

        public MainController(FirmManager initialManager)
        {
            _firmManager = initialManager;
            _originalFirmManager = new FirmManager(initialManager.FirmView, initialManager.GetAllFirms()); 
        }

        public FirmManager FirmManager
        {
            get { return _firmManager; }
        }

        public void StartFilterProcess()
        {
            var filterController = new FilterController(_originalFirmManager); 

            if (filterController.ShowFilterForm() == DialogResult.OK)
            {
                var filteredMngr = filterController.GetFilteredFirmManager();

                if (filteredMngr != null)
                {
                    _firmManager = filteredMngr; 
                }
            }
        }
    }

    public class FilterController
    {
        private FirmManager _originalFirmManager;
        private FirmManager _filteredFirmManager;
        private List<FilterRule> _rules = new List<FilterRule>();

        public FilterController(FirmManager firmManager)
        {
            _originalFirmManager = new FirmManager(firmManager.FirmView, firmManager.GetAllFirms()); 
        }

        public DialogResult ShowFilterForm()
        {
            using (var form = new FormFilter(_originalFirmManager.FirmView))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _rules = form.GetSelectedRules();
                    ApplyFilter();
                }

                return result;
            }
        }

        private void ApplyFilter()
        {
            var allFirms = _originalFirmManager.GetAllFirms(); 

            var filtered = allFirms.Where(firm =>
            {
                return _rules.All(r => r.FirmRespond(firm));
            }).ToArray();

            _filteredFirmManager = new FirmManager(_originalFirmManager.FirmView, filtered);
        }

        public FirmManager GetFilteredFirmManager()
        {
            return _filteredFirmManager;
        }
    }
}

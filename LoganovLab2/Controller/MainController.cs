using LoganovLab2.Firms;

namespace LoganovLab2.Controller
{
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
}

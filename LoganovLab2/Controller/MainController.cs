using LoganovLab2.Firms;

namespace LoganovLab2.Controller
{
    public class MainController
    {
        private FirmManager _firmManager;

        public MainController(FirmManager initialManager)
        {
            _firmManager = initialManager;
        }

        public FirmManager FirmManager
        {
            get { return _firmManager; }
        }

        public void StartFilterProcess()
        {
            var filterController = new FilterController(_firmManager);

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

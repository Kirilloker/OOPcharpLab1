using LoganovLab2.Firms;
using LoganovLab2.Rules;
using LoganovLab2.View;

namespace LoganovLab2.Controller
{
    public class FilterController
    {
        private FirmManager _currentFirmManager;
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

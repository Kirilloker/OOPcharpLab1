using LoganovLab2.Firms;
using LoganovLab2.Rules;
using LoganovLab2.View;

namespace LoganovLab2.Controller
{
    public class FilterController
    {
        private FirmManager _currentFirmManager;
        private FirmManager _filteredFirmManager;
        private List<FilterRule> _rules = new List<FilterRule>();

        public FilterController(FirmManager firmManager)
        {
            _currentFirmManager = firmManager;
        }

        public DialogResult ShowFilterForm()
        {
            using (var form = new FormFilter(_currentFirmManager.FirmView))
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
            var allFirms = _currentFirmManager.GetAllFirms();

            var filtered = allFirms.Where(firm =>
            {
                return _rules.All(r => r.FirmRespond(firm));
            }).ToArray();

            _filteredFirmManager = new FirmManager(_currentFirmManager.FirmView, filtered);
        }

        public FirmManager GetFilteredFirmManager()
        {
            return _filteredFirmManager;
        }
    }
}

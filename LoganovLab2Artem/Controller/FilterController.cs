using LoganovLab2Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Controller
{
    public class FilterContr
    {
        private MainContr _mainContr;
        private FirmMngr _firmMngr;
        private FilterRule _filterRule;

        public FilterContr(MainContr mainContr, FirmMngr firmMngr)
        {
            _mainContr = mainContr;
            _firmMngr = firmMngr;
            _filterRule = new FilterRule();
        }

        public void AddRule(Rule rule)
        {
            _filterRule.AddRule(rule);
        }

        public FirmMngr GetFilteredFirmMngr()
        {
            var filtered = _filterRule.Apply(_firmMngr.GetAllFirms());
            return new FirmMngr(_firmMngr.CurrentView.Clone(), filtered);
        }
    }
}

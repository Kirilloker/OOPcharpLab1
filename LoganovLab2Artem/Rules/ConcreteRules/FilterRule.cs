using LoganovLab1Artem.FirmSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Rules.ConcreteRules
{
    public class FilterRule
    {
        private List<Rule> _rules = new List<Rule>();

        public void AddRule(Rule r)
        {
            if (r != null)
                _rules.Add(r);
        }

        public Firm[] Apply(Firm[] firms)
        {
            return firms.Where(f => _rules.All(r => r.IsMatch(f))).ToArray();
        }
    }
}

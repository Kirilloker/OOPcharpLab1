using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Expressions.ConcreteExpressions
{
    public class ExpContains : ILogExp
    {
        private string _val;
        public ExpContains(object val) { _val = val?.ToString() ?? ""; }
        public bool Compare(object fieldValue)
        {
            return fieldValue != null && fieldValue.ToString().Contains(_val, StringComparison.OrdinalIgnoreCase);
        }
    }
}

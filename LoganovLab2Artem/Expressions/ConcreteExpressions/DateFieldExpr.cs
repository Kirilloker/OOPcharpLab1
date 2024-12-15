using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Expressions.ConcreteExpressions
{
    public class DateFieldExpr : ILogExp
    {
        private ILogExp _exp;
        public DateFieldExpr(ILogExp exp) { _exp = exp; }
        public bool Compare(object fieldValue)
        {
            DateTime val;
            if (DateTime.TryParse(fieldValue?.ToString(), out val))
            {
                return _exp.Compare(val);
            }
            return false;
        }
    }
}

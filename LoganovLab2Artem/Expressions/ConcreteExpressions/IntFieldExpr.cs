using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Expressions.ConcreteExpressions
{
    public class IntFieldExpr : ILogExp
    {
        private ILogExp _exp;
        public IntFieldExpr(ILogExp exp) { _exp = exp; }
        public bool Compare(object fieldValue)
        {
            if (int.TryParse(fieldValue?.ToString(), out int ival))
            {
                return _exp.Compare(ival);
            }
            return false;
        }
    }
}

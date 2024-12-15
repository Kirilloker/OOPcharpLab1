using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Expressions.ConcreteExpressions
{
    public class StrFieldExpr : ILogExp
    {
        private ILogExp _exp;
        public StrFieldExpr(ILogExp exp) { _exp = exp; }
        public bool Compare(object fieldValue) => _exp.Compare(fieldValue?.ToString());
    }
}

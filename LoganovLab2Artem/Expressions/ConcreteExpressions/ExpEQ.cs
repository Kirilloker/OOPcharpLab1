using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Expressions.ConcreteExpressions
{
    public class ExpEQ : ILogExp
    {
        private object _val;
        public ExpEQ(object val) { _val = val; }
        public bool Compare(object fieldValue) => Equals(fieldValue, _val);
    }
}

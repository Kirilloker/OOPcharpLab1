using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Expressions.ConcreteExpressions
{
    public class ExpNoEQ : ILogExp
    {
        private object _val;
        public ExpNoEQ(object val) { _val = val; }
        public bool Compare(object fieldValue) => !Equals(fieldValue, _val);
    }
}

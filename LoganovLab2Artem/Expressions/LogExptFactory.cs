using LoganovLab2Artem.Expressions.ConcreteExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Expressions
{
    public static class LogExpFactory
    {
        public static ILogExp CreateExp(LogExpEnum type, object value)
        {
            switch (type)
            {
                case LogExpEnum.EQ: return new ExpEQ(value);
                case LogExpEnum.NoEQ: return new ExpNoEQ(value);
                case LogExpEnum.GT: return new ExpGT(value);
                case LogExpEnum.LT: return new ExpLT(value);
                case LogExpEnum.GE: return new ExpGE(value);
                case LogExpEnum.LE: return new ExpLE(value);
                case LogExpEnum.Contains: return new ExpContains(value);
                case LogExpEnum.NoContains: return new ExpNoContains(value);
                default: return null;
            }
        }
    }
}

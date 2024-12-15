using LoganovLab2Artem.Expressions;
using LoganovLab2Artem.Expressions.ConcreteExpressions;
using LoganovLab2Artem.FieldSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.Rules.ConcreteRules
{
    public class ContCountRule : Rule
    {
        public ContCountRule(Field field) : base(field) { }
        public override ILogExp CreateFieldExpr(ILogExp baseExp) => new IntFieldExpr(baseExp);
    }
}

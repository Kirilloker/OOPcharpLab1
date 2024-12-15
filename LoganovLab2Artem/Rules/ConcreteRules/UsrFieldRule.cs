using LoganovLab2Artem.Expressions;
using LoganovLab2Artem.Expressions.ConcreteExpressions;
using LoganovLab2Artem.FieldSpace;


namespace LoganovLab2Artem.Rules.ConcreteRules
{
    public class UsrFieldRule : Rule
    {
        private string _usrFieldName;
        public UsrFieldRule(Field field, string usrFieldName) : base(field)
        {
            _usrFieldName = usrFieldName;
        }
        public override ILogExp CreateFieldExpr(ILogExp baseExp) => new StrFieldExpr(baseExp);
    }
}

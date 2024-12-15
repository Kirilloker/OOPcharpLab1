using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Expressions;
using LoganovLab2Artem.FieldSpace;

namespace LoganovLab2Artem.Rules
{
    public abstract class Rule
    {
        protected Field _field;
        protected ILogExp _expr;

        public Rule(Field field)
        {
            _field = field;
        }

        public void SetExpression(ILogExp expr)
        {
            _expr = expr;
        }

        public bool IsMatch(Firm f)
        {
            var val = _field.GetValue(f);
            return _expr == null || _expr.Compare(val);
        }

        public abstract ILogExp CreateFieldExpr(ILogExp baseExp);
    }

}

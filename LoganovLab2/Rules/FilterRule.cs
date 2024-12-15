using LoganovLab1.Domain;
using LoganovLab2.Firms;

namespace LoganovLab2.Rules
{
    public abstract class FilterRule
    {
        protected Field _field;
        protected Filtering.ILogExp _logExp;
        protected object _conditionValue;

        public FilterRule(Field field)
        {
            _field = field;
        }

        public void SetExpression(Filtering.ILogExp logExp)
        {
            _logExp = logExp;
        }

        public void SetConditionValue(object value)
        {
            _conditionValue = value;
        }

        public bool FirmRespond(Firm firm)
        {
            var valStr = _field.GetValue(firm);
            object val = valStr;

            // В зависимости от типа данных поля преобразуем строку в нужный тип
            switch (_field.GetFieldDataType())
            {
                case FieldDataType.Int:
                    if (int.TryParse(valStr, out int iv)) val = iv; 
                    else val = null;
                    break;
                case FieldDataType.DateTime:
                    if (DateTime.TryParse(valStr, out DateTime dv)) val = dv; 
                    else val = null;
                    break;
                case FieldDataType.String:
                default:
                    break;
            }

            return _logExp != null && _logExp.Compare(val, _conditionValue);
        }
    }
}

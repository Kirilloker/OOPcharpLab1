using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;

namespace LoganovLab2Artem.FieldSpace.ConcreteField
{
    public class UsrField : Field
    {
        private string _usrFieldName;
        public UsrField(string fieldName)
        {
            _usrFieldName = fieldName;
        }

        public override string GetValue(Firm f) => f.GetField(_usrFieldName);

        public override Field Clone() => new UsrField(_usrFieldName);

        public override Rule CreateRule()
            => new UsrFieldRule(this, _usrFieldName);
    }
}

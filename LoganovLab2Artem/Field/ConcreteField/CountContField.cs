using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.FieldSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;

namespace LoganovLab2Artem.Fieldspace.ConcreteField
{
    public class CountContField : Field
    {
        public override string GetValue(Firm f)
        {
            return f.GetAllContacts().Length.ToString();
        }
        public override Field Clone() => new CountContField();
        public override Rule CreateRule() => new ContCountRule(this);
    }
}

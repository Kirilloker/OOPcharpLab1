using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;

namespace LoganovLab2Artem.FieldSpace.ConcreteField
{
    public class NameField : Field
    {
        public override string GetValue(Firm f) => f.Name;
        public override Field Clone() => new NameField();
        public override Rule CreateRule() => new NameRule(this);
    }
}

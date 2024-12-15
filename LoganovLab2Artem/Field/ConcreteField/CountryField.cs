using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;

namespace LoganovLab2Artem.FieldSpace.ConcreteField
{
    public class CountryField : Field
    {
        public override string GetValue(Firm f) => f.Country;
        public override Field Clone() => new CountryField();
        public override Rule CreateRule() => new CountryRule(this);
    }
}

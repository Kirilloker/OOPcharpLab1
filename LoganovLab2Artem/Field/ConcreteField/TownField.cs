using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;

namespace LoganovLab2Artem.FieldSpace.ConcreteField
{
    public class TownField : Field
    {
        public override string GetValue(Firm f) => f.Town;
        public override Field Clone() => new TownField();
        public override Rule CreateRule() => new TownRule(this);
    }
}

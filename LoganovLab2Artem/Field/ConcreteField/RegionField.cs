using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;

namespace LoganovLab2Artem.FieldSpace.ConcreteField
{
    public class RegionField : Field
    {
        public override string GetValue(Firm f) => f.Region;
        public override Field Clone() => new RegionField();
        public override Rule CreateRule() => new RegionRule(this);

        
    }
}

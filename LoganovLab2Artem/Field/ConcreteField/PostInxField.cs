using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;

namespace LoganovLab2Artem.FieldSpace.ConcreteField
{
    public class PostInxField : Field
    {
        public override string GetValue(Firm f) => f.PostInx;
        public override Field Clone() => new PostInxField();
        public override Rule CreateRule() => new PostInxRule(this);
    }

}

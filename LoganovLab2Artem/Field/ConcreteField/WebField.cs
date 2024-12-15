using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;
using System.Data;

namespace LoganovLab2Artem.FieldSpace.ConcreteField
{
    public class WebField : Field
    {
        public override string GetValue(Firm f) => f.Web;
        public override Field Clone() => new WebField();
        public override Rules.Rule CreateRule() => new WebRule(this);
    }
}

using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;

namespace LoganovLab2Artem.FieldSpace.ConcreteField
{
    public class DateInField : Field
    {
        public override string GetValue(Firm f) => f.DateIn?.ToString("dd.MM.yyyy") ?? "";
        public override Field Clone() => new DateInField();
        public override Rule CreateRule() => new DateInRule(this);
    }
}

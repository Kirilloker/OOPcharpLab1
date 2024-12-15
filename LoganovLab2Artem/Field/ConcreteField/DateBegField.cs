using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;
using LoganovLab2Artem.Rules.ConcreteRules;

namespace LoganovLab2Artem.FieldSpace.ConcreteField
{
    public class DateBegField : Field
    {
        public override string GetValue(Firm f)
        {
            return f.DateIn?.ToString("dd.MM.yyyy") ?? "";
        }
        public override Field Clone() => new DateBegField();
        public override Rule CreateRule() => new DateBegRule(this);
    }

}

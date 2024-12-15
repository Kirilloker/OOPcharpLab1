using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.Rules;


namespace LoganovLab2Artem.FieldSpace
{
    public abstract class Field
    {
        public abstract string GetValue(Firm f);
        public abstract Field Clone();
        public abstract Rule CreateRule();
    }
}

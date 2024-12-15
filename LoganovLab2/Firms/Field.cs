using LoganovLab1.Domain;
using LoganovLab2.Rules;

namespace LoganovLab2.Firms
{
    public abstract class Field
    {
        public string FieldName { get; protected set; }

        public Field(string fieldName)
        {
            FieldName = fieldName;
        }

        public abstract string GetValue(Firm firm);
        public abstract Field Clone();
        public abstract FilterRule CreateRule();

        public abstract FieldDataType GetFieldDataType();
    }

    public enum FieldDataType
    {
        String,
        DateTime,
        Int
    }
}

using LoganovLab1.Domain;

namespace LoganovLab2.Firms
{
    public class FirmView
    {
        private List<Field> _fields = new List<Field>();

        public void AddField(Field field)
        {
            if (field != null)
                _fields.Add(field);
        }

        public Field[] GetFields()
        {
            return _fields.ToArray();
        }

        public Dictionary<Field, string> GetFieldValues(Firm firm)
        {
            return _fields.ToDictionary(f => f, f => f.GetValue(firm));
        }
    }
}

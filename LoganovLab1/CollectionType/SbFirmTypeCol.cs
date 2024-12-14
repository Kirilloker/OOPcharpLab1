using LoganovLab1.Type;

namespace LoganovLab1.CollectionType
{
    // Коллекция типов подразделений
    public class SbFirmTypeCol : IEnumerable<SbFirmType>
    {
        private List<SbFirmType> _types = new List<SbFirmType>();

        public void AddType(SbFirmType type)
        {
            if (type != null && !_types.Any(t => t.Name == type.Name))
                _types.Add(type);
        }

        public SbFirmType[] ToArray()
        {
            return _types.ToArray();
        }

        public SbFirmType GetMainOfficeType()
        {
            return _types.FirstOrDefault(t => t.IsMainOffice);
        }

        public SbFirmType GetByName(string name)
        {
            return _types.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerator<SbFirmType> GetEnumerator()
        {
            return _types.GetEnumerator(); 
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); 
        }
    }
}

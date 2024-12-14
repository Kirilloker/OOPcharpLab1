using LoganovLab1.Type;

namespace LoganovLab1.CollectionType
{
    // Коллекция типов подразделений
    public class SubFirmTypeCollection : IEnumerable<SubFirmType>
    {
        private List<SubFirmType> _types = new List<SubFirmType>();

        public void AddType(SubFirmType type)
        {
            if (type != null && !_types.Any(t => t.Name == type.Name))
                _types.Add(type);
        }

        public SubFirmType[] ToArray()
        {
            return _types.ToArray();
        }

        public SubFirmType GetMainOfficeType()
        {
            return _types.FirstOrDefault(t => t.IsMainOffice);
        }

        public SubFirmType GetByName(string name)
        {
            return _types.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerator<SubFirmType> GetEnumerator()
        {
            return _types.GetEnumerator(); 
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator(); 
        }
    }
}

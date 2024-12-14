using LoganovLab1.Type;
using System.Collections;

namespace LoganovLab1.CollectionType
{
    // Коллекция типов контактов
    public class ContTypeCol: IEnumerable<ContType>
    {
        private List<ContType> _types = new List<ContType>();

        public void AddType(ContType type)
        {
            if (type != null && !_types.Any(t => t.Name == type.Name))
                _types.Add(type);
        }

        public ContType[] ToArray()
        {
            return _types.ToArray();
        }

        public ContType GetTypeByName(string name)
        {
            return _types.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerator<ContType> GetEnumerator()
        {
            return _types.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
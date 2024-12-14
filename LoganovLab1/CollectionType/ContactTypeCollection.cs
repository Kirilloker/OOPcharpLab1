using LoganovLab1.Type;
using System.Collections;

namespace LoganovLab1.CollectionType
{
    // Коллекция типов контактов
    public class ContactTypeCollection: IEnumerable<ContactType>
    {
        private List<ContactType> _types = new List<ContactType>();

        public void AddType(ContactType type)
        {
            if (type != null && !_types.Any(t => t.Name == type.Name))
                _types.Add(type);
        }

        public ContactType[] ToArray()
        {
            return _types.ToArray();
        }

        public ContactType GetTypeByName(string name)
        {
            return _types.FirstOrDefault(t => t.Name == name);
        }

        public IEnumerator<ContactType> GetEnumerator()
        {
            return _types.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
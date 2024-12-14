namespace LoganovLab1
{
    // Коллекция типов контактов
    public class ContTypeCol
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
    }
}

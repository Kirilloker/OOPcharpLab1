namespace LoganovLab1Artem.ContactSpace
{
    public class ContTypeCol
    {
        private List<ContType> _lst;

        public int Count => _lst.Count;

        public ContType this[int index]
        {
            get => _lst[index];
            set => _lst[index] = value;
        }

        public ContTypeCol()
        {
            _lst = new List<ContType>();
        }

        public void Add(ContType type)
        {
            if (type != null && !_lst.Exists(t => t.Name == type.Name))
            {
                _lst.Add(type);
            }
        }

        public void Clear()
        {
            _lst.Clear();
        }

        public ContType[] ToArray()
        {
            return _lst.ToArray();
        }

        public ContType GetByName(string name)
        {
            return _lst.Find(type => type.Name == name);
        }
    }
}

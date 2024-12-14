using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab1Artem.SubFirmSpace
{
    // Коллекция типов подразделений фирмы
    public class SybFirmTypeCollection : IEnumerable<SubFirmType>
    {
        private List<SubFirmType> _types;

        public SybFirmTypeCollection()
        {
            _types = new List<SubFirmType>();
        }

        public void AddType(SubFirmType type)
        {
            if (type != null)
            {
                bool exists = false;
                foreach (SubFirmType t in _types)
                {
                    if (t.Name == type.Name)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    _types.Add(type);
                }
            }
        }

        public SubFirmType[] ToArray()
        {
            return _types.ToArray();
        }

        public SubFirmType GetMainOfficeType()
        {
            foreach (SubFirmType t in _types)
            {
                if (t.IsMainOffice)
                {
                    return t;
                }
            }
            return null;
        }

        public SubFirmType GetByName(string name)
        {
            foreach (SubFirmType t in _types)
            {
                if (t.Name == name)
                {
                    return t;
                }
            }
            return null;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab1Artem.ContactSpace
{
    public class ContTypeCol 
    {
        private List<ContType> _types;

        public ContTypeCol()
        {
            _types = new List<ContType>();
        }

        public void AddType(ContType type)
        {
            if (type != null)
            {
                bool exists = false;
                foreach (ContType t in _types)
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

        public ContType[] ToArray()
        {
            return _types.ToArray();
        }

        public ContType GetTypeByName(string name)
        {
            foreach (ContType t in _types)
            {
                if (t.Name == name)
                {
                    return t;
                }
            }
            return null;
        }
    }
}



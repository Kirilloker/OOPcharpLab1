using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab1
{
    // Коллекция типов подразделений
    public class SbFirmTypeCol
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
    }
}

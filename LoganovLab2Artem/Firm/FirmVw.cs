using LoganovLab1Artem.FirmSpace;
using LoganovLab2Artem.FieldSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.FirmSpace
{
    public class FirmVw
    {
        private List<Field> _fields;

        public FirmVw()
        {
            _fields = new List<Field>();
        }

        public void AddField(Field field)
        {
            _fields.Add(field);
        }

        public Field[] GetFields() => _fields.ToArray();

        public string[] GetFirmValues(Firm f)
        {
            return _fields.Select(fd => fd.GetValue(f)).ToArray();
        }

        public FirmVw Clone()
        {
            var newVw = new FirmVw();
            foreach (var f in _fields)
            {
                newVw.AddField(f.Clone());
            }
            return newVw;
        }
    }
}

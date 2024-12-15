using LoganovLab1Artem.FirmSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab2Artem.FirmSpace
{
    public class FirmMngr
    {
        private FirmVw _firmVw;
        private List<Firm> _firms;

        public FirmMngr(FirmVw firmVw, IEnumerable<Firm> firms)
        {
            _firmVw = firmVw;
            _firms = firms.ToList();
        }

        public FirmVw CurrentView => _firmVw;

        public Firm[] GetAllFirms() => _firms.ToArray();

        public void SetFirms(IEnumerable<Firm> firms)
        {
            _firms = firms.ToList();
        }

        public void SetView(FirmVw vw)
        {
            _firmVw = vw;
        }

        public string GetFieldName(int index)
        {
            var fields = _firmVw.GetFields();
            if (index >= 0 && index < fields.Length)
            {
                return fields[index].GetType().Name.Replace("Field", "");
            }
            return null;
        }
    }
}

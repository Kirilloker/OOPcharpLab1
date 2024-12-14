using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.SubFirmSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoganovLab1Artem.FirmSpace
{
    public class FirmFactory
    {
        private static FirmFactory _instance;
        public static FirmFactory Instance
        {
            get
            {
                if (_instance == null) _instance = new FirmFactory();
                return _instance;
            }
        }

        private string[] _userFieldNames = new string[] { "Field1", "Field2", "Field3", "Field4", "Field5" };
        public SybFirmTypeCollection SbFirmTypes { get; private set; }
        public ContTypeCol ContactTypes { get; private set; }

        private FirmFactory()
        {
            SbFirmTypes = new SybFirmTypeCollection();
            ContactTypes = new ContTypeCol();
        }

        public void SetUserFieldNames(string[] fieldNames)
        {
            if (fieldNames != null && fieldNames.Length == 5)
                _userFieldNames = fieldNames;
        }

        public Firm CreateFirm()
        {
            var firm = new Firm(_userFieldNames);

            // Создадим основное подразделение
            var mainOfficeType = SbFirmTypes.GetMainOfficeType();

            if (mainOfficeType != null)
            {
                var mainOffice = new SubFirm(mainOfficeType);
                firm.AddSubFirm(mainOffice);
            }

            return firm;
        }
    }
}

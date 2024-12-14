using LoganovLab1.CollectionType;
using LoganovLab1.Domain;

namespace LoganovLab1.Factory
{
    // Фабрика фирм (Singleton)
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
        public SubFirmTypeCollection SubFirmTypes { get; private set; }
        public ContactTypeCollection ContactTypes { get; private set; }

        private FirmFactory()
        {
            SubFirmTypes = new SubFirmTypeCollection();
            ContactTypes = new ContactTypeCollection();
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
            var mainOfficeType = SubFirmTypes.GetMainOfficeType();

            if (mainOfficeType != null)
            {
                var mainOffice = new SubFirm(mainOfficeType);
                firm.AddSubFirm(mainOffice);
            }

            return firm;
        }
    }
}

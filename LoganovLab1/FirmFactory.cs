namespace LoganovLab1
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
        public SbFirmTypeCol SbFirmTypes { get; private set; }
        public ContTypeCol ContactTypes { get; private set; }

        private FirmFactory()
        {
            SbFirmTypes = new SbFirmTypeCol();
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

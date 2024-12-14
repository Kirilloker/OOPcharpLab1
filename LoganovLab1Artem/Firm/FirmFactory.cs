using LoganovLab1Artem.SubFirmSpace;
using LoganovLab1Artem.ContactSpace;

namespace LoganovLab1Artem.FirmSpace
{
    public class FirmFactory
    {
        private string[] flds;
        private string NameMain;

        private static FirmFactory _instance;

        public static FirmFactory Instance
        {
            get
            {
                if (_instance == null) _instance = new FirmFactory();
                return _instance;
            }
        }

        // Коллекции типов подразделений и контактов
        public SbFirmTypeCol SbFirmTypes { get; private set; }
        public ContTypeCol ContactTypes { get; private set; }

        private FirmFactory()
        {
            flds = new string[] { "Field1", "Field2", "Field3", "Field4", "Field5" };
            SbFirmTypes = new SbFirmTypeCol();
            ContactTypes = new ContTypeCol();
            NameMain = "Основное подраздление";
        }

        public void SetFieldNames(string[] fieldNames)
        {
            if (fieldNames != null && fieldNames.Length == 5)
            {
                flds = fieldNames;
            }
        }

        public string[] GetFieldNames()
        {
            return flds;
        }

        public Firm Create()
        {
            var firm = new Firm(flds);

            if (!string.IsNullOrEmpty(NameMain))
            {
                // Создаем главное подразделение, если указано NameMain
                var mainOfficeType = SbFirmTypes.GetEnumerator().Current;
                if (mainOfficeType != null && mainOfficeType.IsMain)
                {
                    var mainOffice = new SubFirm(mainOfficeType);
                    firm.AddSbFirm(mainOffice);
                }
            }

            return firm;
        }
    }
}

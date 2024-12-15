using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.FirmSpace;
using LoganovLab1Artem.SubFirmSpace;
using LoganovLab2Artem.Controller;
using LoganovLab2Artem.FieldSpace.ConcreteField;
using LoganovLab2Artem.FirmSpace;
using LoganovLab2Artem.MyForm;

namespace LoganovLab2Artem
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            FirmFactory.Instance.SetFieldNames(new string[] { "Field1", "Field2", "Field3" });

            FirmFactory.Instance.SbFirmTypes.Add(new SbFirmType("Основной офис", true));
            FirmFactory.Instance.SbFirmTypes.Add(new SbFirmType("Отдел снабжения"));
            FirmFactory.Instance.SbFirmTypes.Add(new SbFirmType("Отдел продаж"));
            FirmFactory.Instance.SbFirmTypes.Add(new SbFirmType("Отдел логистики"));

            FirmFactory.Instance.ContactTypes.Add(new ContType("Звонок"));
            FirmFactory.Instance.ContactTypes.Add(new ContType("Письмо"));
            FirmFactory.Instance.ContactTypes.Add(new ContType("Коммерческое предложение"));

            var firmFactory = FirmFactory.Instance;
            var firms = new List<Firm>();

            var firmData = new List<(string Name, string Country, string Region, string Town, string PostInx, string Web)>
            {
                ("ООО 'Технологии будущего'", "Россия", "Москва", "Москва", "101000", "www.futuretech.ru"),
                ("АО 'Звездный путь'", "Россия", "Санкт-Петербург", "Санкт-Петербург", "190000", "www.stellarway.ru"),
                ("ЗАО 'Глобальные решения'", "Россия", "Казань", "Казань", "420000", "www.globalsolutions.ru"),
                ("ООО 'Инновации и прогресс'", "Россия", "Новосибирск", "Новосибирск", "630000", "www.innoprog.ru"),
                ("АО 'Северный ветер'", "Россия", "Архангельск", "Архангельск", "163000", "www.northwind.ru")
            };

            foreach (var (name, country, region, town, postInx, web) in firmData)
            {
                var firm = firmFactory.Create();
                firm.Name = name;
                firm.Country = country;
                firm.Region = region;
                firm.Town = town;
                firm.PostInx = postInx;
                firm.Web = web;
                firm.SetField("Field1", $"Значение для {name}");
                firm.SetField("Field2", $"Пользовательское значение для {name}");
                firm.SetField("Field3", $"Еще одно значение для {name}");
                firm.AddSbFirm(new SubFirm(FirmFactory.Instance.SbFirmTypes.GetByName("Основной офис"), "Основной офис"));
                firms.Add(firm);
            }

            var firmVw = new FirmVw();
            firmVw.AddField(new NameField());
            firmVw.AddField(new CountryField());
            firmVw.AddField(new RegionField());
            firmVw.AddField(new TownField());
            firmVw.AddField(new PostInxField());
            firmVw.AddField(new WebField());

            var firmMngr = new FirmMngr(firmVw, firms);

            var contTypeCol = new ContTypeCol();
            var mainContr = new MainContr(firmMngr, contTypeCol);

            var mainForm = new frmMain(mainContr);
            Application.Run(mainForm);
        }
    }

    public static class FirmListExtensions
    {
        public static List<Firm> FilterByRegion(this List<Firm> firmList, string region)
        {
            return firmList.Where(firm => firm.Region == region).ToList();
        }
    }
}

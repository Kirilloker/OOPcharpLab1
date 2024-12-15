using LoganovLab1.Domain;
using LoganovLab1.Factory;
using LoganovLab1.Type;
using LoganovLab2.Controller;
using LoganovLab2.Firms;

namespace LoganovLab2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            InitializeData();

            var firmVw = new FirmView();
            // Оставляем три исходных поля
            firmVw.AddField(new NameField());
            firmVw.AddField(new RegionField());
            firmVw.AddField(new TownField());
            // Заменяем остальные поля


            var allFirms = CreateSampleFirms();
            var firmMngr = new FirmManager(firmVw, allFirms);
            var mainContr = new MainController(firmMngr);

            Application.Run(new FormMain(mainContr));
        }

        static void InitializeData()
        {
            var f = FirmFactory.Instance;
            f.SubFirmTypes.AddType(new SubFirmType("MainOffice", true));
            f.SubFirmTypes.AddType(new SubFirmType("Branch1"));
            f.ContactTypes.AddType(new ContactType("Встреча", "Встретиться с клиентом"));
            f.ContactTypes.AddType(new ContactType("Звонок", "Позвонить клиенту"));
        }

        static Firm[] CreateSampleFirms()
        {
            var f = FirmFactory.Instance;

            Firm MakeFirm(string full, string reg, string city)
            {
                var firm = f.CreateFirm();
                firm.FullName = full;
                firm.Region = reg;
                firm.City = city;

                return firm;
            }

            return new[]
            {
        MakeFirm("ООО СеверСтрой", "Северо-Западный", "Архангельск"),
        MakeFirm("ООО УралТехно", "Уральский", "Екатеринбург"),
        MakeFirm("ООО ВолгаПром", "Приволжский", "Самара"),
        MakeFirm("ООО БайкалСервис", "Сибирский", "Иркутск"),
        MakeFirm("ООО КрымФлот", "Южный", "Севастополь")
    };
        }


    }
}

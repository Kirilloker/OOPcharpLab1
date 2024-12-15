using LoganovLab1.Domain;
using LoganovLab1.Factory;
using LoganovLab1.Type;
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
            // Добавляем больше полей
            firmVw.AddField(new NameField());
            firmVw.AddField(new RegionField());
            firmVw.AddField(new TownField());
            firmVw.AddField(new PostInxField());
            firmVw.AddField(new DateBegField());
            firmVw.AddField(new DateInField());
            firmVw.AddField(new WebField());

            var allFirms = CreateSampleFirms(10);
            var firmMngr = new FirmManager(firmVw, allFirms);
            var mainContr = new MainController(firmMngr);

            Application.Run(new FormMain(mainContr));
        }

        static void InitializeData()
        {
            var f = FirmFactory.Instance;
            f.SubFirmTypes.AddType(new SubFirmType("MainOffice", true));
            f.SubFirmTypes.AddType(new SubFirmType("Branch1"));
            f.ContactTypes.AddType(new ContactType("Call", "Phone Call"));
            f.ContactTypes.AddType(new ContactType("Meeting", "Personal Meeting"));
        }

        static Firm[] CreateSampleFirms(int count)
        {
            var f = FirmFactory.Instance;
            var rnd = new Random();

            Firm MakeFirm(string full, string reg, string city, DateTime dt, string idx, string dateIn, string web, string country, string usr, ContactType cType, DateTime cDate, string cDesc)
            {
                var firm = f.CreateFirm();
                firm.FullName = full;
                firm.Region = reg; firm.City = city;
                firm.InsertDate = dt;
                firm.SetUserFieldValue("Field1", idx);
                firm.SetUserFieldValue("Field2", dateIn);
                firm.SetUserFieldValue("Field3", country);
                firm.SetUserFieldValue("Field4", usr);
                firm.SetUserFieldValue("Field5", "Extra"); 

                firm.Website = web;
                firm.AddContactToMainOffice(new Contact { ContactType = cType, Date = cDate, Description = cDesc });
                return firm;
            }


            var names = new[] { "ООО Ромашка", "ООО Василёк", "ООО Тюльпан", "ООО Лилия", "ООО Подсолнух", "ООО Мак", "ООО Астра", "ООО Георгин", "ООО Пион", "ООО Хризантема" };
            var shortNames = new[] { "Ромашка", "Василёк", "Тюльпан", "Лилия", "Подсолнух", "Мак", "Астра", "Георгин", "Пион", "Хризантема" };
            var regions = new[] { "Центральный", "Северный", "Южный", "Западный", "Восточный", "Северо-Западный", "Дальний Восток", "Уральский", "Сибирский", "Приволжский" };
            var cities = new[] { "Москва", "Санкт-Петербург", "Нижний Новгород", "Калининград", "Нижний Новгород", "Мурманск", "Екатеринбург", "Новосибирск", "Казань", "Нижний Новгород" };
            var cTypes = new[] { f.ContactTypes.GetTypeByName("Call"), f.ContactTypes.GetTypeByName("Meeting") };

            return Enumerable.Range(1, count).Select(i =>
                MakeFirm(
                    names[i - 1],
                    regions[i - 1],
                    cities[i - 1],
                    new DateTime(2019 + i % 2, i, 10 + i),
                    (100 + i * 10).ToString(),
                    new DateTime(2020, i % 12 + 1, (i * 2) % 28 + 1).ToString("dd.MM.yyyy"),
                    $"http://site{i}.com",
                    "Russia",
                    $"UserVal{i}",
                    cTypes[i % 2],
                    new DateTime(2021, i % 12 + 1, (i * 3) % 28 + 1),
                    $"Контакт {i}"
                )
            ).ToArray();
        }
    }
}

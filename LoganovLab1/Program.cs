using LoganovLab1.Domain;
using LoganovLab1.Factory;
using LoganovLab1.List;
using LoganovLab1.Type;

namespace SunTechCorp
{
    class Program
    {
        static void Main(string[] args)
        {
            FirmFactory.Instance.SubFirmTypes.AddType(new SubFirmType("Главный офис", true));
            FirmFactory.Instance.SubFirmTypes.AddType(new SubFirmType("Отдел логистики"));
            FirmFactory.Instance.SubFirmTypes.AddType(new SubFirmType("Отдел продаж"));

            FirmFactory.Instance.ContactTypes.AddType(new ContactType("Информационное письмо", "Оповещение"));
            FirmFactory.Instance.ContactTypes.AddType(new ContactType("Деловое предложение", "Предложение сотрудничества"));

            FirmFactory.Instance.SetUserFieldNames(new string[] { "ClientPriority", "MarketCoverage", "CustomField1", "CustomField2", "CustomField3" });

            // Создаём несколько фирм с разными пользовательскими полями
            var firm1 = FirmFactory.Instance.CreateFirm();
            firm1.FullName = "ООО Сириус";
            firm1.ShortName = "Сириус";
            firm1.Region = "Санкт-Петербург";
            firm1.City = "Санкт-Петербург";
            firm1.Address = "ул. Космическая, д.42";
            firm1.CEO = "Александров А.А.";
            firm1.SetUserFieldValue("ClientPriority", "High");
            firm1.SetUserFieldValue("MarketCoverage", "35%");

            FirmFactory.Instance.SetUserFieldNames(new string[] { "ClientPriority", "EmployeeCount", "AnnualTurnover", "CustomField2", "CustomField3" });

            var firm2 = FirmFactory.Instance.CreateFirm();
            firm2.FullName = "ЗАО Вега";
            firm2.ShortName = "Вега";
            firm2.Region = "Санкт-Петербург";
            firm2.City = "Санкт-Петербург";
            firm2.Address = "пр. Мира, д.15";
            firm2.CEO = "Смирнов С.С.";
            firm2.SetUserFieldValue("ClientPriority", "");
            firm2.SetUserFieldValue("EmployeeCount", "200");
            firm2.SetUserFieldValue("AnnualTurnover", "1B RUB");

            FirmFactory.Instance.SetUserFieldNames(new string[] { "ClientPriority", "InvestmentCapacity", "PartnershipStage", "CustomField2", "CustomField3" });

            var firm3 = FirmFactory.Instance.CreateFirm();
            firm3.FullName = "ООО Альтаир";
            firm3.ShortName = "Альтаир";
            firm3.Region = "Москва";
            firm3.City = "Москва";
            firm3.Address = "ул. Звездная, д.9";
            firm3.CEO = "Кузнецов К.К.";
            firm3.SetUserFieldValue("ClientPriority", "Medium");
            firm3.SetUserFieldValue("InvestmentCapacity", "Moderate");
            firm3.SetUserFieldValue("PartnershipStage", "Negotiation");

            // Добавим подразделения для фирм
            var sbTypeLogistics = FirmFactory.Instance.SubFirmTypes.GetByName("Отдел логистики");
            if (sbTypeLogistics != null)
            {
                var logisticsDep = new SubFirm(sbTypeLogistics, "Логистика");
                firm1.AddSubFirm(logisticsDep);
            }

            // Создадим список всех фирм
            var allFirms = new FirmList();
            allFirms.AddFirm(firm1);
            allFirms.AddFirm(firm2);
            allFirms.AddFirm(firm3);

            Console.WriteLine("Список всех фирм:");
            allFirms.DeepPrint();

            // Создадим прототип контакта "Информационное письмо"
            var infoLetterType = FirmFactory.Instance.ContactTypes.GetTypeByName("Информационное письмо");
            var infoLetterPrototype = new Contact
            {
                ContactType = infoLetterType,
                Date = DateTime.Now,
                Description = "Отправлено приветственное письмо"
            };

            // Отфильтруем фирмы из Санкт-Петербурга
            var spbFirms = allFirms.FilterByRegion("Санкт-Петербург");

            // Добавим контакт всем питерским фирмам в главный офис
            spbFirms.AddContactToAllFirms(infoLetterPrototype);

            Console.WriteLine("Добавим контакт 'Информационное письмо' всем фирмам из Санкт-Петербурга в главный офис");
            Console.WriteLine("Фирмы из Санкт-Петербурга после добавления контакта:");
            spbFirms.DeepPrint();

            // Добавим контакт "Деловое предложение" во все фирмы, у которых есть отдел логистики
            var businessOfferType = FirmFactory.Instance.ContactTypes.GetTypeByName("Деловое предложение");
            var businessOfferPrototype = new Contact
            {
                ContactType = businessOfferType,
                Date = DateTime.Now,
                Description = "Отправлено деловое предложение"
            };

            var firmsWithLogistics = allFirms.FilterBySubFirmType("Отдел логистики");
            firmsWithLogistics.AddContactToAllFirmsWithSubFirmType(businessOfferPrototype, "Отдел логистики", true);

            Console.WriteLine("Добавим контакт 'Деловое предложение' всем фирмам с отделом логистики");
            Console.WriteLine("Фирмы с отделом логистики после добавления контакта:");
            firmsWithLogistics.DeepPrint();

            Console.WriteLine("Конец программы");
            Console.WriteLine("Вывод всех фирм");
            allFirms.DeepPrint();

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}

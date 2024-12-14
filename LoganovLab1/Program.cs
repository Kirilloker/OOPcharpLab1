using LoganovLab1.Domain;
using LoganovLab1.Factory;
using LoganovLab1.List;
using LoganovLab1.Type;

namespace LoganovLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация типов подразделений
            FirmFactory.Instance.SubFirmTypes.AddType(new SubFirmType("Основной офис", true));
            FirmFactory.Instance.SubFirmTypes.AddType(new SubFirmType("Отдел снабжения"));
            FirmFactory.Instance.SubFirmTypes.AddType(new SubFirmType("Отдел маркетинга"));

            // Инициализация типов контактов
            FirmFactory.Instance.ContactTypes.AddType(new ContactType("Письмо послали", "Информационное письмо"));
            FirmFactory.Instance.ContactTypes.AddType(new ContactType("Коммерческое предложение", "Предложение о сотрудничестве"));

            // Настройка пользовательских полей для каждой фирмы
            FirmFactory.Instance.SetUserFieldNames(new string[] { "SpecialMark", "MarketShare", "Field3", "Field4", "Field5" });

            // Создаём несколько фирм с разными пользовательскими полями
            var firm1 = FirmFactory.Instance.CreateFirm();
            firm1.FullName = "ООО Ромашка";
            firm1.ShortName = "Ромашка";
            firm1.Region = "Нижний Новгород";
            firm1.City = "Н.Новгород";
            firm1.Address = "ул. Цветочная, д.1";
            firm1.CEO = "Иванов И.И.";
            firm1.SetUserFieldValue("SpecialMark", "VIP");
            firm1.SetUserFieldValue("MarketShare", "25%");

            FirmFactory.Instance.SetUserFieldNames(new string[] { "SpecialMark", "EmployeeCount", "Revenue", "Field4", "Field5" });

            var firm2 = FirmFactory.Instance.CreateFirm();
            firm2.FullName = "ЗАО Ландыш";
            firm2.ShortName = "Ландыш";
            firm2.Region = "Нижний Новгород";
            firm2.City = "Н.Новгород";
            firm2.Address = "пр. Весенний, д.10";
            firm2.CEO = "Петров П.П.";
            firm2.SetUserFieldValue("SpecialMark", "");
            firm2.SetUserFieldValue("EmployeeCount", "150");
            firm2.SetUserFieldValue("Revenue", "500M RUB");

            FirmFactory.Instance.SetUserFieldNames(new string[] { "SpecialMark", "InvestmentPotential", "PartnershipStatus", "Field4", "Field5" });

            var firm3 = FirmFactory.Instance.CreateFirm();
            firm3.FullName = "ООО Одуванчик";
            firm3.ShortName = "Одуванчик";
            firm3.Region = "Москва";
            firm3.City = "Москва";
            firm3.Address = "ул. Садовая, д.3";
            firm3.CEO = "Сидоров С.С.";
            firm3.SetUserFieldValue("SpecialMark", "Silver");
            firm3.SetUserFieldValue("InvestmentPotential", "High");
            firm3.SetUserFieldValue("PartnershipStatus", "В разработке");

            // Добавим подразделения для фирм
            var sbTypeSupply = FirmFactory.Instance.SubFirmTypes.GetByName("Отдел снабжения");
            if (sbTypeSupply != null)
            {
                var supplyDep = new SubFirm(sbTypeSupply, "Снабжение");
                firm1.AddSubFirm(supplyDep);
            }

            // Создадим список всех фирм
            var allFirms = new FirmList();
            allFirms.AddFirm(firm1);
            allFirms.AddFirm(firm2);
            allFirms.AddFirm(firm3);

            Console.WriteLine("Список всех фирм:");
            allFirms.DeepPrint();

            // Создадим прототип контакта "Письмо послали"
            var mailContactType = FirmFactory.Instance.ContactTypes.GetTypeByName("Письмо послали");
            var mailPrototype = new Contact
            {
                ContactType = mailContactType,
                Date = DateTime.Now,
                Description = "Отправлено приветственное письмо"
            };

            // Отфильтруем фирмы из Нижнего Новгорода
            var nnFirms = allFirms.FilterByRegion("Нижний Новгород");

            // Добавим контакт всем нижегородским фирмам в основной офис
            nnFirms.AddContactToAllFirms(mailPrototype);

            Console.WriteLine("Создадим прототип контакта Письмо послали и добавим всем нижегородским фирмам в основной офис");
            Console.WriteLine("Выведем фирмы которых коснулось изменение");
            nnFirms.DeepPrint();

            // Добавим контакт "Коммерческое предложение" во все фирмы, у которых есть отдел снабжения
            var commOfferType = FirmFactory.Instance.ContactTypes.GetTypeByName("Коммерческое предложение");
            var commOfferPrototype = new Contact
            {
                ContactType = commOfferType,
                Date = DateTime.Now,
                Description = "Отправлено КП"
            };

            var firmsWithSupply = allFirms.FilterBySubFirmType("Отдел снабжения");
            firmsWithSupply.AddContactToAllFirmsWithSubFirmType(commOfferPrototype, "Отдел снабжения", true);

            Console.WriteLine("Добавим в фирмы с отделом снабжения контакт коммерческое предложение");
            Console.WriteLine("Фирмы с 'Отдел снабжения' после добавления 'Коммерческое предложение':");
            firmsWithSupply.DeepPrint();

            Console.WriteLine("Конец программы");
            Console.WriteLine("Вывод всех фирм");
            allFirms.DeepPrint();

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}

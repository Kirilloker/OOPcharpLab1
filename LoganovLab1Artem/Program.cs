using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.FirmSpace;
using LoganovLab1Artem.SubFirmSpace;

namespace LoganovLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Устанавливаем пользовательские названия полей для фабрики фирм
            FirmFactory.Instance.SetUserFieldNames(new string[] { "Госпрограмма", "Объем закупок", "Признак 1", "Признак 2", "Признак 3" });

            // Добавляем типы подразделений и типы контактов
            FirmFactory.Instance.SbFirmTypes.AddType(new SubFirmType("Основной офис", true));
            FirmFactory.Instance.SbFirmTypes.AddType(new SubFirmType("Отдел снабжения"));
            FirmFactory.Instance.ContactTypes.AddType(new ContType("Звонок"));
            FirmFactory.Instance.ContactTypes.AddType(new ContType("Письмо"));
            FirmFactory.Instance.ContactTypes.AddType(new ContType("Коммерческое предложение"));

            // Создаем 4 фирмы
            Firm firm1 = FirmFactory.Instance.CreateFirm();
            firm1.FullName = "ООО 'Первый клиент'";
            firm1.ShortName = "Первый";
            firm1.Region = "Москва";
            firm1.City = "Москва";

            Firm firm2 = FirmFactory.Instance.CreateFirm();
            firm2.FullName = "АО 'Второй клиент'";
            firm2.ShortName = "Второй";
            firm2.Region = "Санкт-Петербург";
            firm2.City = "Санкт-Петербург";

            Firm firm3 = FirmFactory.Instance.CreateFirm();
            firm3.FullName = "ЗАО 'Третий клиент'";
            firm3.ShortName = "Третий";
            firm3.Region = "Нижний Новгород";
            firm3.City = "Нижний Новгород";

            Firm firm4 = FirmFactory.Instance.CreateFirm();
            firm4.FullName = "ООО 'Четвертый клиент'";
            firm4.ShortName = "Четвертый";
            firm4.Region = "Нижний Новгород";
            firm4.City = "Нижний Новгород";

            // Добавляем фирмы в общий список
            FirmList firmList = new FirmList();
            firmList.AddFirm(firm1);
            firmList.AddFirm(firm2);
            firmList.AddFirm(firm3);
            firmList.AddFirm(firm4);

            // Демонстрация работы фильтрации по региону
            Console.WriteLine("\n--- Список фирм в регионе 'Нижний Новгород' ---");
            FirmList firmsInNizhny = firmList.FilterByRegion("Нижний Новгород");
            foreach (Firm firm in firmsInNizhny.ToArray())
            {
                Console.WriteLine(firm);
            }

            // Демонстрация добавления контакта "Письмо послали" всем фирмам из Нижнего Новгорода
            Console.WriteLine("\n--- Добавление контакта 'Письмо послали' всем фирмам из Нижнего Новгорода ---");
            Contact contact = new Contact
            {
                ContactType = FirmFactory.Instance.ContactTypes.GetTypeByName("Письмо"),
                Description = "Письмо послали",
                Date = DateTime.Now
            };
            firmsInNizhny.AddContactToAllFirms(contact);

            // Отобразим все контакты фирм из Нижнего Новгорода
            Console.WriteLine("\n--- Список контактов фирм из региона 'Нижний Новгород' ---");
            foreach (Firm firm in firmsInNizhny.ToArray())
            {
                Console.WriteLine("\nФирма: " + firm.FullName);
                foreach (Contact c in firm.GetAllContacts())
                {
                    Console.WriteLine($"Контакт: {c.ContactType.Name}, Описание: {c.Description}, Дата: {c.Date}");
                }
            }

            // Демонстрация добавления контакта "Коммерческое предложение" для всех фирм с подразделением "Отдел снабжения"
            Console.WriteLine("\n--- Добавление контакта 'Коммерческое предложение' фирмам с 'Отдел снабжения' ---");
            Contact commercialOffer = new Contact
            {
                ContactType = FirmFactory.Instance.ContactTypes.GetTypeByName("Коммерческое предложение"),
                Description = "Отправлено коммерческое предложение",
                Date = DateTime.Now
            };
            firmList.AddContactToAllFirmsWithSubFirmType(commercialOffer, "Отдел снабжения", true);

            // Отобразим все контакты для всех фирм
            Console.WriteLine("\n--- Список всех фирм и их контактов ---");
            foreach (Firm firm in firmList.ToArray())
            {
                Console.WriteLine("\nФирма: " + firm.FullName);
                foreach (Contact c in firm.GetAllContacts())
                {
                    Console.WriteLine($"Контакт: {c.ContactType.Name}, Описание: {c.Description}, Дата: {c.Date}");
                }
            }

            Console.WriteLine("\n--- Работа программы завершена ---");
        }
    }
}

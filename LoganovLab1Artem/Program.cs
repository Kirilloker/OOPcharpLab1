using System;
using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.FirmSpace;
using LoganovLab1Artem.SubFirmSpace;

namespace LoganovLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Настраиваем пользовательские поля для фабрики фирм
            FirmFactory.Instance.SetFieldNames(new string[] { "Госпрограмма", "Объем закупок", "Признак 1", "Признак 2", "Признак 3" });

            // Добавляем типы подразделений
            FirmFactory.Instance.SbFirmTypes.Add(new SubFirmType("Основной офис", true));
            FirmFactory.Instance.SbFirmTypes.Add(new SubFirmType("Отдел снабжения"));
            FirmFactory.Instance.SbFirmTypes.Add(new SubFirmType("Отдел продаж"));
            FirmFactory.Instance.SbFirmTypes.Add(new SubFirmType("Отдел логистики"));

            // Добавляем типы контактов
            FirmFactory.Instance.ContactTypes.Add(new ContType("Звонок"));
            FirmFactory.Instance.ContactTypes.Add(new ContType("Письмо"));
            FirmFactory.Instance.ContactTypes.Add(new ContType("Коммерческое предложение"));

            // Создаем 4 фирмы с разным количеством подразделений
            Firm firm1 = CreateFirm("ООО 'Первый клиент'", "Москва", "Москва", new[] { "Отдел снабжения", "Отдел продаж" }); // 3 подразделения
            Firm firm2 = CreateFirm("АО 'Второй клиент'", "Санкт-Петербург", "Санкт-Петербург", new[] { "Отдел продаж" }); // 1 подразделение
            Firm firm3 = CreateFirm("ЗАО 'Третий клиент'", "Нижний Новгород", "Нижний Новгород", new[] { "Отдел снабжения", "Отдел продаж", "Отдел логистики" }); // 4 подразделения
            Firm firm4 = CreateFirm("ООО 'Четвертый клиент'", "Нижний Новгород", "Нижний Новгород", new[] { "Отдел логистики" }); // 2 подразделения

            // Создаем список фирм
            FirmList firmList = new FirmList();
            firmList.AddFirm(firm1);
            firmList.AddFirm(firm2);
            firmList.AddFirm(firm3);
            firmList.AddFirm(firm4);

            // Фильтрация фирм по региону "Нижний Новгород"
            Console.WriteLine("\n--- Фирмы в регионе 'Нижний Новгород' ---");
            FirmList firmsInNizhny = firmList.FilterByRegion("Нижний Новгород");
            foreach (Firm firm in firmsInNizhny.ToArray())
            {
                Console.WriteLine(firm.Name);
            }

            // Добавление контакта "Письмо" всем фирмам из Нижнего Новгорода
            Contact letterContact = new Contact
            {
                CntType = FirmFactory.Instance.ContactTypes.GetByName("Письмо"),
                Descr = "Письмо отправлено",
                BeginDt = DateTime.Now
            };
            firmsInNizhny.AddContactToAllFirms(letterContact);

            // Отображение контактов фирм из Нижнего Новгорода
            Console.WriteLine("\n--- Контакты фирм из Нижнего Новгорода ---");
            foreach (Firm firm in firmsInNizhny.ToArray())
            {
                Console.WriteLine($"\nФирма: {firm.Name}");
                foreach (Contact contact in firm.GetAllContacts())
                {
                    Console.WriteLine($"Контакт: {contact.CntType.Name}, Описание: {contact.Descr}, Дата: {contact.BeginDt}");
                }
            }

            // Добавление контакта "Коммерческое предложение" фирмам с подразделением "Отдел снабжения"
            Console.WriteLine("\n--- Добавление 'Коммерческое предложение' фирмам с подразделением 'Отдел снабжения' ---");
            Contact offerContact = new Contact
            {
                CntType = FirmFactory.Instance.ContactTypes.GetByName("Коммерческое предложение"),
                Descr = "Отправлено коммерческое предложение",
                BeginDt = DateTime.Now
            };
            firmList.AddContactToAllFirmsWithSubFirmType(offerContact, "Отдел снабжения");

            // Отображение всех контактов всех фирм
            Console.WriteLine("\n--- Все фирмы и их контакты ---");
            foreach (Firm firm in firmList.ToArray())
            {
                Console.WriteLine($"\nФирма: {firm.Name}");
                foreach (Contact contact in firm.GetAllContacts())
                {
                    Console.WriteLine($"Контакт: {contact.CntType.Name}, Описание: {contact.Descr}, Дата: {contact.BeginDt}");
                }
            }

            // Отображение всех подразделений всех фирм
            Console.WriteLine("\n--- Все фирмы и их подразделения ---");
            foreach (Firm firm in firmList.ToArray())
            {
                Console.WriteLine($"\nФирма: {firm.Name}");
                var subFirms = firm.GetAllSubFirms();
                Console.WriteLine($"  Подразделения ({subFirms.Length}):");
                foreach (var subFirm in subFirms)
                {
                    Console.WriteLine($"  - {subFirm.Name} ({(subFirm.IsMain ? "Главное" : "Второстепенное")})");
                }
            }

            Console.WriteLine("\n--- Программа завершена ---");
        }


        static Firm CreateFirm(string name, string region, string town, string[] subFirmTypes)
        {
            Firm firm = FirmFactory.Instance.Create();
            firm.Name = name;
            firm.Region = region;
            firm.Town = town;

            // Добавляем указанные подразделения к фирме
            foreach (string subFirmTypeName in subFirmTypes)
            {
                var subFirmType = FirmFactory.Instance.SbFirmTypes.GetByName(subFirmTypeName);
                if (subFirmType != null)
                {
                    firm.AddSbFirm(new SubFirm(subFirmType, subFirmTypeName));
                }
            }

            return firm;
        }
    }
}

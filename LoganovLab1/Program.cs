namespace LoganovLab1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Инициализация типов подразделений
            FirmFactory.Instance.SbFirmTypes.AddType(new SbFirmType("Основной офис", true));
            FirmFactory.Instance.SbFirmTypes.AddType(new SbFirmType("Отдел снабжения"));
            FirmFactory.Instance.SbFirmTypes.AddType(new SbFirmType("Отдел маркетинга"));

            // Инициализация типов контактов
            FirmFactory.Instance.ContactTypes.AddType(new ContType("Письмо послали", "Информационное письмо"));
            FirmFactory.Instance.ContactTypes.AddType(new ContType("Коммерческое предложение", "Предложение о сотрудничестве"));

            // Настройка пользовательских полей
            FirmFactory.Instance.SetUserFieldNames(new string[] { "SpecialMark", "ProgramParticipation", "Volume", "FlagX", "FlagY" });

            // Создаём несколько фирм
            var firm1 = FirmFactory.Instance.CreateFirm();
            firm1.FullName = "ООО Ромашка";
            firm1.ShortName = "Ромашка";
            firm1.Region = "Нижний Новгород";
            firm1.City = "Н.Новгород";
            firm1.Address = "ул. Цветочная, д.1";
            firm1.CEO = "Иванов И.И.";
            firm1.SetUserFieldValue("SpecialMark", "VIP");
            // Добавим подразделение отдел снабжения
            var sbTypeSupply = FirmFactory.Instance.SbFirmTypes.GetByName("Отдел снабжения");
            if (sbTypeSupply != null)
            {
                var supplyDep = new SubFirm(sbTypeSupply, "Снабжение");
                firm1.AddSubFirm(supplyDep);
            }

            var firm2 = FirmFactory.Instance.CreateFirm();
            firm2.FullName = "ЗАО Ландыш";
            firm2.ShortName = "Ландыш";
            firm2.Region = "Нижний Новгород";
            firm2.City = "Н.Новгород";
            firm2.Address = "пр. Весенний, д.10";
            firm2.CEO = "Петров П.П.";

            var firm3 = FirmFactory.Instance.CreateFirm();
            firm3.FullName = "ООО Одуванчик";
            firm3.ShortName = "Одуванчик";
            firm3.Region = "Москва";
            firm3.City = "Москва";
            firm3.Address = "ул. Садовая, д.3";
            firm3.CEO = "Сидоров С.С.";

            // Создадим список всех фирм
            var allFirms = new FirmList();
            allFirms.AddFirm(firm1);
            allFirms.AddFirm(firm2);
            allFirms.AddFirm(firm3);

            // Отфильтруем фирмы из Нижнего Новгорода
            var nnFirms = allFirms.FilterByRegion("Нижний Новгород");

            // Создадим прототип контакта "Письмо послали"
            var mailContactType = FirmFactory.Instance.ContactTypes.GetTypeByName("Письмо послали");
            var mailPrototype = new Contact
            {
                ContactType = mailContactType,
                Date = DateTime.Now,
                Description = "Отправлено приветственное письмо"
            };

            // Добавим контакт всем нижегородским фирмам в основной офис
            nnFirms.AddContactToAllFirms(mailPrototype);

            // Проверим результат
            Console.WriteLine("Фирмы из Н.Новгорода после добавления контакта 'Письмо послали':");
            foreach (var f in nnFirms.ToArray())
            {
                Console.WriteLine(f);
                var contacts = f.GetAllContacts();
                foreach (var c in contacts)
                {
                    Console.WriteLine($"   Контакт: {c.ContactType.Name}, {c.Date}, {c.Description}");
                }
            }

            Console.WriteLine();

            // Теперь добавим контакт "Коммерческое предложение" во все фирмы, у которых есть отдел снабжения
            var commOfferType = FirmFactory.Instance.ContactTypes.GetTypeByName("Коммерческое предложение");
            var commOfferPrototype = new Contact
            {
                ContactType = commOfferType,
                Date = DateTime.Now,
                Description = "Отправлено КП"
            };

            // Фильтруем все фирмы по наличию отдела снабжения
            var firmsWithSupply = allFirms.FilterBySubFirmType("Отдел снабжения");
            firmsWithSupply.AddContactToAllFirmsWithSubFirmType(commOfferPrototype, "Отдел снабжения", true);

            Console.WriteLine("Фирмы с 'Отдел снабжения' после добавления 'Коммерческое предложение':");
            foreach (var f in firmsWithSupply.ToArray())
            {
                Console.WriteLine(f);
                var contacts = f.GetAllContacts();
                foreach (var c in contacts)
                {
                    Console.WriteLine($"   Контакт: {c.ContactType.Name}, {c.Date}, {c.Description}");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}

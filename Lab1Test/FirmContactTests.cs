//using NUnit.Framework;
//using LoganovLab1.Domain;
//using LoganovLab1.Type;
//using System;

//namespace Lab1Test
//{
//    [TestFixture]
//    public class FirmContactTests
//    {
//        private Firm _firm;

//        [SetUp]
//        public void Setup()
//        {
//            // Создаем объект Firm с набором пользовательских полей
//            _firm = new Firm(new string[] { "Field1", "Field2", "Field3", "Field4", "Field5" });
//        }

//        [Test]
//        public void AddContactToMainOffice_ShouldAddContact_WhenMainOfficeExists()
//        {
//            // Arrange
//            var mainOfficeType = new SubFirmType("Main Office", isMainOffice: true);
//            var mainOffice = new SubFirm(mainOfficeType);
//            _firm.AddSubFirm(mainOffice);
//            var contact = new Contact
//            {
//                ContactType = new ContactType("Email"),
//                Date = DateTime.Now,
//                Description = "Primary contact"
//            };

//            // Act
//            _firm.AddContactToMainOffice(contact);

//            // Assert
//            var contacts = mainOffice.GetContacts();
//            Assert.That(contacts.Length, Is.EqualTo(1), "Ожидается, что контакт будет добавлен в основной офис.");
//            Assert.That(contacts[0].Description, Is.EqualTo("Primary contact"), "Ожидается, что описание контакта будет корректным.");
//        }

//        [Test]
//        public void AddContactToMainOffice_ShouldIgnoreContact_WhenMainOfficeDoesNotExist()
//        {
//            // Arrange
//            var contact = new Contact
//            {
//                ContactType = new ContactType("Phone"),
//                Date = DateTime.Now,
//                Description = "Backup contact"
//            };

//            // Act
//            _firm.AddContactToMainOffice(contact);

//            // Assert
//            var allContacts = _firm.GetAllContacts();
//            Assert.That(allContacts.Length, Is.EqualTo(0), "Ожидается, что контакт не будет добавлен, так как основной офис отсутствует.");
//        }

//        [Test]
//        public void AddContactToMainOffice_ShouldIgnoreNullContact()
//        {
//            // Arrange
//            var mainOfficeType = new SubFirmType("Main Office", isMainOffice: true);
//            var mainOffice = new SubFirm(mainOfficeType);
//            _firm.AddSubFirm(mainOffice);

//            // Act
//            _firm.AddContactToMainOffice(null);

//            // Assert
//            var contacts = mainOffice.GetContacts();
//            Assert.That(contacts.Length, Is.EqualTo(0), "Ожидается, что null-контакт не будет добавлен.");
//        }

//        [Test]
//        public void GetAllContacts_ShouldReturnContactsFromAllSubFirms()
//        {
//            // Arrange
//            var subFirm1 = new SubFirm(new SubFirmType("Sales"));
//            var subFirm2 = new SubFirm(new SubFirmType("Support"));
//            _firm.AddSubFirm(subFirm1);
//            _firm.AddSubFirm(subFirm2);

//            var contact1 = new Contact
//            {
//                ContactType = new ContactType("Email"),
//                Date = DateTime.Now,
//                Description = "Sales contact"
//            };

//            var contact2 = new Contact
//            {
//                ContactType = new ContactType("Phone"),
//                Date = DateTime.Now,
//                Description = "Support contact"
//            };

//            subFirm1.AddContact(contact1);
//            subFirm2.AddContact(contact2);

//            // Act
//            var allContacts = _firm.GetAllContacts();

//            // Assert
//            Assert.That(allContacts.Length, Is.EqualTo(2), "Ожидается, что будут возвращены два контакта.");
//            Assert.That(allContacts[0].Description, Is.EqualTo("Sales contact"));
//            Assert.That(allContacts[1].Description, Is.EqualTo("Support contact"));
//        }

//        [Test]
//        public void GetAllContacts_ShouldReturnEmptyArray_WhenNoContactsExist()
//        {
//            // Act
//            var allContacts = _firm.GetAllContacts();

//            // Assert
//            Assert.That(allContacts.Length, Is.EqualTo(0), "Ожидается, что массив будет пустым, так как нет контактов.");
//        }

//        [Test]
//        public void AddDuplicateContact_ShouldAllowDuplicates()
//        {
//            // Arrange
//            var mainOfficeType = new SubFirmType("Main Office", isMainOffice: true);
//            var mainOffice = new SubFirm(mainOfficeType);
//            _firm.AddSubFirm(mainOffice);

//            var contact = new Contact
//            {
//                ContactType = new ContactType("Email"),
//                Date = DateTime.Now,
//                Description = "Duplicate contact"
//            };

//            // Act
//            _firm.AddContactToMainOffice(contact);
//            _firm.AddContactToMainOffice(contact);

//            // Assert
//            var contacts = mainOffice.GetContacts();
//            Assert.That(contacts.Length, Is.EqualTo(2), "Ожидается, что дублирующиеся контакты будут добавлены.");
//        }
//    }
//}

using LoganovLab1.Domain;
using LoganovLab1.Factory;
using LoganovLab1.List;
using LoganovLab1.Type;

namespace Lab1Test
{
    [TestFixture]
    public class ModuleTest
    {
        private Firm _firm;

        [SetUp]
        public void Setup()
        {
            _firm = new Firm(new string[] { "Field1", "Field2", "Field3", "Field4", "Field5" });
            FirmFactory.Instance.SetUserFieldNames(new[] { "Field1", "Field2", "Field3", "Field4", "Field5" });
            FirmFactory.Instance.SubFirmTypes.AddType(new SubFirmType("Main Office", true));
            FirmFactory.Instance.ContactTypes.AddType(new ContactType("Письмо послали"));
            FirmFactory.Instance.ContactTypes.AddType(new ContactType("Коммерческое предложение"));
        }

        [Test]
        public void Test_Firm_Creation_By_Factory()
        {
            var firm = FirmFactory.Instance.CreateFirm();
            Assert.That(firm, Is.Not.Null);
            Assert.That(firm.GetMainOffice(), Is.Not.Null);
            Assert.That(firm.GetMainOffice().SubFirmType.Name, Is.EqualTo("Main Office"));
        }

        [Test]
        public void Test_Creation_Of_Nizhny_Novgorod_Firms_List()
        {
            var firm1 = new Firm(new[] { "Field1", "Field2", "Field3", "Field4", "Field5" }) { Region = "Нижегородская область" };
            var firm2 = new Firm(new[] { "Field1", "Field2", "Field3", "Field4", "Field5" }) { Region = "Нижегородская область" };
            var firm3 = new Firm(new[] { "Field1", "Field2", "Field3", "Field4", "Field5" }) { Region = "Москва" };
            var firmList = new FirmList(new[] { firm1, firm2, firm3 });

            var nizhnyNovgorodFirms = firmList.FilterByRegion("Нижегородская область").ToArray();
            Assert.That(nizhnyNovgorodFirms.Length, Is.EqualTo(2));
            Assert.That(nizhnyNovgorodFirms.All(f => f.Region == "Нижегородская область"));
        }

        [Test]
        public void Test_Adding_Letter_Sent_Contact_To_Group_Of_Firms()
        {
            var firm1 = FirmFactory.Instance.CreateFirm();
            var firm2 = FirmFactory.Instance.CreateFirm();
            var firmList = new FirmList(new[] { firm1, firm2 });
            var contactType = FirmFactory.Instance.ContactTypes.GetTypeByName("Письмо послали");
            var contact = new Contact { ContactType = contactType, Date = DateTime.Now, Description = "Тестовое письмо" };

            firmList.AddContactToAllFirms(contact);

            foreach (var firm in firmList.ToArray())
            {
                var mainOffice = firm.GetMainOffice();
                Assert.That(mainOffice, Is.Not.Null);
                var contacts = mainOffice.GetContacts();
                Assert.That(contacts.Any(c => c.ContactType.Name == "Письмо послали"));
            }
        }

        [Test]
        public void Test_Adding_Commercial_Offer_Contact_To_Firms_With_Supply_Department()
        {
            var supplySubFirmType = new SubFirmType("Отдел снабжения");
            FirmFactory.Instance.SubFirmTypes.AddType(supplySubFirmType);

            var firm1 = new Firm(new[] { "Field1", "Field2", "Field3", "Field4", "Field5" });
            var firm2 = new Firm(new[] { "Field1", "Field2", "Field3", "Field4", "Field5" });
            firm1.AddSubFirm(new SubFirm(supplySubFirmType));

            var firmList = new FirmList(new[] { firm1, firm2 });

            var contactType = FirmFactory.Instance.ContactTypes.GetTypeByName("Коммерческое предложение");
            var contact = new Contact { ContactType = contactType, Date = DateTime.Now, Description = "Тестовое коммерческое предложение" };

            firmList.AddContactToAllFirmsWithSubFirmType(contact, "Отдел снабжения");

            var firm1SubFirms = firm1.GetAllSubFirms();
            var supplySubFirm = firm1SubFirms.FirstOrDefault(sf => sf.SubFirmType.Name == "Отдел снабжения");
            Assert.That(supplySubFirm, Is.Not.Null);
            var contacts = supplySubFirm.GetContacts();
            Assert.That(contacts.Any(c => c.ContactType.Name == "Коммерческое предложение"));

            var firm2SubFirms = firm2.GetAllSubFirms();
            var hasContact = firm2SubFirms.Any(sf => sf.GetContacts().Any(c => c.ContactType.Name == "Коммерческое предложение"));
            Assert.That(hasContact, Is.False);
        }
    }
}

using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.FirmSpace;
using LoganovLab1Artem.SubFirmSpace;

[TestFixture]
public class FirmTests
{
    [Test]
    public void AddCont_ShouldAddContactToFirstSubFirm_WhenSubFirmExists()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" });
        var subFirm = new SubFirm(new SubFirmType("Type1"));
        firm.AddSbFirm(subFirm);
        var contact = new Contact { Descr = "New Contact" };

        firm.AddCont(contact);

        Assert.That(subFirm.GetContacts().Length, Is.EqualTo(1));
        Assert.That(subFirm.GetContacts()[0].Descr, Is.EqualTo("New Contact"));
    }

    [Test]
    public void AddContToSbFirm_ShouldAddContactToSpecificSubFirm()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" });
        var subFirm1 = new SubFirm(new SubFirmType("Type1"));
        var subFirm2 = new SubFirm(new SubFirmType("Type2"));
        firm.AddSbFirm(subFirm1);
        firm.AddSbFirm(subFirm2);
        var contact = new Contact { Descr = "Special Contact" };

        firm.AddContToSbFirm(subFirm2, contact);

        Assert.That(subFirm2.GetContacts().Length, Is.EqualTo(1));
        Assert.That(subFirm2.GetContacts()[0].Descr, Is.EqualTo("Special Contact"));
    }

    [Test]
    public void AddSbFirm_ShouldAddSubFirmToFirm()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" });
        var subFirm = new SubFirm(new SubFirmType("Type1"));

        firm.AddSbFirm(subFirm);

        Assert.That(firm.GetAllSubFirms().Length, Is.EqualTo(1));
    }

    [Test]
    public void GetAllSubFirms_ShouldReturnAllSubFirms()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" });
        var subFirm1 = new SubFirm(new SubFirmType("Type1"));
        var subFirm2 = new SubFirm(new SubFirmType("Type2"));
        firm.AddSbFirm(subFirm1);
        firm.AddSbFirm(subFirm2);

        var subFirms = firm.GetAllSubFirms();

        Assert.That(subFirms.Length, Is.EqualTo(2));
    }

    [Test]
    public void GetAllContacts_ShouldReturnAllContactsFromAllSubFirms()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" });
        var subFirm1 = new SubFirm(new SubFirmType("Type1"));
        var subFirm2 = new SubFirm(new SubFirmType("Type2"));
        firm.AddSbFirm(subFirm1);
        firm.AddSbFirm(subFirm2);
        subFirm1.AddCont(new Contact { Descr = "Contact1" });
        subFirm2.AddCont(new Contact { Descr = "Contact2" });

        var contacts = firm.GetAllContacts();

        Assert.That(contacts.Length, Is.EqualTo(2));
    }

    [Test]
    public void AddContactToMainOffice_ShouldAddContactToMainOfficeIfExists()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" });
        var subFirm = new SubFirm(new SubFirmType("MainType", true));
        firm.AddSbFirm(subFirm);
        var contact = new Contact { Descr = "Main Office Contact" };

        firm.AddContactToMainOffice(contact);

        Assert.That(subFirm.GetContacts().Length, Is.EqualTo(1));
        Assert.That(subFirm.GetContacts()[0].Descr, Is.EqualTo("Main Office Contact"));
    }

    [Test]
    public void AddField_ShouldAddFieldIfNotExists()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" });

        firm.AddField("CustomField");

        Assert.That(firm.GetField("CustomField"), Is.EqualTo(string.Empty));
    }

    [Test]
    public void SetField_ShouldSetFieldValueIfFieldExists()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" });

        firm.SetField("Field1", "NewValue");

        Assert.That(firm.GetField("Field1"), Is.EqualTo("NewValue"));
    }

    [Test]
    public void RenameField_ShouldRenameFieldIfOldFieldExists()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" });

        firm.RenameField("Field1", "RenamedField1");

        Assert.That(firm.GetField("RenamedField1"), Is.EqualTo(string.Empty));
    }

    [Test]
    public void ToString_ShouldReturnFirmInfoInStringFormat()
    {
        var firm = new Firm(new string[] { "Field1", "Field2" })
        {
            Name = "FirmName",
            Country = "CountryName",
            Region = "RegionName",
            Town = "TownName",
            Street = "StreetName"
        };

        var result = firm.ToString();

        Assert.That(result, Is.EqualTo("FirmName, CountryName, RegionName, TownName, StreetName"));
    }
}

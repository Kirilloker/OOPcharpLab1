using System;
using System.Collections.Generic;
using NUnit.Framework;
using LoganovLab1Artem.FirmSpace;
using LoganovLab1Artem.ContactSpace;
using LoganovLab1Artem.SubFirmSpace;

[TestFixture]
public class FirmTests
{
    [Test]
    public void CreateFirm_ShouldCreateFirmWithCorrectFields()
    {
        // Arrange
        var factory = FirmFactory.Instance;
        factory.SetFieldNames(new string[] { "Field1", "Field2", "Field3", "Field4", "Field5" });
        factory.SbFirmTypes.Add(new SbFirmType("Главное подразделение", true));

        // Act
        var firm = factory.Create();

        // Assert
        Assert.That(firm, Is.Not.Null);
        Assert.That(firm.GetFieldNames().Length, Is.EqualTo(5));
    }


    [Test]
    public void FilterByRegion_ShouldReturnFirmsFromNizhnyNovgorodRegion()
    {
        // Arrange
        var firm1 = new Firm(new string[] { "Field1", "Field2" }) { Region = "Нижегородская область" };
        var firm2 = new Firm(new string[] { "Field1", "Field2" }) { Region = "Московская область" };
        var firm3 = new Firm(new string[] { "Field1", "Field2" }) { Region = "Нижегородская область" };

        var firmList = new List(new List<Firm> { firm1, firm2, firm3 });

        // Act
        var filteredFirms = firmList.FilterByRegion("Нижегородская область");

        // Assert
        Assert.That(filteredFirms.ToArray().Length, Is.EqualTo(2));
        Assert.That(filteredFirms.ToArray()[0].Region, Is.EqualTo("Нижегородская область"));
        Assert.That(filteredFirms.ToArray()[1].Region, Is.EqualTo("Нижегородская область"));
    }

    [Test]
    public void AddContactToAllFirms_ShouldAddContactWithDescriptionToEachFirm()
    {
        // Arrange
        var contact = new Contact
        {
            Descr = "Письмо послали",
            CntType = new ContType("Письмо")
        };

        var firm1 = new Firm(new string[] { "Field1", "Field2" });
        var firm2 = new Firm(new string[] { "Field1", "Field2" });
        firm1.AddSbFirm(new SubFirm(new SbFirmType("Основное подразделение")));
        firm2.AddSbFirm(new SubFirm(new SbFirmType("Основное подразделение")));

        var firmList = new List(new List<Firm> { firm1, firm2 });

        // Act
        firmList.AddContactToAllFirms(contact);

        // Assert
        Assert.That(firm1.GetAllContacts().Length, Is.EqualTo(1));
        Assert.That(firm1.GetAllContacts()[0].Descr, Is.EqualTo("Письмо послали"));
        Assert.That(firm2.GetAllContacts().Length, Is.EqualTo(1));
        Assert.That(firm2.GetAllContacts()[0].Descr, Is.EqualTo("Письмо послали"));
    }


    [Test]
    public void AddCommercialOfferToFirmsWithSupplyDepartment_ShouldAddOfferToFirmsWithSupplySubFirm()
    {
        // Arrange
        var contact = new Contact
        {
            Descr = "Коммерческое предложение",
            CntType = new ContType("Коммерческое предложение")
        };

        var supplyType = new SbFirmType("Отдел снабжения");

        var subFirmWithSupply = new SubFirm(supplyType);
        var firmWithSupply = new Firm(new string[] { "Field1", "Field2" });
        firmWithSupply.AddSbFirm(subFirmWithSupply);

        var firmWithoutSupply = new Firm(new string[] { "Field1", "Field2" });

        var firmList = new List(new List<Firm> { firmWithSupply, firmWithoutSupply });

        // Act
        firmList.AddContactToAllFirmsWithSubFirmType(contact, "Отдел снабжения");

        // Assert
        Assert.That(firmWithSupply.GetAllContacts().Length, Is.EqualTo(1));
        Assert.That(firmWithSupply.GetAllContacts()[0].Descr, Is.EqualTo("Коммерческое предложение"));
        Assert.That(firmWithoutSupply.GetAllContacts().Length, Is.EqualTo(0));
    }
}

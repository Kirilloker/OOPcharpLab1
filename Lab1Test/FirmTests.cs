using NUnit.Framework;
using LoganovLab1.Domain;
using LoganovLab1.Type;

namespace Lab1Test
{
    [TestFixture]
    public class FirmTests
    {
        private Firm _firm;

        [SetUp]
        public void Setup()
        {
            _firm = new Firm(new string[] { "Field1", "Field2", "Field3", "Field4", "Field5" });
        }

        [Test]
        public void AddSubFirm_ShouldAddSubFirmToList()
        {
            // Arrange
            var subFirm = new SubFirm(new SubFirmType("Отдел продаж"));

            // Act
            _firm.AddSubFirm(subFirm);

            // Assert
            var allSubFirms = _firm.GetAllSubFirms();
            Assert.That(allSubFirms.Length, Is.EqualTo(1));
            Assert.That(allSubFirms[0].Name, Is.EqualTo("Отдел продаж"));
        }

        [Test]
        public void AddSubFirm_ShouldNotAddNullSubFirm()
        {
            // Act
            _firm.AddSubFirm(null);

            // Assert
            var allSubFirms = _firm.GetAllSubFirms();
            Assert.That(allSubFirms.Length, Is.EqualTo(0));
        }

        [Test]
        public void AddSubFirm_ShouldAddMultipleSubFirms()
        {
            // Arrange
            var subFirm1 = new SubFirm(new SubFirmType("Отдел продаж"));
            var subFirm2 = new SubFirm(new SubFirmType("Отдел маркетинга"));
            var subFirm3 = new SubFirm(new SubFirmType("Отдел логистики"));

            // Act
            _firm.AddSubFirm(subFirm1);
            _firm.AddSubFirm(subFirm2);
            _firm.AddSubFirm(subFirm3);

            // Assert
            var allSubFirms = _firm.GetAllSubFirms();
            Assert.That(allSubFirms.Length, Is.EqualTo(3));
            Assert.That(allSubFirms[0].Name, Is.EqualTo("Отдел продаж"));
            Assert.That(allSubFirms[1].Name, Is.EqualTo("Отдел маркетинга"));
            Assert.That(allSubFirms[2].Name, Is.EqualTo("Отдел логистики"));
        }

        [Test]
        public void AddSubFirm_ShouldNotAddDuplicateSubFirms()
        {
            // Arrange
            var subFirm1 = new SubFirm(new SubFirmType("Отдел продаж"));
            var subFirm2 = subFirm1; // Дубликат по ссылке

            // Act
            _firm.AddSubFirm(subFirm1);
            _firm.AddSubFirm(subFirm2);

            // Assert
            var allSubFirms = _firm.GetAllSubFirms();
            Assert.That(allSubFirms.Length, Is.EqualTo(2));
        }

        [Test]
        public void AddSubFirm_ShouldNotFailWhenAddingLargeNumberOfSubFirms()
        {
            // Act
            for (int i = 0; i < 1000; i++)
            {
                _firm.AddSubFirm(new SubFirm(new SubFirmType($"Подразделение {i}")));
            }

            // Assert
            var allSubFirms = _firm.GetAllSubFirms();
            Assert.That(allSubFirms.Length, Is.EqualTo(1000));
        }
    }
}

//using NUnit.Framework;
//using LoganovLab1.Domain;
//using LoganovLab1.Type;

//namespace Lab1Test
//{
//    [TestFixture]
//    public class FirmSubFirmTests
//    {
//        private Firm _firm;

//        [SetUp]
//        public void Setup()
//        {
//            // Инициализация перед каждым тестом — создаём объект Firm с набором пользовательских полей
//            _firm = new Firm(new string[] { "Field1", "Field2", "Field3", "Field4", "Field5" });
//        }

//        [Test]
//        public void AddSubFirm_ShouldAddSubFirmToEmptyList()
//        {
//            // Arrange
//            var subFirm = new SubFirm(new SubFirmType("Отдел продаж"));

//            // Act
//            _firm.AddSubFirm(subFirm);

//            // Assert
//            var allSubFirms = _firm.GetAllSubFirms();
//            Assert.That(allSubFirms.Length, Is.EqualTo(1), "Ожидается, что будет добавлено одно подразделение.");
//            Assert.That(allSubFirms[0].Name, Is.EqualTo("Отдел продаж"), "Ожидается, что имя подразделения будет 'Отдел продаж'.");
//        }

//        [Test]
//        public void AddSubFirm_ShouldAddMultipleSubFirms()
//        {
//            // Arrange
//            var subFirm1 = new SubFirm(new SubFirmType("Отдел продаж"));
//            var subFirm2 = new SubFirm(new SubFirmType("Отдел маркетинга"));
//            var subFirm3 = new SubFirm(new SubFirmType("Отдел логистики"));

//            // Act
//            _firm.AddSubFirm(subFirm1);
//            _firm.AddSubFirm(subFirm2);
//            _firm.AddSubFirm(subFirm3);

//            // Assert
//            var allSubFirms = _firm.GetAllSubFirms();
//            Assert.That(allSubFirms.Length, Is.EqualTo(3), "Ожидается, что будут добавлены три подразделения.");
//            Assert.That(allSubFirms[0].Name, Is.EqualTo("Отдел продаж"));
//            Assert.That(allSubFirms[1].Name, Is.EqualTo("Отдел маркетинга"));
//            Assert.That(allSubFirms[2].Name, Is.EqualTo("Отдел логистики"));
//        }

//        [Test]
//        public void AddSubFirm_ShouldIgnoreNullSubFirm()
//        {
//            // Act
//            _firm.AddSubFirm(null);

//            // Assert
//            var allSubFirms = _firm.GetAllSubFirms();
//            Assert.That(allSubFirms.Length, Is.EqualTo(0), "Ожидается, что null-подразделение не будет добавлено.");
//        }

//        [Test]
//        public void RemoveSubFirm_ShouldRemoveExistingSubFirm()
//        {
//            // Arrange
//            var subFirm = new SubFirm(new SubFirmType("Отдел продаж"));
//            _firm.AddSubFirm(subFirm);

//            // Act
//            var removed = _firm.RemoveSubFirm(subFirm);

//            // Assert
//            Assert.That(removed, Is.True, "Ожидается, что подразделение будет удалено.");
//            var allSubFirms = _firm.GetAllSubFirms();
//            Assert.That(allSubFirms.Length, Is.EqualTo(0), "Ожидается, что список подразделений будет пуст.");
//        }

//        [Test]
//        public void RemoveSubFirm_ShouldHandleNonExistingSubFirm()
//        {
//            // Arrange
//            var subFirm = new SubFirm(new SubFirmType("Отдел продаж"));

//            // Act
//            var removed = _firm.RemoveSubFirm(subFirm);

//            // Assert
//            Assert.That(removed, Is.False, "Ожидается, что удаление несуществующего подразделения вернет false.");
//        }

//        [Test]
//        public void RemoveSubFirm_ShouldIgnoreNull()
//        {
//            // Act
//            var removed = _firm.RemoveSubFirm(null);

//            // Assert
//            Assert.That(removed, Is.False, "Ожидается, что удаление null вернет false.");
//        }

//        [Test]
//        public void RemoveSubFirm_ShouldNotContainRemovedSubFirm()
//        {
//            // Arrange
//            var subFirm1 = new SubFirm(new SubFirmType("Отдел продаж"));
//            var subFirm2 = new SubFirm(new SubFirmType("Отдел маркетинга"));
//            _firm.AddSubFirm(subFirm1);
//            _firm.AddSubFirm(subFirm2);

//            // Act
//            _firm.RemoveSubFirm(subFirm1);

//            // Assert
//            var allSubFirms = _firm.GetAllSubFirms();
//            Assert.That(allSubFirms.Length, Is.EqualTo(1), "Ожидается, что в списке останется одно подразделение.");
//            Assert.That(allSubFirms[0].Name, Is.EqualTo("Отдел маркетинга"), "Ожидается, что оставшееся подразделение будет 'Отдел маркетинга'.");
//        }

//        [Test]
//        public void GetAllSubFirms_ShouldReturnCorrectArray()
//        {
//            // Arrange
//            var subFirm1 = new SubFirm(new SubFirmType("Отдел продаж"));
//            var subFirm2 = new SubFirm(new SubFirmType("Отдел маркетинга"));
//            _firm.AddSubFirm(subFirm1);
//            _firm.AddSubFirm(subFirm2);

//            // Act
//            var allSubFirms = _firm.GetAllSubFirms();

//            // Assert
//            Assert.That(allSubFirms.Length, Is.EqualTo(2), "Ожидается, что массив будет содержать два подразделения.");
//            Assert.That(allSubFirms[0].Name, Is.EqualTo("Отдел продаж"));
//            Assert.That(allSubFirms[1].Name, Is.EqualTo("Отдел маркетинга"));
//        }
//    }
//}

//using NUnit.Framework;
//using LoganovLab1.Domain;

//namespace Lab1Test
//{
//    [TestFixture]
//    public class FirmUserFieldTests
//    {
//        private Firm _firm;

//        [SetUp]
//        public void Setup()
//        {
//            // Инициализация перед каждым тестом — создаём объект Firm с набором пользовательских полей
//            _firm = new Firm(new string[] { "Field1", "Field2", "Field3", "Field4", "Field5" });
//        }

//        [Test]
//        public void SetUserFieldValue_ShouldSetValueForExistingField()
//        {
//            // Act
//            _firm.SetUserFieldValue("Field1", "TestValue");

//            // Assert
//            var value = _firm.GetUserFieldValue("Field1");
//            Assert.That(value, Is.EqualTo("TestValue"), "Ожидается, что значение 'Field1' будет установлено в 'TestValue'.");
//        }

//        [Test]
//        public void SetUserFieldValue_ShouldIgnoreNonExistingField()
//        {
//            // Act
//            _firm.SetUserFieldValue("NonExistingField", "TestValue");

//            // Assert
//            var value = _firm.GetUserFieldValue("NonExistingField");
//            Assert.That(value, Is.Null, "Ожидается, что значение для несуществующего поля останется null.");
//        }

//        [Test]
//        public void SetUserFieldValue_ShouldHandleEmptyValue()
//        {
//            // Act
//            _firm.SetUserFieldValue("Field1", "");

//            // Assert
//            var value = _firm.GetUserFieldValue("Field1");
//            Assert.That(value, Is.EqualTo(""), "Ожидается, что значение 'Field1' будет пустой строкой.");
//        }

//        [Test]
//        public void GetUserFieldValue_ShouldReturnValueForExistingField()
//        {
//            // Arrange
//            _firm.SetUserFieldValue("Field2", "AnotherValue");

//            // Act
//            var value = _firm.GetUserFieldValue("Field2");

//            // Assert
//            Assert.That(value, Is.EqualTo("AnotherValue"), "Ожидается, что метод вернёт 'AnotherValue' для поля 'Field2'.");
//        }

//        [Test]
//        public void GetUserFieldValue_ShouldReturnNullForNonExistingField()
//        {
//            // Act
//            var value = _firm.GetUserFieldValue("NonExistingField");

//            // Assert
//            Assert.That(value, Is.Null, "Ожидается, что метод вернёт null для несуществующего поля.");
//        }

//        [Test]
//        public void GetUserFieldValue_ShouldHandleEmptyFieldName()
//        {
//            // Act
//            var value = _firm.GetUserFieldValue("");

//            // Assert
//            Assert.That(value, Is.Null, "Ожидается, что метод вернёт null для пустого имени поля.");
//        }
//    }
//}

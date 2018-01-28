using Blockchain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockchainTests
{
    /// <summary>
    /// Проверяем работу сереализации и десереализации.
    /// </summary>
    [TestClass]
    public class SerializationTest
    {
        /// <summary>
        /// Сериализация и десереализация пользователя.
        /// </summary>
        [TestMethod]
        public void UserSerializationTest()
        {
            // Arrange.
            var user = new User("admin", "admin");

            // Act.
            var json = user.GetJson();
            var resultUser = User.Deserialize(json);

            // Assert.
            Assert.AreEqual(user.Login, resultUser.Login);
            Assert.AreEqual(user.Password, resultUser.Password);
            Assert.AreEqual(user.Role, resultUser.Role);
            Assert.IsTrue(resultUser.IsCorrect());
        }

        /// <summary>
        /// Сериализация и десереализация данных.
        /// </summary>
        [TestMethod]
        public void DataSerializationTest()
        {
            // Arrange.
            var data = new Data("https://shwanoff.ru/", DataType.Content);

            // Act.
            var json = data.GetJson();
            var resultData = Data.Deserialize(json);

            // Assert.
            Assert.AreEqual(data.Content, resultData.Content);
            Assert.AreEqual(data.Type, resultData.Type);
            Assert.AreEqual(data.Hash, resultData.Hash);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Tests
{

    /// <summary>
    /// Тесты проверяющие пользователя.
    /// </summary>
    [TestClass()]
    public class UserTests
    {
        /// <summary>
        /// Проверяем создание пользователя.
        /// </summary>
        [TestMethod()]
        public void UserTest()
        {
            // Arrange.
            var login = "admin";
            var password = "password";

            // Act.
            var user = new User(login, password);

            // Assert.
            Assert.IsTrue(user.IsCorrect());
        }
    }
}
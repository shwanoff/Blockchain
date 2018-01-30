using Blockchain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Blockchain.Tests
{
    /// <summary>
    /// Тесты проверяющие работу цепочки.
    /// </summary>
    [TestClass()]
    public class ChainTests
    {
        /// <summary>
        /// Проверяем создание цепочки и добавление блоков.
        /// </summary>
        [TestMethod()]
        public void ChainTest()
        {
            var chain = new Chain();

            Assert.IsTrue(chain.CheckCorrect());
        }

        /// <summary>
        /// Проверяем добавление данных.
        /// </summary>
        [TestMethod()]
        public void AddContentTest()
        {
            // Arrange.
            var text = "Hello world";
            var chain = new Chain();
            var login = "user";
            var password = "user";

            // Act.
            chain.AddContent(text);
            chain.AddUser(login, password);

            // Assert.
            Assert.IsTrue(chain.CheckCorrect());
        }
    }
}
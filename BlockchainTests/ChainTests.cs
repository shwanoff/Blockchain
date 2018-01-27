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
            var text = "https://shwanoff.ru/";
            var chain = new Chain();
            var login = "user";
            var password = "user";

            chain.AddContent(text);
            chain.AddUser(login, password);

            Assert.IsTrue(chain.CheckCorrect());
        }
    }
}
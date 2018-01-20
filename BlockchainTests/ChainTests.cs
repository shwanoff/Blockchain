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
            Assert.AreEqual(1, chain.ContentBlocks.Count());
            Assert.AreEqual(text, chain.ContentBlocks.Single().Data.Content);
            Assert.AreEqual(2, chain.UserBlocks.Count());
        }
    }
}
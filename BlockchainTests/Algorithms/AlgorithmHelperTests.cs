using Blockchain.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Algorithms.Tests
{
    /// <summary>
    /// Тесты проверяющие работу хелпера алгоритмов хеширования.
    /// </summary>
    [TestClass()]
    public class AlgorithmHelperTests
    {
        private string _text = "https://shwanoff.ru";

        /// <summary>
        /// Проверяем хеширование строки.
        /// </summary>
        [TestMethod()]
        public void GetHashTest()
        {
            // Arrange.
            var originalHash = "9568884c83a12f70803f500373a2208fcb23734e6b9e8017beb5e5b522df6694";

            // Act.
            var hash = _text.GetHash();

            // Assert.
            Assert.AreEqual(originalHash, hash);
        }

        [TestMethod()]
        public void GetHashTest1()
        {
            // Arrange.
            var component = new Data(_text, DataType.Content);
            var originalHash = "7efa400e1055240d9b3cba68369402bed5ed1059bdf50d4776cf3a155ee262a9";

            // Act.
            var hash = component.GetHash();

            // Assert.
            Assert.AreEqual(originalHash, hash);
        }
    }
}
using Blockchain.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Algorithms.Tests
{
    /// <summary>
    /// Тест хеширования алгоримтом SHA256.
    /// </summary>
    [TestClass()]
    public class Sha256Tests
    {
        // Arrange
        private string _text = "https://shwanoff.ru";
        private IAlgorithm _algorithm = new Sha256();


        /// <summary>
        /// Проверка хеширования строки.
        /// </summary>
        [TestMethod()]
        public void GetHashTest()
        {
            // Arrange.
            var originalHash = "9568884c83a12f70803f500373a2208fcb23734e6b9e8017beb5e5b522df6694";

            // Act.
            var hash = _algorithm.GetHash(_text);

            // Assert.
            Assert.AreEqual(originalHash, hash);
        }

        /// <summary>
        /// Проверяем хеширование компонента.
        /// </summary>
        [TestMethod()]
        public void GetHashTest1()
        {
            // Arrange.
            var originalHash = "7efa400e1055240d9b3cba68369402bed5ed1059bdf50d4776cf3a155ee262a9";
            var component = new Data(_text, DataType.Content, _algorithm);

            // Act.
            var hash = _algorithm.GetHash(component);

            // Assert.
            Assert.AreEqual(originalHash, hash);
        }
    }
}
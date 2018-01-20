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
        private IAlgorithm _algorithm = AlgorithmHelper.GetDefaultAlgorithm();

        /// <summary>
        /// Проверяем хеширование строки.
        /// </summary>
        [TestMethod()]
        public void GetHashTest()
        {
            // Arrange.
            var originalHash = "9568884c83a12f70803f500373a2208fcb23734e6b9e8017beb5e5b522df6694";

            // Act.
            var hash = _text.GetHash(_algorithm);
            var hash2 = _text.GetHash();

            // Assert.
            Assert.AreEqual(originalHash, hash);
            Assert.AreEqual(originalHash, hash2);
        }

        [TestMethod()]
        public void GetHashTest1()
        {
            // Arrange.
            var component = new Data(_text, DataType.Content, _algorithm);
            var component2 = new Data(_text, DataType.Content);
            var originalHash = "0b34382664352b20f64bd8354b798823d888b5a96ca963beb44e1d4c460daf77";

            // Act.
            var hash = component.GetHash(_algorithm);
            var hash2 = component2.GetHash(_algorithm);
            var hash3 = component2.GetHash();
            var hash4 = component.GetHash();

            // Assert.
            Assert.AreEqual(originalHash, hash);
            Assert.AreEqual(originalHash, hash2);
            Assert.AreEqual(originalHash, hash3);
            Assert.AreEqual(originalHash, hash4);
        }
    }
}
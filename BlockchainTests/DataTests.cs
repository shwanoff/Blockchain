using Blockchain;
using Blockchain.Algorithms;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Tests
{
    /// <summary>
    /// Тестируем данные блока.
    /// </summary>
    [TestClass()]
    public class DataTests
    {
        private string _text = "https://shwanoff.ru";
        private IAlgorithm _algorithm = AlgorithmHelper.GetDefaultAlgorithm();

        /// <summary>
        /// Создание нового блока.
        /// </summary>
        [TestMethod()]
        public void DataTest()
        {
            // Arrange.
            var originalHash = "0b34382664352b20f64bd8354b798823d888b5a96ca963beb44e1d4c460daf77";

            // Act.
            var data = new Data(_text, DataType.Content, _algorithm);
            var data2 = new Data(_text, DataType.Content);

            // Assert.
            Assert.AreEqual(originalHash, data.Hash);
            Assert.AreEqual(originalHash, data2.Hash);
        }
    }
}
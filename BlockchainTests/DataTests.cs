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

        /// <summary>
        /// Создание нового блока.
        /// </summary>
        [TestMethod()]
        public void DataTest()
        {
            // Arrange.
            var originalHash = "7efa400e1055240d9b3cba68369402bed5ed1059bdf50d4776cf3a155ee262a9";

            // Act.
            var data = new Data(_text, DataType.Content);

            // Assert.
            Assert.AreEqual(originalHash, data.Hash);
        }
    }
}
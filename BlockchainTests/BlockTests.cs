using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blockchain.Tests
{

    /// <summary>
    /// Тесты проверяющие работу блоков.
    /// </summary>
    [TestClass()]
    public class BlockTests
    {
        /// <summary>
        /// Проверяем создание блока.
        /// </summary>
        [TestMethod()]
        public void BlockTest()
        {
            // Arrange.
            var text = "https://shvanoff.ru/";
            var genesisHash = "680b114be22118e3ecad88e3ad853235ca36014f912552742c5efbf705d39e78";
            var user = User.GetCurrentUser();
            var data = new Data(text, DataType.Content);

            // Act.
            var genesisBlock = Block.GetGenesisBlock();
            var block = new Block(genesisBlock, data, user);

            // Assert.
            Assert.IsTrue(genesisBlock.IsCorrect());
            Assert.AreEqual(genesisHash, genesisBlock.Hash);
            Assert.IsTrue(block.IsCorrect());
        }
    }
}
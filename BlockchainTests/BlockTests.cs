using Blockchain;
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
            var user = User.GetCurrentUser();
            var data = new Data(text, DataType.Content);

            // Act.
            var genesisBlock = Block.GetGenesisBlock();
            var block = new Block(genesisBlock, data, user);

            // Assert.
            Assert.IsTrue(genesisBlock.IsCorrect());
            Assert.IsTrue(block.IsCorrect());
        }
    }
}
using Blockchain.Algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;


namespace Blockchain
{
    /// <summary>
    /// Цепочка блоков.
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Список, содержащий в себе все блоки.
        /// </summary>
        private List<Block> _blockChain = null;

        /// <summary>
        /// Алгоритм хеширования используемый в этой цепочки блоков.
        /// </summary>
        public IAlgorithm Algorithm { get; private set; }

        /// <summary>
        /// Цепочка блоков.
        /// </summary>
        public IEnumerable<Block> BlockChain => _blockChain;

        /// <summary>
        /// Крайний блок в цепочке блоков.
        /// </summary>
        public Block PreviousBlock => _blockChain.Last();

        /// <summary>
        /// Создать новый экземпляр цепочки блоков.
        /// </summary>
        public Chain()
        {
            var globalChain = GetGlobalChein();
            if(globalChain == null)
            {
                CreateNewBlockChain();
            }
            else
            {
                bool globalChainIsCorrect = globalChain.CheckCorrect();
                if(globalChainIsCorrect)
                {
                    _blockChain = globalChain._blockChain;
                }
                else
                {
                    CreateNewBlockChain();
                    throw new TypeLoadException("Полученная глобальная цепочка блоков является некорректной!");
                }
            }
        }

        /// <summary>
        /// Создать новую пустую цепочку блоков.
        /// </summary>
        private void CreateNewBlockChain()
        {
            _blockChain = new List<Block>();
            var currentUser = User.GetCurrentUser();
            var genesisBlock = Block.GetGenesisBlock(currentUser, Algorithm);
            _blockChain.Add(genesisBlock);
        }

        /// <summary>
        /// Проверить корректность цепочки блоков.
        /// </summary>
        /// <returns> Корректность цепочки блоков. true - цепочка блоков корректна, false - цепочка некорректна. </returns>
        public bool CheckCorrect()
        {
            foreach(var block in _blockChain)
            {
                if(!block.IsCorrect(Algorithm))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Получить глобальную цепочку блоков.
        /// </summary>
        /// <returns> Цепочка блоков. </returns>
        private Chain GetGlobalChein()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавить данные в цепочку блоков.
        /// </summary>
        /// <param name="data"> Добавляемые данные. </param>
        public void Add(Data data)
        {
            Contract.Requires<ArgumentNullException>(data != null, $"Не возможно добавить данные в цепочку блоков. Отсутствуют данные для сохранения в блоке.");
            Contract.Requires<ArgumentNullException>(data.IsCorrect(Algorithm), $"Не возможно добавить данные в цепочку блоков. Данные являются некорректными.");

            var block = new Block(PreviousBlock, data, User.GetCurrentUser(), Algorithm);
            if(block.IsCorrect(Algorithm))
            {
                _blockChain.Add(block);
            }
            else
            {
                throw new ArgumentException("Не удалось добавить данные в цепочку блоков. Созданные блок некорректен.", nameof(block));
            }
        }
    }
}

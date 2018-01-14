using Blockchain.Algorithms;
using System;
using System.Diagnostics.Contracts;

namespace Blockchain
{
    /// <summary>
    /// Блок из цепочки блоков.
    /// </summary>
    public class Block : IHashable
    {
        /// <summary>
        /// Версия спецификации блока.
        /// </summary>
        public ushort Version { get; private set; }

        /// <summary>
        /// Псевдоуникальный 128-битный идентификатор.
        /// </summary>
        public Guid Code { get; private set; }

        /// <summary>
        /// Момент создания блока.
        /// </summary>
        public ulong CreatedOn { get; private set; }

        /// <summary>
        /// Хеш блока.
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// Хеш предыдущего блока.
        /// </summary>
        public string PreviousHash { get; private set; }

        /// <summary>
        /// Данные блока.
        /// </summary>
        public Data Data { get; private set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего блок.
        /// </summary>
        public User User { get; private set; }

        /// <summary>
        /// Создать экземпляр блока.
        /// </summary>
        /// <param name="previousBlock">Предыдущий блок.</param>
        /// <param name="data">Данные, сохраняемые в блоке.</param>
        /// <param name="algorithm">Алгоритм хеширования.</param>
        /// <param name="user">Идентификатор пользователя, создавшего блок.</param>
        public Block(Block previousBlock, Data data, User user, IAlgorithm algorithm)
        {
            Contract.Requires<ArgumentNullException>(previousBlock != null, $"Не возможно создать блок. Отсутствует ссылка на предыдущий блок.");
            Contract.Requires<ArgumentException>(previousBlock.IsCorrect(algorithm), $"Не возможно создать блок. Предыдущий блок не корректный.");
            Contract.Requires<ArgumentNullException>(data != null, $"Не возможно создать блок. Отсутствуют данные для сохранения в блоке.");
            Contract.Requires<ArgumentNullException>(user != null, $"Не возможно создать блок. Отсутствует идентификатор пользователя.");

            Version = Properties.Settings.Default.Version;
            Code = Guid.NewGuid();
            CreatedOn = (ulong)DateTime.Now.Ticks;
            PreviousHash = previousBlock.Hash;
            Data = data;
            User = user;
            Hash = this.GetHash(algorithm);
        }

        /// <summary>
        /// Создать новый экземпляр стартового (генезис) блока.
        /// </summary>
        /// <param name="user"> Пользователь системы. </param>
        /// <param name="algorithm"> Алгоритм хеширования. </param>
        protected Block(User user, IAlgorithm algorithm)
        {
            Version = Properties.Settings.Default.Version;
            Code = Guid.NewGuid();
            CreatedOn = (ulong)DateTime.Now.Ticks;
            PreviousHash = algorithm.GetHash(Guid.NewGuid().ToString());
            Data = new Data("Genesis block", algorithm);
            User = user;
            Hash = this.GetHash(algorithm);
        }

        /// <summary>
        /// Получить начальный блок цепочки блоков.
        /// </summary>
        /// <param name="user"> Пользователь системы. </param>
        /// <param name="algorithm"> Алгоритм хеширования. </param>
        /// <returns> Стартовый блок. </returns>
        public static Block GetGenesisBlock(User user, IAlgorithm algorithm)
        {
            var genesisBlock = new Block(user, algorithm);
            return genesisBlock;
        }

        /// <summary>
        /// Получить данные из объекта, на основе которых будет строиться хеш.
        /// </summary>
        /// <returns> Хешируемые данные. </returns>
        public string GetStringForHash()
        {
            var data = "";
            data += Version;
            data += Code;
            data += CreatedOn;
            data += PreviousHash;
            data += Data.Hash;
            data += User;
            return data;
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Идентификатор блока. </returns>
        public override string ToString()
        {
            return Code.ToString();
        }
    }
}

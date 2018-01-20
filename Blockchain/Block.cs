using Blockchain.Algorithms;
using System;
using System.Diagnostics.Contracts;
using Blockchain.Exceptions;

namespace Blockchain
{
    /// <summary>
    /// Блок из цепочки блоков.
    /// </summary>
    public class Block : IHashable
    {
        /// <summary>
        /// Алгоритм хеширования.
        /// </summary>
        private IAlgorithm _algorithm = AlgorithmHelper.GetDefaultAlgorithm();

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
        /// <param name="user"> Идентификатор пользователя, создавшего блок. </param>
        public Block(Block previousBlock, Data data, User user, IAlgorithm algorithm = null)
        {
            if(previousBlock == null)
            {
                throw new MethodRequiresException(nameof(previousBlock));
            }

            if(!previousBlock.IsCorrect())
            {
                throw new MethodRequiresException(nameof(previousBlock));
            }

            if(data == null)
            {
                throw new MethodRequiresException(nameof(data));
            }

            if(!data.IsCorrect())
            {
                throw new MethodRequiresException(nameof(data));
            }

            if(user == null)
            {
                throw new MethodRequiresException(nameof(user));
            }

            if(!user.IsCorrect())
            {
                throw new MethodRequiresException(nameof(user));
            }

            if(algorithm != null)
            {
                _algorithm = algorithm;
            }

            Version = Properties.Settings.Default.Version;
            Code = Guid.NewGuid();
            CreatedOn = (ulong)DateTime.Now.Ticks;
            PreviousHash = previousBlock.Hash;
            Data = data;
            User = user;
            Hash = this.GetHash(_algorithm);

            if (!this.IsCorrect())
            {
                throw new MethodResultException(nameof(Block));
            }
        }

        /// <summary>
        /// Создать новый экземпляр стартового (генезис) блока.
        /// </summary>
        /// <param name="user"> Пользователь системы. </param>
        /// <param name="algorithm"> Алгоритм хеширования. </param>
        protected Block(IAlgorithm algorithm = null)
        {
            if (algorithm != null)
            {
                _algorithm = algorithm;
            }

            Version = 1;
            Code = Guid.Parse("8002EFBA-72FA-4156-B7F2-BBB818160E64");
            CreatedOn = (ulong)DateTime.Parse("2018-01-01").Ticks;
            User = new User("admin", "admin", UserRole.Admin);
            PreviousHash = _algorithm.GetHash("79098738-8772-4F0A-998D-9EC7737720F4");
            Data = User.GetData();
            Hash = this.GetHash(_algorithm);

            if (!this.IsCorrect())
            {
                throw new MethodResultException(nameof(Block));
            }
        }

        /// <summary>
        /// Получить начальный блок цепочки блоков.
        /// </summary>
        /// <param name="algorithm"> Алгоритм хеширования. </param>
        /// <returns> Стартовый блок. </returns>
        public static Block GetGenesisBlock(IAlgorithm algorithm = null)
        {
            if (algorithm == null)
            {
                algorithm = AlgorithmHelper.GetDefaultAlgorithm();
            }

            var genesisBlock = new Block(algorithm);
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

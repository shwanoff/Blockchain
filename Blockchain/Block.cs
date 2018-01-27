using Blockchain.Algorithms;
using System;
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
        public int Version { get; private set; }

        /// <summary>
        /// Псевдоуникальный 128-битный идентификатор.
        /// </summary>
        public Guid Code { get; private set; }

        /// <summary>
        /// Момент создания блока.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

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
            #region Requires
            if (previousBlock == null)
            {
                throw new MethodRequiresException(nameof(previousBlock), "Предыдущий блок не может быть равен null.");
            }

            if(!previousBlock.IsCorrect())
            {
                throw new MethodRequiresException(nameof(previousBlock), "Предыдущий блок некорректный.");
            }

            if(data == null)
            {
                throw new MethodRequiresException(nameof(data), "Данные не могут быть равны null.");
            }

            if(!data.IsCorrect())
            {
                throw new MethodRequiresException(nameof(data), "Данные некорректные.");
            }

            if(user == null)
            {
                throw new MethodRequiresException(nameof(user), "Пользователь не может быть равен null.");
            }

            if(!user.IsCorrect())
            {
                throw new MethodRequiresException(nameof(user), "Пользователь некорректный.");
            }
            #endregion

            if (algorithm != null)
            {
                _algorithm = algorithm;
            }

            Version = 1;
            Code = Guid.NewGuid();
            CreatedOn = DateTime.Now.ToUniversalTime();
            PreviousHash = previousBlock.Hash;
            Data = data;
            User = user;
            Hash = this.GetHash(_algorithm);

            if (!this.IsCorrect())
            {
                throw new MethodResultException(nameof(Block), "Ошибка создания блока. Блок некорректный.");
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
            CreatedOn = DateTime.UtcNow;
            User = new User("admin", "admin", UserRole.Admin);
            PreviousHash = _algorithm.GetHash("79098738-8772-4F0A-998D-9EC7737720F4");
            Data = User.GetData();
            Hash = this.GetHash(_algorithm);

            if (!this.IsCorrect())
            {
                throw new MethodResultException(nameof(Block), "Ошибка создания генезис блока. Блок некорректный.");
            }
        }

        /// <summary>
        /// Создание блока цепочки из блока провайдера данных.
        /// </summary>
        /// <param name="block"> Блок провайдера данных. </param>
        public Block(BlockchainData.Block block)
        {
            if(block == null)
            {
                throw new MethodRequiresException(nameof(block), "Блок данных провайдера данных не может быть равен null.");
            }

            Version = block.Version;
            Code = Guid.Parse(block.Code);
            CreatedOn = block.CreatedOn;
            User = User.Deserialize(block.User);
            PreviousHash = block.PreviousHash;
            Data = Data.Deserialize(block.Data);
            Hash = block.Hash;

            if (!this.IsCorrect())
            {
                throw new MethodResultException(nameof(Block), "Ошибка создания блока из блока провайдера данных. Блок некорректный.");
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

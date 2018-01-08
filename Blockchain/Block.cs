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
        /// Создать экземпляр блока.
        /// </summary>
        /// <param name="previousBlock">Предыдущий блок.</param>
        /// <param name="data">Данные, сохраняемые в блоке.</param>
        /// <param name="algorithm">Алгоритм хеширования.</param>
        public Block(Block previousBlock, Data data, IAlgorithm algorithm)
        {
            Contract.Requires<ArgumentNullException>(previousBlock != null, $"Не возможно создать блок. Отсутствует ссылка на предыдущий блок.");
            Contract.Requires<ArgumentException>(previousBlock.IsCorrect(), $"Не возможно создать блок. Предыдущий блок не корректный.");
            Contract.Requires<ArgumentNullException>(data != null, $"Не возможно создать блок. Отсутствуют данные для сохранения в блоке.");

            Version = Properties.Settings.Default.Version;
            Code = Guid.NewGuid();
            CreatedOn = (ulong)DateTime.Now.Ticks;
            PreviousHash = previousBlock.Hash;
            Data = data;
            Hash = this.GetHash(algorithm);
        }

        /// <inheritdoc />
        public string GetStringForHash()
        {
            var data = "";
            data += Version;
            data += Code;
            data += CreatedOn;
            data += PreviousHash;
            data += Data.Hash;
            return data;
        }

        public bool IsCorrect()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns>Идентификатор блока.</returns>
        public override string ToString()
        {
            return Code.ToString();
        }
    }
}

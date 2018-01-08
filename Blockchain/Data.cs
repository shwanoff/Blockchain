using Blockchain.Algorithms;

namespace Blockchain
{
    /// <summary>
    /// Данные хранимые в блоке из цепочки блоков.
    /// </summary>
    public class Data : IHashable
    {
        /// <summary>
        /// Содержимое блока.
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Хеш блока.
        /// </summary>
        public string Hash { get; private set; }

        /// <summary>
        /// Создать экземпляр данных.
        /// </summary>
        /// <param name="content">Данные.</param>
        /// <param name="algorithm">Алгоритм для хеширования.</param>
        public Data(string content, IAlgorithm algorithm)
        {
            Content = content;
            Hash = this.GetHash(algorithm);
        }

        /// <inheritdoc />
        public string GetStringForHash()
        {
            return Content;
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns>Хранящиеся данные.</returns>
        public override string ToString()
        {
            return Content;
        }
    }
}

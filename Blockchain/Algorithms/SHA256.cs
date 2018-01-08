using System;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Algorithms
{
    /// <summary>
    /// Алгоритм шифрования SHA256.
    /// </summary>
    public class Sha256 : IAlgorithm
    {
        /// <summary>
        /// Компонент для вычисления хеша SHA256.
        /// </summary>
        private SHA256 _sha256 = null;

        /// <summary>
        /// Создать новый экземпляр алгоритма.
        /// </summary>
        public Sha256()
        {
            _sha256 = SHA256.Create();
        }

        /// <summary>
        /// Получить хеш.
        /// </summary>
        /// <param name="data">Входные данные</param>
        /// <returns>Хеш.</returns>
        public string GetHash(string data)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(data), "Не возможно выполнить хеширование. Аргумент не содержит данных");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            var bytes = Encoding.Default.GetBytes(data);
            var hashByte = _sha256.ComputeHash(bytes);
            var hash = BitConverter.ToString(hashByte);

            return hash;
        }

        /// <summary>
        /// Получить хеш.
        /// </summary>
        /// <param name="data">Хешируемый компонент.</param>
        /// <returns>Хеш компонента.</returns>
        public string GetHash(IHashable data)
        {
            Contract.Requires<ArgumentNullException>(data != null, "Не возможно выполнить хеширование. Аргумент равен null.");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            var dataBeforeHash = data.GetStringForHash();
            var hash = GetHash(dataBeforeHash);
            return hash;
        }
    }
}

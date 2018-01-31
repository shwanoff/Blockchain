using System;
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
            var bytes = Encoding.UTF8.GetBytes(data);
            var hashByte = _sha256.ComputeHash(bytes);
            var hash = BitConverter.ToString(hashByte);

            var formattedHash = hash.Replace("-", "")
                                    .ToLower();

            return formattedHash;
        }

        /// <summary>
        /// Получить хеш.
        /// </summary>
        /// <param name="data">Хешируемый компонент.</param>
        /// <returns>Хеш компонента.</returns>
        public string GetHash(IHashable data)
        {
            var dataBeforeHash = data.GetStringForHash();
            var hash = GetHash(dataBeforeHash);
            return hash;
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Название алгоритма хеширования. </returns>
        public override string ToString()
        {
            return "SHA 256";
        }
    }
}

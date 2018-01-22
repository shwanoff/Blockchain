using Blockchain.Algorithms;
using Blockchain.Exceptions;
using System.Runtime.Serialization;

namespace Blockchain
{
    /// <summary>
    /// Данные хранимые в блоке из цепочки блоков.
    /// </summary>
    [DataContract]
    public class Data : IHashable
    {
        /// <summary>
        /// Алгоритм хеширования.
        /// </summary>
        private IAlgorithm _algorithm = AlgorithmHelper.GetDefaultAlgorithm();

        /// <summary>
        /// Содержимое блока.
        /// </summary>
        [DataMember]
        public string Content { get; private set; }

        /// <summary>
        /// Хеш данных.
        /// </summary>
        [DataMember]
        public string Hash { get; private set; }

        /// <summary>
        /// Тип хранимых данных.
        /// </summary>
        [DataMember]
        public DataType Type { get; private set; }

        /// <summary>
        /// Создать экземпляр данных.
        /// </summary>
        /// <param name="content"> Данные. </param>
        /// <param name="algorithm"> Алгоритм для хеширования. </param>
        public Data(string content, DataType type, IAlgorithm algorithm = null)
        {
            // Проверяем предусловия.
            if(string.IsNullOrEmpty(content))
            {
                throw new MethodRequiresException(nameof(content), "Данные не могут быть пустыми или равняться null");
            }

            // Если не указан алгоритм, то берем по умолчанию.
            if (algorithm != null)
            {
                _algorithm = algorithm;
            }

            Content = content;
            Type = type;

            Hash = this.GetHash(_algorithm);

            if (!this.IsCorrect())
            {
                throw new MethodResultException(nameof(Data), "Ошибка создания данных. Данные некорректны.");
            }
        }

        /// <summary>
        /// Получить данные из объекта, на основе которых будет строиться хеш.
        /// </summary>
        /// <returns> Хешируемые данные. </returns>
        public string GetStringForHash()
        {
            var text = Content;
            text += Type;
            return text;
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Хранящиеся данные. </returns>
        public override string ToString()
        {
            return Content;
        }

        public string Json()
        {
            var jsonString = this.GetJson();
            return jsonString;
        }
    }
}

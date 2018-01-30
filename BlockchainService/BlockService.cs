using System;
using System.Runtime.Serialization;

namespace BlockchainService
{
    /// <summary>
    /// Блок данных.
    /// </summary>
    [DataContract]
    public class BlockService
    {
        /// <summary>
        /// Версия спецификации блока.
        /// </summary>
        [DataMember]
        public int Version { get; set; }

        /// <summary>
        /// Момент создания блока.
        /// </summary>
        [DataMember]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Хеш блока.
        /// </summary>
        [DataMember]
        public string Hash { get; set; }

        /// <summary>
        /// Хеш предыдущего блока.
        /// </summary>
        [DataMember]
        public string PreviousHash { get; set; }

        /// <summary>
        /// Данные блока.
        /// </summary>
        [DataMember]
        public string Data { get; set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего блок.
        /// </summary>
        [DataMember]
        public string User { get; set; }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Уникальный код. </returns>
        public override string ToString()
        {
            return Data;
        }
    }
}
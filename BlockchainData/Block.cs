using System;
using System.Runtime.Serialization;

namespace BlockchainData
{
    [DataContract]
    public class Block
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int BlockId { get; set; }

        /// <summary>
        /// Версия спецификации блока.
        /// </summary>
        [DataMember]
        public int Version { get; set; }

        /// <summary>
        /// Момент создания блока.
        /// </summary>
        [DataMember]
        [DateTimeKind(DateTimeKind.Utc)]
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
    }
}

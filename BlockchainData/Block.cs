using System;

namespace BlockchainData
{
    public class Block
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public int BlockId { get; set; }

        /// <summary>
        /// Версия спецификации блока.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Псевдоуникальный 128-битный идентификатор.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Момент создания блока.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Хеш блока.
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Хеш предыдущего блока.
        /// </summary>
        public string PreviousHash { get; set; }

        /// <summary>
        /// Данные блока.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Идентификатор пользователя, создавшего блок.
        /// </summary>
        public string User { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BlockchainExplorerWeb.Models
{
    /// <summary>
    /// Данные блока.
    /// </summary>
    [DataContract]
    public class Data
    {
        /// <summary>
        /// Информация.
        /// </summary>
        [DataMember]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Хеш блока.
        /// </summary>
        [DataMember]
        public string Hash { get; set; }

        /// <summary>
        /// Тип данных.
        /// </summary>
        [DataMember]
        public Enums.DataType Type { get; set; }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Информация. </returns>
        public override string ToString()
        {
            return Content;
        }
    }
}
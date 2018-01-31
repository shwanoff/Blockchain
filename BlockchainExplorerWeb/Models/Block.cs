using BlockchainExplorerWeb.Controllers;
using System;
using System.Runtime.Serialization;

namespace BlockchainExplorerWeb.Models
{
    /// <summary>
    /// Блок данных.
    /// </summary>
    [DataContract]
    public class Block
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
        /// Данные пользователя.
        /// </summary>
        [DataMember]
        public string User { get; set; }

        /// <summary>
        /// Десериализованный экземпляр пользователя.
        /// </summary>
        public User UserObject
        {
            get
            {
                var user = Api.Deserialize<User>(User);
                return user;
            }
        }

        /// <summary>
        /// Десериализованный экземпляр данных.
        /// </summary>
        public Data DataObject
        {
            get
            {
                var data = Api.Deserialize<Data>(Data);
                return data;
            }
        }

        /// <summary>
        /// Является ли данный блок исходным.
        /// </summary>
        public bool GenesisBlock
        {
            get
            {
                if(Hash == "71e12e272ea682f5e1d60fc5cf5e8ec776df33e38ac8f3f6e6b9ab5eeedde245")
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Хеш блока. </returns>
        public override string ToString()
        {
            return Hash;
        }
    }
}
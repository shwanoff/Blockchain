using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blockchain
{
    /// <summary>
    /// Служебный класс для десериализации ответа на получение всех блоков.
    /// </summary>
    [DataContract]
    public class GetChainResultRoot
    {
        /// <summary>
        /// Список блоков.
        /// </summary>
        [DataMember]
        public List<BlockchainData.Block> GetChainResult { get; set; }
    }
}

using BlockchainExplorerWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace BlockchainExplorerWeb.Models
{
    /// <summary>
    /// Цепочка блоков.
    /// </summary>
    [DataContract]
    public class Chain
    {
        /// <summary>
        /// Количество блоков в цепочке.
        /// </summary>
        public int Length => GetChainResult.Count;

        // TODO: Неправильное имя из-за десериализации данных. Исправить в будущем.
        /// <summary>
        /// Список блоков.
        /// </summary>
        [DataMember]
        public List<Block> GetChainResult { get; set; }

        /// <summary>
        /// Получит новый экземпляр цепочки блоков.
        /// </summary>
        public Chain()
        {
            Sync();
        }

        /// <summary>
        /// Получить блок по хешу.
        /// </summary>
        /// <param name="hash"> Хеш. </param>
        /// <returns> Найденый блок. null если блок не найден. </returns>
        public Block GetBlock(string hash)
        {
            var block = GetChainResult.SingleOrDefault(b => b.Hash == hash);
            return block;
        }

        /// <summary>
        /// Выполнить синхронизацию данных.
        /// </summary>
        public void Sync()
        {
            var api = new Api();
            var chain = api.GetChain();

            if (chain != null)
            {
                GetChainResult = chain.GetChainResult;
            }
            else
            {
                throw new ArgumentNullException(nameof(chain), "Не удалось получить цепочку блоков из api службы.");
            }
        }

        /// <summary>
        /// Добавить блок. 
        /// </summary>
        /// <param name="data"> Данные добавляемые в блок. </param>
        /// <returns> Добавленный блок. null если блок не был добавлен. </returns>
        public Block AddBlock(string data)
        {
            var api = new Api();
            var success = api.AddData(data);
            if (success)
            {
                var chain = api.GetChain();
                GetChainResult = chain.GetChainResult;
                return chain.GetChainResult.Last(); // TODO: Дичь! Но пока сойдет из-за нехватки времени.
            }

            return null;
        }

        /// <summary>
        /// Приведение объекта к строке. 
        /// </summary>
        /// <returns> Количество блоков в цепочке. </returns>
        public override string ToString()
        {
            return $"Chain {Length} blocks";
        }
    }
}
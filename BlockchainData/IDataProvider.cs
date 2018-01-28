#define CONTRACTS_FULL

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace BlockchainData
{
    /// <summary>
    /// Базовый интерфейс, который должен реализовывать провайдер данных.
    /// </summary>
    [ContractClass(typeof(DataProviderContract))]
    public interface IDataProvider
    {
        /// <summary>
        /// Добавление блока данных.
        /// </summary>
        /// <param name="version"> Версия блока. </param>
        /// <param name="code"> Уникальный код блока. </param>
        /// <param name="createdOn"> Дата создания блока. </param>
        /// <param name="hash"> Хеш блока. </param>
        /// <param name="previousHash"> Хеш предыдущего блока. </param>
        /// <param name="data"> Данные блока. </param>
        /// <param name="user"> Данные о пользователе. </param>
        void AddBlock(int version, DateTime createdOn, string hash, string previousHash, string data, string user);

        /// <summary>
        /// Получить все блоки.
        /// </summary>
        /// <returns> Список всех блоков. </returns>
        List<Block> GetBlocks();

        /// <summary>
        /// Очистить хранилище. Удаление всех блоков.
        /// </summary>
        void Crear();
    }

    /// <summary>
    /// Класс, определяющий контракты для интерфейса.
    /// </summary>
    [ContractClassFor(typeof(IDataProvider))]
    internal abstract class DataProviderContract : IDataProvider
    {
        public void AddBlock(int version, DateTime createdOn, string hash, string previousHash, string data, string user)
        {
            Contract.Requires<ArgumentException>(version < 0, $"Не возможно создать блок данных провайдера данных. Версия не может быть меньше нуля.");
            Contract.Requires<ArgumentNullException>(string.IsNullOrEmpty(hash), $"Не возможно создать блок данных провайдера данных. Хеш не может быть пустым или равным null.");
            Contract.Requires<ArgumentNullException>(string.IsNullOrEmpty(previousHash), $"Не возможно создать блок данных провайдера данных. Предыдущий хеш не может быть пустым или равным null.");
            Contract.Requires<ArgumentNullException>(string.IsNullOrEmpty(data), $"Не возможно создать блок данных провайдера данных. Данные не могут быть пустыми или равными null.");
            Contract.Requires<ArgumentNullException>(string.IsNullOrEmpty(user), $"Не возможно создать блок данных провайдера данных. Данные пользователя не могут быть пустыми или равными null.");
        }

        public void Crear()
        {
            
        }

        public List<Block> GetBlocks()
        {
            Contract.Ensures(Contract.Result<List<Block>>() != null);
            return new List<Block>();
        }

    }
}

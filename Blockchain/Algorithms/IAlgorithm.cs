#define CONTRACTS_FULL

using System;
using System.Diagnostics.Contracts;

namespace Blockchain.Algorithms
{
    /// <summary>
    /// Интерфейс, который должен быть реализован алгоритмом хеширования.
    /// </summary>
    [ContractClass(typeof(AlgorithmContract))]
    public interface IAlgorithm
    {
        /// <summary>
        /// Получить хеш из строки теста.
        /// </summary>
        /// <param name="data">Хешируемый текст.</param>
        /// <returns> Хеш от строки. </returns>
        string GetHash(string data);

        /// <summary>
        /// Получить хеш из компонента поддерживающего хеширование.
        /// </summary>
        /// <param name="data">Хешируемый компонент.</param>
        /// <returns> Хеш от компонента. </returns>
        string GetHash(IHashable data);
    }


    /// <summary>
    /// Класс, определяющий контракты для интерфейса.
    /// </summary>
    [ContractClassFor(typeof(IAlgorithm))]
    internal abstract class AlgorithmContract : IAlgorithm
    {
        string IAlgorithm.GetHash(string data)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(data), $"Не возможно выполнить хеширование. Аргумент {nameof(data)} не содержит данных.");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
            return string.Empty;
        }

        string IAlgorithm.GetHash(IHashable data)
        {
            Contract.Requires<ArgumentNullException>(data != null, $"Не возможно выполнить хеширование. Аргумент {nameof(data)} равен null.");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
            return string.Empty;
        }
    }
}

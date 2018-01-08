using System;
using System.Diagnostics.Contracts;

namespace Blockchain.Algorithms
{
    /// <summary>
    /// Класс, содержащий вспомогательные методы упрощающие работу с алгоритмами хеширования.
    /// </summary>
    public static class AlgorithmHelper
    {
        /// <summary>
        /// Получить хеш компонента.
        /// </summary>
        /// <param name="component">Компонент, поддерживающий хеширование.</param>
        /// <param name="algorithm">Алгоритм хеширования.</param>
        /// <returns>Хеш компонента.</returns>
        public static string GetHash(this IHashable component, IAlgorithm algorithm)
        {
            Contract.Requires<ArgumentNullException>(component != null, "Не возможно выполнить хеширование. Аргумент равен null.");
            Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

            var hash = algorithm.GetHash(component);
            return hash;
        }
    }
}

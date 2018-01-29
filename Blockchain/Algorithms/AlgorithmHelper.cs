using Blockchain.Exceptions;

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
        public static string GetHash(this IHashable component, IAlgorithm algorithm = null)
        {
            // Если не был передан алгоритм хеширования, то получаем алгоритм по умолчанию.
            if (algorithm == null)
            {
                algorithm = GetDefaultAlgorithm();
            }

            var hash = algorithm.GetHash(component);
            return hash;
        }

        /// <summary>
        /// Получить хеш строки.
        /// </summary>
        /// <param name="text"> Хешируемая строка. </param>
        /// <param name="algorithm"> Алгоритм хеширования. </param>
        /// <returns> Хеш строки. </returns>
        public static string GetHash(this string text, IAlgorithm algorithm = null)
        {
            // Если не был передан алгоритм хеширования, то получаем алгоритм по умолчанию.
            if(algorithm == null)
            {
                algorithm = GetDefaultAlgorithm();
            }

            var hash = algorithm.GetHash(text);
            return hash;
        }

        /// <summary>
        /// Получить используемый алгоритм хеширования.
        /// </summary>
        /// <returns> Алгоритм хеширования. </returns>
        public static IAlgorithm GetDefaultAlgorithm()
        {
            // TODO: Реализовать получение алгоритма по умолчанию из конфигурационного файла.
            return new Sha256();
        }
    }
}

namespace Blockchain.Algorithms
{
    /// <summary>
    /// Интерфейс, который должен быть реализован алгоритмом хеширования.
    /// </summary>
    public interface IAlgorithm
    {
        /// <summary>
        /// Получить хеш из строки теста.
        /// </summary>
        /// <param name="data">Хешируемый текст.</param>
        /// <returns>Хеш от строки.</returns>
        string GetHash(string data);

        /// <summary>
        /// Получить хеш из компонента поддерживающего хеширование.
        /// </summary>
        /// <param name="data">Хешируемый компонент.</param>
        /// <returns>Хеш от компонента.</returns>
        string GetHash(IHashable data);
    }
}

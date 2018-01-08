namespace Blockchain.Algorithms
{
    /// <summary>
    /// Интерфейс для объектов которые могут быть хешированы.
    /// </summary>
    public interface IHashable
    {
        /// <summary>
        /// Получить данные из объекта, на основе которых будет строиться хеш.
        /// </summary>
        /// <returns>Хешируемые данные.</returns>
        string GetStringForHash();
    }
}

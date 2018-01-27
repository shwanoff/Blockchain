namespace Blockchain
{
    /// <summary>
    /// Тип хранимых в блоке данных.
    /// </summary>
    public enum DataType : byte
    {
        /// <summary>
        /// Информация.
        /// </summary>
        Content = 1,

        /// <summary>
        /// Сведения о пользователях.
        /// </summary>
        User = 2,

        /// <summary>
        /// Сведения о сервере.
        /// </summary>
        Node = 3
    }
}

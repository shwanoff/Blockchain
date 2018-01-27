namespace BlockchainData
{
    /// <summary>
    /// Вспомогательный класс, содержащий методы для упрощения написания кода.
    /// </summary>
    public static class DataProviderHelper
    {
        /// <summary>
        /// Получить провайдер данных по умолчанию.
        /// </summary>
        /// <returns></returns>
        public static IDataProvider GetDefaultDataProvider()
        {
            return new SqlDataProvider();
        }
    }
}

using Blockchain;

namespace BlockchainService
{
    /// <summary>
    /// 
    /// </summary>
    public class Instance
    {
        /// <summary>
        /// Объект синхронизации, необходим для безопасности при многопоточном использовании.
        /// </summary>
        private static object _sync = new object();

        /// <summary>
        /// Основной объект, в котором будет храниться уникальный экземпляр класса. 
        /// </summary>
        private static Instance _instance;

        /// <summary>
        /// Цепочка блоков.
        /// </summary>
        private Chain _chain;

        /// <summary>
        /// Цепочка блоков.
        /// </summary>
        public Chain Chain
        {
            get
            {
                return _chain;
            }
        }

        /// <summary>
        /// Создать новый экземпляр класса Instance.
        /// </summary>
        private Instance()
        {
            _chain = new Chain();

            
        }

        /// <summary>
        /// Получить экземпляр класса.
        /// </summary>
        /// <returns> Уникальный экземпляр класса. </returns>
        public static Instance Get()
        {
            lock (_sync) // Используется чтобы избежать одновременного доступа критической секции из разных потоков.
            {
                // Если экземпляр еще не инициализирован - выполняем инициализацию. 
                // Иначе возвращаем имеющийся экземпляр.
                if (_instance == null)
                {
                    _instance = new Instance();
                }
            }
            return _instance;
        }
    }
}
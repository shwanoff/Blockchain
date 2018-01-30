using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlockchainService
{
    /// <summary>
    /// Реализация контракта службы. 
    /// </summary>
    public class BlockchainService : ServiceBase, IBlockchainService
    {
        /// <summary>
        /// Добавление данных.
        /// </summary>
        /// <param name="text"> Содержимое данных. </param>
        /// <returns> Добавленных блок. </returns>
        public async Task<BlockService> AddDataAsync(string text)
        {
            return await Task.Run(() => AddData(text));
        }

        /// <summary>
        /// Добавление пользователя. 
        /// </summary>
        /// <param name="login"> Логин. </param>
        /// <param name="password"> Пароль. </param>
        /// <param name="role"> Права доступа. </param>
        /// <returns> Добавленных блок с данными о пользователе. </returns>
        public async Task<BlockService> AddUserAsync(string login, string password, string role)
        {
            return await Task.Run(() => AddUser(login, password, role));
        }

        /// <summary>
        /// Добавление хоста.
        /// </summary>
        /// <param name="ip"> Адрес хоста в сети. </param>
        /// <returns> Добавленных блок с данными о хосте. </returns>
        public async Task<BlockService> AddHostAsync(string ip)
        {
            return await Task.Run(() => AddHost(ip));
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login"> Логин. </param>
        /// <param name="password"> Пароль. </param>
        /// <returns> Авторизованный пользователь системы. </returns>
        public async Task<User> LoginAsync(string login, string password)
        {
            return await Task.Run(() => Login(login, password));
        }
        
        /// <summary>
        /// Получение всех блоков цепочки.
        /// </summary>
        /// <returns> Список блоков цепочки блоков. </returns>
        public async Task<List<BlockService>> GetChainAsync()
        {
            return await Task.Run(() => GetBlocks());
        }
    }
}

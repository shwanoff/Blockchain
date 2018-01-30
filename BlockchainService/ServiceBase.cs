using Blockchain;
using System;
using System.Collections.Generic;

namespace BlockchainService
{
    /// <summary>
    /// Базовый класс для реализации контракта службы.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        /// Добавление данных.
        /// </summary>
        /// <param name="text"> Содержимое данных. </param>
        /// <returns> Добавленных блок. </returns>
        protected BlockService AddData(string text)
        {
            try
            {
                var block = Instance.Get().Chain.AddContent(text);
                var b = ConvertBlock(block);
                return b;
            }
            catch
            {
                throw;
                // TODO: Добавить сообщение об ошибке в сохранение в лог.
            }
        }

        /// <summary>
        /// Добавление пользователя. 
        /// </summary>
        /// <param name="login"> Логин. </param>
        /// <param name="password"> Пароль. </param>
        /// <param name="role"> Права доступа. </param>
        /// <returns> Добавленных блок с данными о пользователе. </returns>
        protected BlockService AddUser(string login, string password, string role)
        {
            try
            {
                if (int.TryParse(role, out int parseRole))
                {
                    if (Enum.IsDefined(typeof(UserRole), parseRole))
                    {
                        var r = (UserRole)parseRole;

                        var block = Instance.Get().Chain.AddUser(login, password, r);
                        var b = ConvertBlock(block);
                        return b;
                    }
                }

                throw new ArgumentException("Неизвестная роль пользователя.", nameof(role));
            }
            catch
            {
                throw;
                // TODO: Добавить сообщение об ошибке в сохранение в лог.
            }
        }

        /// <summary>
        /// Добавление хоста.
        /// </summary>
        /// <param name="ip"> Адрес хоста в сети. </param>
        /// <returns> Добавленных блок с данными о хосте. </returns>
        protected BlockService AddHost(string ip)
        {
            try
            {
                var block = Instance.Get().Chain.AddHost(ip);
                var b = ConvertBlock(block);
                return b;
            }
            catch
            {
                throw;
                // TODO: Добавить сообщение об ошибке в сохранение в лог.
            }
        }

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login"> Логин. </param>
        /// <param name="password"> Пароль. </param>
        /// <returns> Авторизованный пользователь системы. </returns>
        protected User Login(string login, string password)
        {
            try
            {
                var user = Instance.Get().Chain.LoginUser(login, password);
                if (user == null)
                {
                    return null;
                }
                var u = new User()
                {
                    Login = user.Login,
                    Role = user.Role
                };
                return u;
            }
            catch
            {
                throw;
                // TODO: Добавить сообщение об ошибке в сохранение в лог.
            }
        }

        /// <summary>
        /// Получение всех блоков цепочки.
        /// </summary>
        /// <returns> Список блоков цепочки блоков. </returns>
        protected List<BlockService> GetBlocks()
        {
            try
            {

                var blocks = Instance.Get().Chain.BlockChain;
                var result = new List<BlockService>();
                foreach (var block in blocks)
                {
                    var b = ConvertBlock(block);
                    result.Add(b);
                }

                return result;
            }
            catch
            {
                throw;
                // TODO: Добавить сообщение об ошибке в сохранение в лог.
            }
        }
        
        /// <summary>
        /// Преобразование блока цепочки блоков в блок службы.
        /// </summary>
        /// <param name="block"> Блок цепочки блоков. </param>
        /// <returns> Блок службы. </returns>
        private BlockService ConvertBlock(Block block)
        {
            if(block == null)
            {
                throw new ArgumentNullException(nameof(block), "Блок цепочки блоков не может быть равен null");
            }

            if(!block.IsCorrect())
            {
                throw new ArgumentException("Некорректный блок цепочки блоков.", nameof(block));
            }

            var b = new BlockService()
            {
                Version = block.Version,
                CreatedOn = block.CreatedOn,
                Hash = block.Hash,
                PreviousHash = block.PreviousHash,
                Data = block.Data.GetJson(),
                User = block.User.GetJson()
            };

            return b;
        }
    }
}
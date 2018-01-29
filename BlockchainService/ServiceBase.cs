using Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainService
{
    public abstract class ServiceBase
    {
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
        protected BlockService AddUser(string login, string password, string role, string code)
        {
            try
            {
                if (int.TryParse(role, out int parseRole))
                {
                    if (Enum.IsDefined(typeof(UserRole), parseRole))
                    {
                        var r = (UserRole)parseRole;

                        if(Guid.TryParse(code, out Guid guid))
                        {
                            var block = Instance.Get().Chain.AddUser(login, password, r, guid);
                            var b = ConvertBlock(block);
                            return b;
                        }
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



        private BlockService ConvertBlock(Block block)
        {
            //TODO: Добавить проверку.

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
using Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlockchainService
{
    public abstract class ServiceBase
    {
        protected Block AddData(string text)
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
        protected Block AddUser(string login, string password, string role)
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

        protected Block AddHost(string ip)
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

        protected List<Block> GetBlocks()
        {
            try
            {
                var blocks = Instance.Get().Chain.BlockChain;
                var result = new List<Block>();
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



        private Block ConvertBlock(Blockchain.Block block)
        {
            //TODO: Добавить проверку.

            var b = new Block()
            {
                Version = block.Version,
                Code = block.Code.ToString(),
                CreatedOn = block.CreatedOn,
                Hash = block.Hash,
                PreviousHash = block.PreviousHash,
                Data = block.Data.Content,
                User = block.User.Login
            };
            return b;
        }
    }
}
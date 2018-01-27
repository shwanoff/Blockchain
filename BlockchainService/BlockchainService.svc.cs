using System.Collections.Generic;
using System.Threading.Tasks;
using Blockchain;

namespace BlockchainService
{
    public class BlockchainService : IBlockchainService
    {
        public Block AddData(string text)
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

        public async Task<Block> AddDataAsync(string text)
        {
            return await Task.Run(() => AddData(text));
        }

        public bool AddUser(string login, string password, UserRole role = UserRole.Reader)
        {
            bool result = false;
            try
            {
                Instance.Get().Chain.AddUser(login, password, role);
                result = true;
            }
            catch
            {
                result = false;
                throw;
                // TODO: Добавить сообщение об ошибке в сохранение в лог.
            }

            return result;
        }

        public async Task<bool> AddUserAsync(string login, string password, UserRole role = UserRole.Reader)
        {
            return await Task.Run(() => AddUser(login, password, role));
        }

        public List<Block> GetBlocks()
        {
            try
            {
                var blocks = Instance.Get().Chain.BlockChain;
                var result = new List<Block>();
                foreach(var block in blocks)
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

        public async Task<List<Block>> GetBlocksAsync()
        {
            return await Task.Run(() => GetBlocks());
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

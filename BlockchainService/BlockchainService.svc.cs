using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blockchain;

namespace BlockchainService
{
    public class BlockchainService : ServiceBase, IBlockchainService
    {
        public async Task<Block> AddDataAsync(string text)
        {
            return await Task.Run(() => AddData(text));
        }

        public async Task<Block> AddUserAsync(string login, string password, string role)
        {
            return await Task.Run(() => AddUser(login, password, role));
        }

        public async Task<Block> AddHostAsync(string ip)
        {
            return await Task.Run(() => AddHost(ip));
        }

        

        public async Task<User> LoginAsync(string login, string password)
        {
            return await Task.Run(() => Login(login, password));
        }

        

        public async Task<List<Block>> GetChainAsync()
        {
            return await Task.Run(() => GetBlocks());
        }
    }
}

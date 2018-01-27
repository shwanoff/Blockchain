using Blockchain;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;

namespace BlockchainService
{
    [ServiceContract]
    public interface IBlockchainService
    {
        
        bool AddUser(string login, string password, UserRole role = UserRole.Reader);

        [OperationContract]
        Task<bool> AddUserAsync(string login, string password, UserRole role = UserRole.Reader);

        
        Block AddData(string text);

        [OperationContract]
        Task<Block> AddDataAsync(string text);

        
        List<Block> GetBlocks();

        [OperationContract]
        Task<List<Block>> GetBlocksAsync();
    }
}

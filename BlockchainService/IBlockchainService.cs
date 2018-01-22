using Blockchain;
using System.ServiceModel;

namespace BlockchainService
{
    [ServiceContract]
    public interface IBlockchainService
    {
        [OperationContract]
        bool AddUser(string login, string password, UserRole role = UserRole.Reader);

        [OperationContract]
        bool AddData(string text);
    }
}

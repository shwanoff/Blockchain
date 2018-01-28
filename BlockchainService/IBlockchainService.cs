using Blockchain;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace BlockchainService
{
    [ServiceContract]
    public interface IBlockchainService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AddHost/{ip}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<Block> AddHostAsync(string ip);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AddUserAsync/{login}&{password}&{role}&{code}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<Block> AddUserAsync(string login, string password, string role, string code);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AddData/{text}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<Block> AddDataAsync(string text);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Login/{login}&{password}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<User> LoginAsync(string login, string password);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetChain/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<List<Block>> GetChainAsync();
    }
}

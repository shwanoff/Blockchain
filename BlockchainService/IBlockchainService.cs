using Blockchain;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace BlockchainService
{
    /// <summary>
    /// Контракт службы.
    /// </summary>
    [ServiceContract]
    public interface IBlockchainService
    {

        /// <summary>
        /// Добавление хоста.
        /// </summary>
        /// <param name="ip"> Адрес хоста в сети. </param>
        /// <returns> Добавленных блок с данными о хосте. </returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AddHost/{ip}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<BlockService> AddHostAsync(string ip);

        /// <summary>
        /// Добавление пользователя. 
        /// </summary>
        /// <param name="login"> Логин. </param>
        /// <param name="password"> Пароль. </param>
        /// <param name="role"> Права доступа. </param>
        /// <returns> Добавленных блок с данными о пользователе. </returns>
        // TODO: Огромная дыра, нешифрованый пароль в get. Исправить.
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AddUser/{login}&{password}&{role}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<BlockService> AddUserAsync(string login, string password, string role);

        /// <summary>
        /// Добавление данных.
        /// </summary>
        /// <param name="text"> Содержимое данных. </param>
        /// <returns> Добавленных блок. </returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/AddData/{text}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<BlockService> AddDataAsync(string text);

        /// <summary>
        /// Авторизация пользователя.
        /// </summary>
        /// <param name="login"> Логин. </param>
        /// <param name="password"> Пароль. </param>
        /// <returns> Авторизованный пользователь системы. </returns>
        // TODO: Огромная дыра, нешифрованый пароль в get. Исправить.
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/Login/{login}&{password}", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<User> LoginAsync(string login, string password);

        /// <summary>
        /// Получение всех блоков цепочки.
        /// </summary>
        /// <returns> Список блоков цепочки блоков. </returns>
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/GetChain/", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Task<List<BlockService>> GetChainAsync();
    }
}

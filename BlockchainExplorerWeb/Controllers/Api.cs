using BlockchainExplorerWeb.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;

namespace BlockchainExplorerWeb.Controllers
{
    /// <summary>
    /// Класс для упрощения взаимодействия с API службы.
    /// </summary>
    public class Api
    {
        /// <summary>
        /// Адрес глобальной службы.
        /// </summary>
        private string _address = "http://blockchain-dev-as.azurewebsites.net";

        /// <summary>
        /// Создать новый экземпляр цепочки блоков.
        /// </summary>
        /// <returns> Цепочка блоков. </returns>
        public Chain GetChain()
        {
            var json = SendRequest("getchain", "");
            var chain = Deserialize<Chain>(json);
            return chain;
        }

        /// <summary>
        /// Добавить данные в цепочку блоков.
        /// </summary>
        /// <param name="data"> Данные, которые будут добавлены в цепочку блоков. </param>
        /// <returns> Успешность добавления данных. </returns>
        public bool AddData(string data)
        {
            var json = SendRequest("adddata", data);
            return !string.IsNullOrEmpty(json);
        }

        /// <summary>
        /// Отправить запрос к api службы блокчейн.
        /// </summary>
        /// <param name="method"> Вызываемый метод службы. </param>
        /// <param name="data"> Параметры метода передаваемые через &. </param>
        /// <returns> Json ответ сервера. null если нет ответа. </returns>
        private string SendRequest(string method, string data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(20);

                // http://localhost:28451/BlockchainService.svc/api/getchain/ пример запроса.
                string repUri = $"{_address}/BlockchainService.svc/api/{method}/{data}";
                var response = client.GetAsync(repUri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }

            return null;
        }

        /// <summary>
        /// Десериализовать json данные в объект.
        /// </summary>
        /// <typeparam name="T"> Тип данных, в который будет выполняться десериализация. </typeparam>
        /// <param name="json"> Json данные. </param>
        /// <returns> Десериализованный экземпляр объекта. </returns>
        public static T Deserialize<T>(string json)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var deserializer = new DataContractJsonSerializer(typeof(T));
                var requestResult = (T)deserializer.ReadObject(ms);

                return requestResult;
            }
        }
    }
}
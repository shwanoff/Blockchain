using Blockchain.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using Blockchain.Exceptions;
using BlockchainData;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;

namespace Blockchain
{
    /// <summary>
    /// Цепочка блоков.
    /// </summary>
    public class Chain
    {
        /// <summary>
        /// Алгоритм хеширования.
        /// </summary>
        private IAlgorithm _algorithm = AlgorithmHelper.GetDefaultAlgorithm();

        /// <summary>
        /// Провайдер данных.
        /// </summary>
        private IDataProvider _dataProvider = DataProviderHelper.GetDefaultDataProvider();

        /// <summary>
        /// Список, содержащий в себе все блоки.
        /// </summary>
        private List<Block> _blockChain = new List<Block>();

        /// <summary>
        /// Список IP адресов хостов.
        /// </summary>
        private List<string> _hosts = new List<string>();

        /// <summary>
        /// Список пользователей.
        /// </summary>
        private List<User> _users = new List<User>();

        /// <summary>
        /// Список данных.
        /// </summary>
        private List<Data> _data = new List<Data>();

        /// <summary>
        /// Цепочка блоков.
        /// </summary>
        public IEnumerable<Block> BlockChain => _blockChain;

        /// <summary>
        /// Крайний блок в цепочке блоков.
        /// </summary>
        public Block PreviousBlock => _blockChain.Last();

        /// <summary>
        /// Информационные данные.
        /// </summary>
        public IEnumerable<Data> Content => _data;

        /// <summary>
        /// Пользователи системы.
        /// </summary>
        public IEnumerable<User> Users => _users;

        /// <summary>
        /// IP адреса серверов.
        /// </summary>
        public IEnumerable<string> Hosts => _hosts;

        public int Length => _blockChain.Count;

        /// <summary>
        /// Создать новый экземпляр цепочки блоков.
        /// </summary>
        public Chain()
        {
            // Получаем цепочки блоков.
            var globalChain = GetGlobalChein();
            var localChain = GetLocalChain();

            // Создаем новую, если ни одной цепочки не найдено.
            if (globalChain == null && localChain == null)
            {
                CreateNewBlockChain();
            }
            else
            {
                // Если глобальная цепочка больше, синхронизируемся с ней. Иначе с локальной, если они совпадают.
                if(globalChain?.Length > localChain?.Length)
                {
                    ReplaceLocalChainFromGlobalChain(globalChain);
                }
                else
                {
                    // Для проверки совпадения цепочек, достаточно проверить хеши крайних блоков.
                    // Если не совпадают, выбираем глобальную цепочку. Иначе загружаем локальную.
                    if(globalChain?.PreviousBlock?.Hash != localChain?.PreviousBlock?.Hash)
                    {
                        ReplaceLocalChainFromGlobalChain(globalChain);
                    }
                    else
                    {
                        LoadDataFromLocalChain(localChain);
                    }
                }
            }

            if (!CheckCorrect())
            {
                throw new MethodResultException(nameof(Chain), "Ошибка создания цепочки блоков. Цепочка некорректна.");
            }
        }

        /// <summary>
        /// Получить данные из локальной цепочки.
        /// </summary>
        /// <param name="localChain"> Локальная цепочка блоков. </param>
        private void LoadDataFromLocalChain(Chain localChain)
        {
            if(localChain == null)
            {
                throw new MethodRequiresException(nameof(localChain), "Локальная цепочка блоков не может быть равна null.");
            }

            foreach(var block in localChain._blockChain)
            {
                _blockChain.Add(block);
                AddDataInList(block);
                SendBlockToGlobalChain(block);
            }
        }

        /// <summary>
        /// Заменить локальную цепочку данных блоками из глобальной цепочки.
        /// </summary>
        /// <param name="globalChain"> Глобавльная цепочка данных. </param>
        private void ReplaceLocalChainFromGlobalChain(Chain globalChain)
        {
            if(globalChain == null)
            {
                throw new MethodRequiresException(nameof(globalChain), "Глобальная цепочка блоков не может быть равна null.");
            }

            // TODO: Очень топорная синхронизация (полная замена). Необходимо разработать алгоритм слияния.
            _dataProvider.Crear();

            foreach (var block in globalChain._blockChain)
            {
                AddBlock(block);
            }
        }

        /// <summary>
        /// Создание цепочки блоков из списка блоков провайдера данных.
        /// </summary>
        /// <param name="blocks"> Блоки провайдера данных. </param>
        private Chain(List<BlockchainData.Block> blocks)
        {
            if (blocks == null)
            {
                throw new MethodRequiresException(nameof(blocks), "Список блоков провайдера данных не может быть равным null.");
            }

            foreach (var block in blocks)
            {
                var b = new Block(block);
                _blockChain.Add(b);

                AddDataInList(b);
            }

            if (!CheckCorrect())
            {
                throw new MethodResultException(nameof(Chain), "Ошибка создания цепочки блоков. Цепочка некорректна.");
            }
        }

        /// <summary>
        /// Создание цепочки блоков из блоков данных.
        /// </summary>
        /// <param name="blocks"> Список блоков данных. </param>
        private Chain(List<Block> blocks)
        {
            if (blocks == null)
            {
                throw new MethodRequiresException(nameof(blocks), "Список блоков не может быть равным null.");
            }

            foreach (var block in blocks)
            {
                _blockChain.Add(block);

                AddDataInList(block);
            }

            if (!CheckCorrect())
            {
                throw new MethodResultException(nameof(Chain), "Ошибка создания цепочки блоков. Цепочка некорректна.");
            }
        }

        /// <summary>
        /// Создать новую пустую цепочку блоков.
        /// </summary>
        private void CreateNewBlockChain()
        {
            _dataProvider.Crear();
            _blockChain = new List<Block>();
            var genesisBlock = Block.GetGenesisBlock(_algorithm);
            AddBlock(genesisBlock);
        }

        /// <summary>
        /// Проверить корректность цепочки блоков.
        /// </summary>
        /// <returns> Корректность цепочки блоков. true - цепочка блоков корректна, false - цепочка некорректна. </returns>
        public bool CheckCorrect()
        {
            foreach (var block in _blockChain)
            {
                if (!block.IsCorrect(_algorithm))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Получить глобальную цепочку блоков.
        /// </summary>
        /// <returns> Цепочка блоков. </returns>
        private Chain GetGlobalChein()
        {
            _hosts.Add("http://blockchain-dev-as.azurewebsites.net"); // TODO: Сделай получение из конфиг файла.

            foreach (var host in Hosts)
            {
                // TODO: Здесь нужно будет переделать. Предварительно выбирается хост с самой большой цепочкой блоков и уже он синхранизуется.
                var blocks = GetBlocksFromHosts(host);
                if (blocks.Count > 0)
                {
                    var chain = new Chain(blocks);
                    return chain;
                }
            }

            return null;
        }

        /// <summary>
        /// Получение цепочки блоков из локального хранилища.
        /// </summary>
        /// <returns></returns>
        private Chain GetLocalChain()
        {
            var blocks = _dataProvider.GetBlocks();
            if (blocks.Count > 0)
            {
                var chain = new Chain(blocks);
                return chain;
            }

            return null;
        }

        /// <summary>
        /// Добавить данные в цепочку блоков.
        /// </summary>
        /// <param name="text"> Добавляемые данные. </param>
        public Block AddContent(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new MethodRequiresException(nameof(text), "Текст не должен быть пустым или равен null.");
            }

            var data = new Data(text, DataType.Content);
            var block = new Block(PreviousBlock, data, User.GetCurrentUser(), _algorithm);

            AddBlock(block);

            return block;
        }

        /// <summary>
        /// Добавить данные о пользователе в цепочку.
        /// </summary>
        /// <param name="login"> Имя пользователя. </param>
        /// <param name="password"> Пароль пользователя. </param>
        /// <param name="role"> Права доступа пользователя. </param>
        public Block AddUser(string login, string password, UserRole role = UserRole.Reader)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new MethodRequiresException(nameof(login), "Логин не может быть пустым или равным null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new MethodRequiresException(nameof(password), "Пароль не может быть пустым или равным null.");
            }

            if (Users.Any(b => b.Login == login))
            {
                return null;
            }

            var user = new User(login, password, role);
            var data = user.GetData();
            var block = new Block(PreviousBlock, data, User.GetCurrentUser());
            AddBlock(block);
            return block;
        }

        /// <summary>
        /// Добавление сведений об адресе глобальной цепочки.
        /// </summary>
        /// <param name="ip"> Адрес хоста в сети. </param>
        /// <returns> Блок данных с адресом хоста. </returns>
        public Block AddHost(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                throw new MethodRequiresException(nameof(ip), "IP адрес хоста не может быть пустым или равным null.");
            }

            // TODO: Добавить проверку формата ip адреса

            var data = new Data(ip, DataType.Node);
            var block = new Block(PreviousBlock, data, User.GetCurrentUser(), _algorithm);
            AddBlock(block);

            return block;
        }

        /// <summary>
        /// Авторизоваться пользователем сети.
        /// </summary>
        /// <param name="login"> Логин. </param>
        /// <param name="password"> Пароль. </param>
        /// <returns> Пользователь сети. null если не удалось авторизоваться. </returns>
        public User LoginUser(string login, string password)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new MethodRequiresException(nameof(login), "Логин не может быть пустым или равным null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new MethodRequiresException(nameof(password), "Пароль не может быть пустым или равным null.");
            }

            var user = Users.SingleOrDefault(b => b.Login == login);
            if (user == null)
            {
                return null;
            }

            var passwordHash = password.GetHash();
            if (user.Password != passwordHash)
            {
                return null;
            }

            return user;
        }

        /// <summary>
        /// Добавить блок.
        /// </summary>
        /// <param name="block"> Добавляемый блок. </param>
        private void AddBlock(Block block)
        {
            if (!block.IsCorrect())
            {
                throw new MethodRequiresException(nameof(block), "Блок не корректный.");
            }

            // Не добавляем уже существующий блок.
            if(_blockChain.Any(b => b.Hash == block.Hash))
            {
                return;
            }

            // TODO: Реализовать транзакцию.
            _blockChain.Add(block);
            _dataProvider.AddBlock(block.Version, block.CreatedOn, block.Hash, block.PreviousHash, block.Data.GetJson(), block.User.GetJson());
            AddDataInList(block);
            SendBlockToGlobalChain(block);

            if (!CheckCorrect())
            {
                throw new MethodResultException(nameof(Chain), "Была нарушена корректность после добавления блока.");
            }
        }

        /// <summary>
        /// Добавление данных из блоков в списки быстрого доступа.
        /// </summary>
        /// <param name="block"> Блок. </param>
        private void AddDataInList(Block block)
        {
            switch (block.Data.Type)
            {
                case DataType.Content:
                    _data.Add(block.Data);
                    foreach (var host in _hosts)
                    {
                        SendBlockToHosts(host, "AddData", block.Data.Content);
                    }
                    break;
                case DataType.User:
                    var user = new User(block);
                    _users.Add(user);
                    foreach (var host in _hosts)
                    {
                        SendBlockToHosts(host, "AddUser", $"{user.Login}&{user.Password}&{user.Role}");
                    }
                    break;
                case DataType.Node:
                    _hosts.Add(block.Data.Content);
                    foreach (var host in _hosts)
                    {
                        SendBlockToHosts(host, "AddHost", block.Data.Content);
                    }
                    break;
                default:
                    throw new MethodRequiresException(nameof(block), "Неизвестный тип блока.");
            }
        }

        /// <summary>
        /// Добавление данных из блоков в списки быстрого доступа.
        /// </summary>
        /// <param name="block"> Блок. </param>
        private void SendBlockToGlobalChain(Block block)
        {
            switch (block.Data.Type)
            {
                case DataType.Content:
                    foreach (var host in _hosts)
                    {
                        SendBlockToHosts(host, "AddData", block.Data.Content);
                    }
                    break;
                case DataType.User:
                    var user = new User(block);
                    foreach (var host in _hosts)
                    {
                        // TODO: Исправить. Получаем хеш пароля а не пароль. некорректно.
                        SendBlockToHosts(host, "AddUser", $"{user.Login}&{user.Password}&{user.Role}");
                    }
                    break;
                case DataType.Node:
                    _hosts.Add(block.Data.Content);
                    foreach (var host in _hosts)
                    {
                        SendBlockToHosts(host, "AddHost", block.Data.Content);
                    }
                    break;
                default:
                    throw new MethodRequiresException(nameof(block), "Неизвестный тип блока.");
            }
        }

        /// <summary>
        /// Получение всех блоков от хоста через api.
        /// </summary>
        /// <param name="ip"> Адрес хоста в сети. </param>
        /// <returns> Список блоков. </returns>
        private static List<Block> GetBlocksFromHosts(string ip)
        {
            // http://localhost:28451/BlockchainService.svc/api/getchain/ пример запроса.
            var response = SendRequest(ip, "getchain", "");
            var blocks = DeserializeCollectionBlocks(response);
            return blocks;
        }

        /// <summary>
        /// Отправка запроса к api хоста.
        /// </summary>
        /// <param name="ip"> Адрес хоста сети. </param>
        /// <param name="method"> Метод вызываемый у хоста. </param>
        /// <param name="data"> Передаваемые параметры метода через &. </param>
        /// <returns> Json ответ хоста. </returns>
        private static string SendRequest(string ip, string method, string data)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // http://localhost:28451/BlockchainService.svc/api/getchain/ пример запроса.
                string repUri = $"{ip}/BlockchainService.svc/api/{method}/{data}";
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
        /// Формирование списка блоков на основе полученого json ответа хоста.
        /// </summary>
        /// <param name="json"> Json ответ хоста на запрос получения всех блоков. </param>
        /// <returns> Список блоков глобальной цепочки. </returns>
        private static List<Block> DeserializeCollectionBlocks(string json)
        {
            var jsonFormatter2 = new DataContractJsonSerializer(typeof(GetChainResultRoot));

            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var deserializer = new DataContractJsonSerializer(typeof(GetChainResultRoot));
                var requestResult = (GetChainResultRoot)deserializer.ReadObject(ms);

                var result = new List<Block>();
                foreach (var block in requestResult.GetChainResult)
                {
                    result.Add(new Block(block));
                }

                return result;
            }
        }

        /// <summary>
        /// Запрос к api хоста на добавление блока данных.
        /// </summary>
        /// <param name="ip"> Адрес хоста в сети. </param>
        /// <param name="method"> Вызываемый метод хоста. </param>
        /// <param name="data"> Параметры метода хоста через &.</param>
        /// <returns> Успешность выполнения запроса. </returns>
        private bool SendBlockToHosts(string ip, string method, string data)
        {
            var result = SendRequest(ip, method, data);
            var success = !string.IsNullOrEmpty(result);
            return success;
        }
    }
}

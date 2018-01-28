using Blockchain.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using Blockchain.Exceptions;
using BlockchainData;
using System.Net.Http;

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
        private List<string> _nodes = new List<string>();

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
        public IEnumerable<string> Nodes => _nodes;

        /// <summary>
        /// Создать новый экземпляр цепочки блоков.
        /// </summary>
        public Chain()
        {
            var globalChain = GetGlobalChein();
            if(globalChain == null)
            {
                CreateNewBlockChain();
            }
            else
            {
                bool globalChainIsCorrect = globalChain.CheckCorrect();
                if(globalChainIsCorrect)
                {
                    _blockChain = globalChain._blockChain;
                    _algorithm = globalChain._algorithm;
                    _dataProvider = globalChain._dataProvider;
                    _nodes = globalChain._nodes;
                    _data = globalChain._data;
                    _users = globalChain._users;
                }
                else
                {
                    CreateNewBlockChain();
                    throw new TypeLoadException("Полученная глобальная цепочка блоков является некорректной!");
                }
            }

            if(!CheckCorrect())
            {
                throw new MethodResultException(nameof(Chain), "Ошибка создания цепочки блоков. Цепочка некорректна.");
            }
        }

        /// <summary>
        /// Создание цепочки блоков из списка блоков провайдера данных.
        /// </summary>
        /// <param name="blocks"> Блоки провайдера данных. </param>
        private Chain(List<BlockchainData.Block> blocks)
        {
            if(blocks == null)
            {
                throw new MethodRequiresException(nameof(blocks), "Список блоков провайдера данных не может быть равным null.");
            }

            foreach(var block in blocks)
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
            foreach(var block in _blockChain)
            {
                if(!block.IsCorrect(_algorithm))
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
            // TODO: Реализовать получение хоста из конфигурационных файлов.
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
            if(string.IsNullOrEmpty(text))
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
        public Block AddUser(string login, string password, UserRole role = UserRole.Reader, Guid? code = null)
        {
            if (string.IsNullOrEmpty(login))
            {
                throw new MethodRequiresException(nameof(login), "Логин не может быть пустым или равным null.");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new MethodRequiresException(nameof(password), "Пароль не может быть пустым или равным null.");
            }

            if(Users.Any(b => b.Login == login))
            {
                return null;
            }

            var user = new User(login, password, role, code: code);
            var data = user.GetData();
            var block = new Block(PreviousBlock, data, User.GetCurrentUser());
            AddBlock(block);
            return block;
        }

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
            if(user == null)
            {
                return null;
            }

            var passwordHash = password.GetHash();
            if(user.Password != passwordHash)
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
            if(!block.IsCorrect())
            {
                throw new MethodRequiresException(nameof(block), "Блок не корректный.");
            }

            // TODO: Реализовать транзакцию.
            _blockChain.Add(block);
            _dataProvider.AddBlock(block.Version, block.CreatedOn, block.Hash, block.PreviousHash, block.Data.GetJson(), block.User.GetJson());
            AddDataInList(block);

            if(!CheckCorrect())
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
                    foreach(var host in _nodes)
                    {
                        SendBlockToHosts(host, "AddData", block.Data.Content);
                    }
                    break;
                case DataType.User:
                    var user = new User(block);
                    _users.Add(user);
                    foreach (var host in _nodes)
                    {
                        SendBlockToHosts(host, "AddUser", $"{user.Login}&{user.Password}&{user.Role}");
                    }
                    break;
                case DataType.Node:
                    _nodes.Add(block.Data.Content);
                    foreach (var host in _nodes)
                    {
                        SendBlockToHosts(host, "AddHost", block.Data.Content);
                    }
                    break;
                default:
                    throw new MethodRequiresException(nameof(block), "Неизвестный тип блока.");
            }
        }

        private async void SendBlockToHosts(string ip, string method, string data)
        {
            using (var client = new HttpClient())
            {
                // http://localhost:28451/BlockchainService.svc/api/getchain/
                string repUri = $"http://{ip}/BlockchainService.svc/api/{method}/{data}/";
                var response = await client.GetAsync(repUri);
            }
        }
    }
}

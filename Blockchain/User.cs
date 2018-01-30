using Blockchain.Algorithms;
using Blockchain.Exceptions;
using System;
using System.Runtime.Serialization;

namespace Blockchain
{
    /// <summary>
    /// Пользователь системы.
    /// </summary>
    [DataContract]
    public class User : IHashable
    {
        /// <summary>
        /// Алгоритм хеширования.
        /// </summary>
        private IAlgorithm _algorithm = AlgorithmHelper.GetDefaultAlgorithm();

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [DataMember]
        public string Login { get; private set; }

        /// <summary>
        /// Разрешения пользователя.
        /// </summary>
        [DataMember]
        public UserRole Role { get; private set; }

        /// <summary>
        /// Хеш пароля пользователя.
        /// </summary>
        [DataMember]
        public string Password { get; private set; }

        /// <summary>
        /// Хеш пользователя.
        /// </summary>
        [DataMember]
        public string Hash { get; private set; }

        /// <summary>
        /// Создать новый экземпляр пользователя и правами на чтение.
        /// </summary>
        /// <param name="login"> Имя пользователя. </param>
        /// <param name="password"> Пароль пользователя. </param>
        public User(string login, string password, IAlgorithm algorithm = null)
            :this(login, password, UserRole.Reader, algorithm)
        {
            
        }

        /// <summary>
        /// Создать новый экземпляр пользователя.
        /// </summary>
        /// <param name="login"> Имя пользователя. </param>
        /// <param name="password"> Пароль пользователя. </param>
        /// <param name="role"> Права пользователя. </param>
        /// <param name="algorithm"> Алгоритм хеширования. </param>
        public User(string login, string password, UserRole role, IAlgorithm algorithm = null)
        {
            // Проверяем предусловия.
            if(string.IsNullOrEmpty(login))
            {
                throw new MethodRequiresException(nameof(login), "Логин не может быть пустым или равным null.");
            }

            if(string.IsNullOrEmpty(password))
            {
                throw new MethodRequiresException(nameof(password), "Пароль не может быть пустым или равным null.");
            }

            if(algorithm != null)
            {
                _algorithm = algorithm;
            }

            // Устанавливаем значения.
            Login = login;
            Password = password.GetHash(_algorithm);
            Role = role;
            Hash = this.GetHash(_algorithm);

            if(!this.IsCorrect())
            {
                throw new MethodResultException(nameof(User), "Ошибка создания пользователя. Пользователь некорректный.");
            }
        }

        /// <summary>
        /// Создать экземпляр пользователя на основе блока.
        /// </summary>
        /// <param name="block"> Блок цепочки, содержащий информацию о пользователе. </param>
        public User(Block block)
        {
            if(block == null)
            {
                throw new MethodRequiresException(nameof(block), "Блок не может быть равен null");
            }

            if(!block.IsCorrect())
            {
                throw new MethodRequiresException(nameof(block), "Блок некорректный");
            }

            if(block.Data == null)
            {
                throw new MethodRequiresException(nameof(block), "Данные блока не могут быть равны null");
            }

            if(block.Data.Type != DataType.User)
            {
                throw new MethodRequiresException(nameof(block), "Некоректный тип данных блока.");
            }

            var user = Deserialize(block.Data.Content);

            Login = user.Login;
            Password = user.Password;
            Role = user.Role;
            Hash = user.Hash;

            if(!this.IsCorrect())
            {
                throw new MethodResultException(nameof(User), "Не корректный пользователь.");
            }
        }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Имя пользователя. </returns>
        public override string ToString()
        {
            return Login;
        }

        /// <summary>
        /// Получить текущего пользователя системы.
        /// </summary>
        /// <returns> Текущий пользователь системы. </returns>
        public static User GetCurrentUser()
        {
            // TODO: Исправить получение текущего пользователя.
            return new User("admin", "admin", UserRole.Admin);
        }

        /// <summary>
        /// Получить данные из объекта, на основе которых будет строиться хеш.
        /// </summary>
        /// <returns> Хешируемые данные. </returns>
        public string GetStringForHash()
        {
            var text = Login;
            text += (int)Role;

            return text;
        }

        /// <summary>
        /// Получить блок для добавления пользователя в систему.
        /// </summary>
        /// <returns> Блок содержащий информацию о пользователе. </returns>
        public Data GetData()
        {
            var jsonString = this.GetJson();
            var data = new Data(jsonString, DataType.User, _algorithm);
            return data;
        }

        /// <summary>
        /// Десериализация пользователя из JSON.
        /// </summary>
        /// <param name="json"> Строка с данными пользователя в формате JSON. </param>
        /// <returns> Объект пользователя. </returns>
        public static User Deserialize(string json)
        {
            if(string.IsNullOrEmpty(json))
            {
                throw new MethodRequiresException(nameof(json), "Строка десеализации не может быть пустой или равной null.");
            }

            var user = Helpers.Deserialize(typeof(User), json);

            if(!user.IsCorrect())
            {
                throw new MethodResultException(nameof(user), "Некоректный пользователь после десериализации.");
            }

            return user as User ??
                throw new FormatException("Не удалось выполнить пользователя данных.");
        }
    }
}

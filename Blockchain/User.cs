using Blockchain.Algorithms;
using Blockchain.Exceptions;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

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
        /// Идентификатор пользователя.
        /// </summary>
        [DataMember]
        public Guid Code { get; private set; }

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
            Code = Guid.NewGuid();
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
            return new User("admin", "password", UserRole.Admin);
        }

        /// <summary>
        /// Получить данные из объекта, на основе которых будет строиться хеш.
        /// </summary>
        /// <returns> Хешируемые данные. </returns>
        public string GetStringForHash()
        {
            var text = Code.ToString();
            text += Login;
            text += Password;
            text += Role;

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
    }
}

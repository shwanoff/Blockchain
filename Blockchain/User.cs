using Blockchain.Algorithms;
using Blockchain.Exceptions;
using System;
using System.Linq;

namespace Blockchain
{
    /// <summary>
    /// Пользователь системы.
    /// </summary>
    public class User : IHashable
    {
        /// <summary>
        /// Алгоритм хеширования.
        /// </summary>
        private IAlgorithm _algorithm = AlgorithmHelper.GetDefaultAlgorithm();

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public Guid Code { get; private set; }

        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// Разрешения пользователя.
        /// </summary>
        public UserRole Role { get; private set; }

        /// <summary>
        /// Хеш пароля пользователя.
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// Хеш пользователя.
        /// </summary>
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
                throw new MethodRequiresException(nameof(login));
            }

            if(string.IsNullOrEmpty(password))
            {
                throw new MethodRequiresException(nameof(password));
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
                throw new MethodResultException(nameof(User));
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
            // TODO: Создавать json
            string text = "User\r\n";
            text += $"Login: {Login}\r\n";
            text += $"Password: {Password}\r\n";
            text += $"Role: {(int)Role}";

            var data = new Data(text, DataType.User, _algorithm);
            return data;
        }
    }
}

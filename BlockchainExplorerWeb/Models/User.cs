using System.Runtime.Serialization;

namespace BlockchainExplorerWeb.Models
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// Имя пользователя.
        /// </summary>
        [DataMember]
        public string Login { get; set; }

        /// <summary>
        /// Хеш пароля.
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Хеш.
        /// </summary>
        [DataMember]
        public string Hash { get; set; }

        /// <summary>
        /// Права доступа.
        /// </summary>
        [DataMember]
        public Enums.UserRole Role { get; set; }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Логин. </returns>
        public override string ToString()
        {
            return Login;
        }
    }
}
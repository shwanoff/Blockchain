using Blockchain;
using System.Runtime.Serialization;

namespace BlockchainService
{
    /// <summary>
    /// Пользователь службы.
    /// </summary>
    [DataContract]
    public class User
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        [DataMember]
        public string Login { get; set; }

        /// <summary>
        /// Права доступа.
        /// </summary>
        [DataMember]
        public UserRole Role { get; set; }

        /// <summary>
        /// Приведение объекта к строке.
        /// </summary>
        /// <returns> Логин пользователя. </returns>
        public override string ToString()
        {
            return Login;
        }
    }
}
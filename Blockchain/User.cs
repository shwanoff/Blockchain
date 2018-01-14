using System;

namespace Blockchain
{
    public class User
    {
        public Guid Code { get; private set; }
        public string Login { get; private set; }

        public User(string login)
        {
            Code = Guid.NewGuid();
            Login = login;
        }

        public override string ToString()
        {
            return Login;
        }

        public static User GetCurrentUser()
        {
            // TODO: Исправить получение текущего пользователя.
            return new User("admin");
        }
    }
}

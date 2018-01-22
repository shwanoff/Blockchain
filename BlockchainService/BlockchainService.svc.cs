using Blockchain;

namespace BlockchainService
{
    public class BlockchainService : IBlockchainService
    {
        public bool AddData(string text)
        {
            bool result = false;
            try
            {
                Instance.Get().Chain.AddContent(text);
                result = true;
            }
            catch
            {
                result = false;
                throw;
                // TODO: Добавить сообщение об ошибке в сохранение в лог.
            }

            return result;
        }

        public bool AddUser(string login, string password, UserRole role = UserRole.Reader)
        {
            bool result = false;
            try
            {
                Instance.Get().Chain.AddUser(login, password, role);
                result = true;
            }
            catch
            {
                result = false;
                throw;
                // TODO: Добавить сообщение об ошибке в сохранение в лог.
            }

            return result;
        }
    }
}

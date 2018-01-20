namespace Blockchain
{
    /// <summary>
    /// Права доступа пользователя.
    /// </summary>
    public enum UserRole : byte
    {
        /// <summary>
        /// Администратор.
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Права на запись.
        /// </summary>
        Writer = 2,

        /// <summary>
        /// Права на чтение.
        /// </summary>
        Reader = 3
    }
}

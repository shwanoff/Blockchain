using System.Data.Entity;

namespace BlockchainData
{
    /// <summary>
    /// Контекст подключения к базе данных MS SQL.
    /// </summary>
    public class BlockSqlContext : DbContext
    {
        /// <summary>
        /// Создать экземпляр контекста данных SQL.
        /// </summary>
        public BlockSqlContext()
            : base("BlockchainLocalConnection")
        {
        }

        /// <summary>
        /// Блоки данных.
        /// </summary>
        public DbSet<Block> Blocks { get; set; }
    }
}

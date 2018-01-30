using System.Data.Entity;
using System.Data.Entity.Infrastructure;

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
            // Применяем атрибут utc к свойствам сущностей отмеченные этим атрибутом.
            ((IObjectContextAdapter)this).ObjectContext.ObjectMaterialized += (sender, e) => DateTimeKindAttribute.Apply(e.Entity);
        }

        /// <summary>
        /// Блоки данных.
        /// </summary>
        public DbSet<Block> Blocks { get; set; }
    }
}

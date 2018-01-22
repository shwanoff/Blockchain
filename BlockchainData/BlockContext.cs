using System.Data.Entity;

namespace BlockchainData
{
    public class BlockContext : DbContext
    {
        public BlockContext()
            : base("BlockchainConnection")
        {
            // hack to force the EntityFramework.SqlServer.dll to be copied when another project references this one
            var forceDllCopy = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        public DbSet<Block> Blocks { get; set; }
    }
}

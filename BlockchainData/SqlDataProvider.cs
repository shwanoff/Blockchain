using System.Collections.Generic;
using System.Linq;

namespace BlockchainData
{
    public class SqlDataProvider : IDataProvider
    {
        public void AddBlock(ushort version, string code, ulong createdOn, string hash, string previousHash, string data, string user)
        {
            using (var db = new BlockContext())
            {
                var block = new Block()
                {
                    Version = version,
                    Code = code,
                    CreatedOn = createdOn,
                    Hash = hash,
                    PreviousHash = previousHash,
                    Data = data,
                    User = user
                };

                db.Blocks.Add(block);
                db.SaveChanges();
            }
        }

        public List<Block> GetBlocks()
        {
            using (var db = new BlockContext())
            {
                var blocks = db.Blocks.ToList();
                return blocks;
            }
        }
    }
}

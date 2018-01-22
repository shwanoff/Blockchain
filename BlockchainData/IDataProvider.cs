using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainData
{
    public interface IDataProvider
    {
        void AddBlock(ushort version, string code, ulong createdOn, string hash, string previousHash, string data, string user);
        List<Block> GetBlocks();
    }
}

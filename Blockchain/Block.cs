using Blockchain.Algorithms;
using System;

namespace Blockchain
{
    public class Block : IHashable
    {
        public ushort Version { get; private set; }
        public Guid Code { get; private set; }
        public ulong CreatedOn { get; private set; }
        public string Hash { get; private set; }
        public string PreviousHash { get; private set; }
        public Data Data { get; private set; }

        public string GetStringForHash()
        {
            var data = "";
            data += Version;
            data += Code;
            data += CreatedOn;
            data += PreviousHash;
            data += Data.Hash;
            return data;
        }
    }
}

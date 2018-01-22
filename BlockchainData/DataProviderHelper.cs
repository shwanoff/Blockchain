using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainData
{
    public static class DataProviderHelper
    {
        public static IDataProvider GetDefaultDataProvider()
        {
            return new SqlDataProvider();
        }
    }
}

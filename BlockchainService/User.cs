using Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BlockchainService
{
    [DataContract]
    public class User
    {
        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public UserRole Role { get; set; }

        public override string ToString()
        {
            return Login;
        }
    }
}
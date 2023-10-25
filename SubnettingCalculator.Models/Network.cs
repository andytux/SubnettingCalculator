﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SubnettingCalculator.Models
{
    
    public class Network
    {
        public IpAddress IpAddress { get; init; }
        public SubnetMask SubnetMask { get; init; }
        public IpAddress NetID { get; init; }
        public IpAddress BroadCast { get; init; }
        public IpAddress FirstHost { get; init; }
        public IpAddress LastHost { get; init; }
        public int SumHosts { get; init; }

        public Network(IpAddress ipAddress, SubnetMask subnetmask)
        {
            IpAddress = ipAddress;
            SubnetMask = subnetmask;

            NetID = ipAddress & subnetmask;
            BroadCast = NetID | ~subnetmask;
            FirstHost = NetID + 1;
            LastHost = BroadCast - 1;
            SumHosts = (int)Math.Pow(2,32 - SubnetMask.CidrSuffix)-2;
        }     
    }
}

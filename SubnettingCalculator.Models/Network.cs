using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SubnettingCalculator.Models
{
    
    public  class Network
    {
        private byte[] ipAddress {  get; set; }
        private byte[] subnetmask { get; set; }

        public Network(byte[] ipAddress, byte[] subnetmask)
        {
            this.ipAddress = ipAddress;
            this.subnetmask = subnetmask;
        }

        public static byte[] CalculateNetId(byte[] ipAddress, byte[] subnetMask)
        {
            if (ipAddress.Length != 4 || subnetMask.Length != 4)
            {
                throw new ArgumentException("IP-Adresse und Subnetzmaske müssen jeweils 4 Bytes lang sein.");
            }

            byte[] netId = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                netId[i] = (byte)(ipAddress[i] & subnetMask[i]);
            }

            return netId;
        }
        
        public static byte[] CalculateBroadCast(byte[] netId, byte[] subnetMask)
        {
            if (netId.Length != 4 || subnetMask.Length != 4)
            {
                throw new ArgumentException("NetID-Adresse und Subnetzmaske müssen jeweils 4 Bytes lang sein.");
            }

            byte[] broadCast = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                broadCast[i] = (byte)(netId[i] | ~subnetMask[i]);
            }

            return broadCast;
        }



    }
}

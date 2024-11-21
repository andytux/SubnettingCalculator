namespace SubnettingCalculator.Models
{
    public class IPv4Address : BaseAddressIPv4
    {
        public IPv4Address(byte[] octets)
        {
            Octets = octets;
        }

        public IPv4Address(string octets)
        {
            Octets = OctetsStringToByteArray(octets);
        }

        public static IPv4Address operator &(IPv4Address ipAddress, SubnetMaskIPv4 Subnetmask)
        {
            byte[] result = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                result[i] = (byte)(ipAddress.Octets[i] & Subnetmask.Octets[i]);
            }
            return new IPv4Address(result);
        }

        public static IPv4Address operator |(IPv4Address ipAddress, SubnetMaskIPv4 Subnetmask)
        {
            byte[] result = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                result[i] = (byte)(ipAddress.Octets[i] | Subnetmask.Octets[i]);
            }
            return new IPv4Address(result);
        }

        public static IPv4Address operator +(IPv4Address netId, int value)
        {
            byte[] result = new byte[netId.Octets.Length];

            for (int i = 0; i < 4; i++)
            {
                if (i == netId.Octets.Length - 1)
                    result[i] = (byte)(netId.Octets[i] + value);
                else
                    result[i] = (byte)netId.Octets[i];
            }
            return new IPv4Address(result);
        }

        public static IPv4Address operator -(IPv4Address broadCastAddress, int value)
        {
            byte[] result = new byte[4];

            for (int i = 0; i < broadCastAddress.Octets.Length; i++)
            {
                if (i == broadCastAddress.Octets.Length - 1)
                    result[i] = (byte)(broadCastAddress.Octets[i] - value);
                else
                    result[i] = (byte)broadCastAddress.Octets[i];
            }
            return new IPv4Address(result);
        }

    }
}

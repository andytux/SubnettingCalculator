namespace SubnettingCalculator.Models
{

    public class NetworkIPv4
    {
        public IPv4Address IPv4Address { get; init; }
        public SubnetMaskIPv4 SubnetMask { get; init; }
        public IPv4Address NetID { get; init; }
        public IPv4Address BroadCast { get; init; }
        public IPv4Address FirstHost { get; init; }
        public IPv4Address LastHost { get; init; }
        public int SumHosts { get; init; }

        public NetworkIPv4(IPv4Address ipAddress, SubnetMaskIPv4 subnetmask)
        {
            IPv4Address = ipAddress;
            SubnetMask = subnetmask;

            NetID = ipAddress & subnetmask;
            BroadCast = NetID | ~subnetmask;
            FirstHost = NetID + 1;
            LastHost = BroadCast - 1;
            SumHosts = (int)Math.Pow(2,32 - SubnetMask.CidrSuffix)-2;
        }
    }
}

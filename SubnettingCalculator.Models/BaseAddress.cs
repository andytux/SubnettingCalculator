namespace SubnettingCalculator.Models
{
    public abstract class BaseAddress
    {
        public byte[] Octets { get; protected set; } = new byte[4];

        internal byte[] OctetsStringToByteArray(string octets)
        {
            byte[] bytes = new byte[4];
            string[] octetsSplit = octets.Split('.');

            if (octetsSplit.Length != 4)
            {
                throw new ArgumentOutOfRangeException("Der String enthält nicht genau 4 Oktette.");
            }

            for (int i = 0; i < 4; i++)
            {
                if (!byte.TryParse(octetsSplit[i], out bytes[i]) || bytes[i] < 0 || bytes[i] > 255)
                {
                    throw new ArgumentOutOfRangeException("Ungültige Oktette in der IP-Adresse.");
                }
            }

            return bytes;
        }

    }
}
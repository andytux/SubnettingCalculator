using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubnettingCalculator.Models;

public class SubnetMaskIPv4 : BaseAddressIPv4
{
    public int CidrSuffix { get; set; }

    public SubnetMaskIPv4(byte[] octets)
    {
        Octets = octets;
        CidrSuffix = GetSuffix(octets);
    }

    public SubnetMaskIPv4(byte[] octets, int cidrSuffix)
    {
        Octets = octets;
        CidrSuffix = cidrSuffix; 
    }
    public SubnetMaskIPv4(string octets)
    {
        Octets = base.OctetsStringToByteArray(octets);
        CidrSuffix = GetSuffix(Octets);
    }
    public SubnetMaskIPv4(int cidrSuffix)
    {
        Octets = ConvertCidrSuffixToOctets(cidrSuffix);
        CidrSuffix = cidrSuffix;
    }

    public static byte[] ConvertCidrSuffixToOctets(int cidrSuffix)
    {
        // Throw ArgumentOutOfRangeException for invalid suffix
        if (cidrSuffix < 1 || cidrSuffix > 30)
            throw new ArgumentOutOfRangeException(nameof(cidrSuffix), "Invalid Suffix. Suffix must be greater than 0 and smaller than 31.");

        // construct the binary string:
        string binarySuffix = string.Empty;

        // add as many '1's as cidrSuffix
        for (int i = 0; i < cidrSuffix; i++)
        {
            binarySuffix += "1";
        }
        // fill up with '0's till string length is 32
        for (int i = 0; i < 32 - cidrSuffix; i++)
        {
            binarySuffix += "0";
        }

        // convert binary string to byte[]:
        byte[] result = new byte[4];

        for (int i = 0; i < 4; i++)
        {
            result[i] = Convert.ToByte(binarySuffix.Substring(8 * i, 8), 2);
        }

        return result;
    }

    public static byte[] GetByteOfInt (int suffix)
    {
        if (suffix < 8|| suffix > 30) { throw new ArgumentOutOfRangeException(); }
        int count = 0;
        int j = 0;
        string bytes = "";
        byte[] byteArray = new byte[4];
        for (int i = 1; i <= 32; i++)
        {
            if (suffix - 1 >= 0)
            {
                bytes += '1';
                suffix--;
                
            }
            else
            {
                bytes += "0";
            }
            count++;
            if (count % 8 == 0)
            {
                byteArray[j] = Convert.ToByte(bytes, 2);
                bytes = string.Empty;
                j++;
            }
        }               
        return byteArray;
    }

    public static int GetSuffix(byte[] octets)
    {
        bool state = true;
        int count = 0;
        string binary = "";
        foreach (byte octet in octets)
        {
            binary += Convert.ToString(octet, 2);
        }
        foreach (char c in binary)
        {
            if (c == '1' && state)
            {
                count++;
            }
            else { state = false; }
            if (c == '1' && !state)
            {
                throw new ArgumentOutOfRangeException("invalid SNM");
            }
        }
        return count;
    }

    public static void InvalidSuffix(byte[] octets)
    {
        bool state = true;
        int currentSuffix = octets[0];

        for (int i = 1; i < octets.Length; i++)
        {            
            if (currentSuffix < octets[i])
            {
                state = false;
                break;
            }
            currentSuffix = octets[i];

        }
        if (!state)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    public static SubnetMaskIPv4 operator ~(SubnetMaskIPv4 snm)
    {
        byte[] result = new byte[4];

        for (int i = 0; i < 4; i++)
        {
            result[i] = (byte)~snm.Octets[i];
        }
        return new SubnetMaskIPv4(result, 0);
    }
}


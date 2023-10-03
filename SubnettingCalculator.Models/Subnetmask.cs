using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubnettingCalculator.Models;

public class Subnetmask : BaseAddress
{
    int CidrSuffix { get; set; }

    public Subnetmask(byte[] octets)
    {
        Octets = octets;
        CidrSuffix = GetSuffix(octets);
    }
    public Subnetmask(string octets)
    {
        Octets = base.OctetsStringToByteArray(octets);
        CidrSuffix = GetSuffix(Octets);
    }
    
    public static byte[] GetByteOfInt (int suffix)
    {
        if (suffix < 8|| suffix > 30) { throw new ArgumentOutOfRangeException; }
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
}


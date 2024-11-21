using NUnit.Framework;
using SubnettingCalculator.Models;
using System.Net;

namespace SubnettingCalculator.Models.Tests
{
    [TestFixture]
    public class IPAddressTests
    {
        [TestCase("192.168.0.1", new byte[] {192, 168, 0, 1})]
        public void IpAddressTest(string input, byte[] expected)
        {
            IPv4Address iPAddress = new IPv4Address(input);

           // byte[] bytes = { 192, 168, 0, 10 };

            Assert.That(iPAddress.Octets, Is.EqualTo(expected));
        }

        [TestCase(new byte[] { 255, 23, 255, 0 })]
        [TestCase(new byte[] { 255, 0, 255, 0 })]
        public void Invalid_GetSuffixOfSNM(byte[] input)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => SubnetMaskIPv4.GetSuffix(input));
        }            
            
        [TestCase(new byte[] { 255, 255, 255, 0 }, 24)]
        [TestCase(new byte[] { 255, 255, 0, 0 }, 16)]
        [TestCase(new byte[] { 255, 0, 0, 0 }, 8)]
        public void GetSuffix_NTest(byte[] input, int expected)
        {
            int result = SubnetMaskIPv4.GetSuffix(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(new byte[] { 255, 23, 255, 0 })]
        [TestCase(new byte[] { 255, 0, 255, 0 })]
        public void Invalid_SNM(byte[] input)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => SubnetMaskIPv4.InvalidSuffix(input));
        }

        [TestCase("192.168.0.1", new byte[] {192, 168, 0, 1})]
        [TestCase("192.162.10.1", new byte[] {192, 162, 10, 1})]
        
        public void OctetsStringToByteArrayTest(string input, byte[] expected)
        {
            IPv4Address iPAddress = new IPv4Address(input);

            Assert.That(iPAddress.Octets, Is.EqualTo(expected));
        }
        
        [TestCase("192.162.10.100.25")]
        [TestCase("260.162.100.25")]
        public void NotValidIP4AdressTest_ThrowsExeption(string input)
        {
            IPv4Address ipAddress;

            Assert.Throws<ArgumentOutOfRangeException>(() =>  ipAddress = new IPv4Address(input));
        }

        [TestCase(24, new byte[] {255,255,255,0})]
        public void GetByteOfIntTest(int suffix, byte[] expected)
        {
            byte[] result = SubnetMaskIPv4.GetByteOfInt(suffix);
            Assert.That(result, Is.EqualTo(expected));
        }
    }

}
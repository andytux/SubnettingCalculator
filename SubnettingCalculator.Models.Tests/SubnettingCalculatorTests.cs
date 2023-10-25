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
            IpAddress iPAddress = new IpAddress(input);

           // byte[] bytes = { 192, 168, 0, 10 };

            Assert.That(iPAddress.Octets, Is.EqualTo(expected));
        }

        [TestCase(new byte[] { 255, 23, 255, 0 })]
        [TestCase(new byte[] { 255, 0, 255, 0 })]
        public void Invalid_GetSuffixOfSNM(byte[] input)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => SubnetMask.GetSuffix(input));
        }            
            
        [TestCase(new byte[] { 255, 255, 255, 0 }, 24)]
        [TestCase(new byte[] { 255, 255, 0, 0 }, 16)]
        [TestCase(new byte[] { 255, 0, 0, 0 }, 8)]
        public void GetSuffix_NTest(byte[] input, int expected)
        {
            int result = SubnetMask.GetSuffix(input);

            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(new byte[] { 255, 23, 255, 0 })]
        [TestCase(new byte[] { 255, 0, 255, 0 })]
        public void Invalid_SNM(byte[] input)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => SubnetMask.InvalidSuffix(input));
        }

        [TestCase("192.168.0.1", new byte[] {192, 168, 0, 1})]
        [TestCase("192.162.10.1", new byte[] {192, 162, 10, 1})]
        
        public void OctetsStringToByteArrayTest(string input, byte[] expected)
        {
            IpAddress iPAddress = new IpAddress(input);

            Assert.That(iPAddress.Octets, Is.EqualTo(expected));
        }
        
        [TestCase("192.162.10.100.25")]
        [TestCase("260.162.100.25")]
        public void NotValidIP4AdressTest_ThrowsExeption(string input)
        {
            IpAddress ipAddress;

            Assert.Throws<ArgumentOutOfRangeException>(() =>  ipAddress = new IpAddress(input));
        }

        [TestCase(24, new byte[] {255,255,255,0})]
        public void GetByteOfIntTest(int suffix, byte[] expected)
        {
            byte[] result = SubnetMask.GetByteOfInt(suffix);
            Assert.That(result, Is.EqualTo(expected));
        }
    }

}
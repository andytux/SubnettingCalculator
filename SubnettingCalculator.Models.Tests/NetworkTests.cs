using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubnettingCalculator.Models.Tests
{
    [TestFixture]
    internal class NetworkTests
    {
        [TestCase(new byte[] { 192, 168, 0, 1 }, new byte[] { 255, 255, 255, 0 }, new byte[] { 192, 168, 0, 0 })]
        [TestCase(new byte[] { 172, 30, 50, 1 }, new byte[] { 255, 0, 0, 0 }, new byte[] { 172, 0, 0, 0 })]
        [TestCase(new byte[] { 85, 45, 10, 0 }, new byte[] { 255, 255, 255, 240 }, new byte[] { 85, 45, 10, 0 })]
        public void NetIDTest(byte[] ipAddress, byte[] subnetmask, byte[] expected)
        {
            IpAddress ip = new IpAddress(ipAddress);
            Subnetmask snm = new Subnetmask(subnetmask);
            
            Network network = new Network(ip, snm);
            Assert.That(network.NetID.Octets, Is.EqualTo(expected));
        }

        [TestCase(new byte[] { 192, 168, 0, 1 }, new byte[] { 255, 255, 255, 0 }, new byte[] { 192, 168, 0, 255 })]
        [TestCase(new byte[] { 172, 30, 50, 1 }, new byte[] { 255, 0, 0, 0 }, new byte[] { 172, 255, 255, 255 })]
        [TestCase(new byte[] { 85, 45, 10, 0 }, new byte[] { 255, 255, 255, 240 }, new byte[] { 85, 45, 10, 15 })]
        [TestCase(new byte[] { 192, 168, 10, 0 }, new byte[] { 255, 255, 255, 0 }, new byte[] { 192, 168, 10, 255 })]
        public void BroadCastTest(byte[] ip, byte[] subnetmask, byte[] expected)
        {
            IpAddress ipAdress = new IpAddress(ip);
            Subnetmask snm = new Subnetmask(subnetmask);

            Network network = new Network(ipAdress, snm);
            Assert.That(network.BroadCast.Octets, Is.EqualTo(expected));
        }

        [TestCase(new byte[] { 192, 168, 0, 1 }, new byte[] { 255, 255, 255, 0 }, new byte[] { 192, 168, 0, 1 })]
        [TestCase(new byte[] { 172, 30, 50, 1 }, new byte[] { 255, 0, 0, 0 }, new byte[] { 172, 0, 0, 1 })]
        [TestCase(new byte[] { 85, 45, 10, 0 }, new byte[] { 255, 255, 255, 240 }, new byte[] { 85, 45, 10, 1 })]
        public void FirstIPTest(byte[] ip, byte[] subnetmask, byte[] expected)
        {
            IpAddress ipAdress = new IpAddress(ip);
            Subnetmask snm = new Subnetmask(subnetmask);


            Network network = new Network(ipAdress, snm);
            Assert.That(network.FirstHost.Octets, Is.EqualTo(expected));
        }
        [TestCase(new byte[] { 192, 168, 0, 1 }, new byte[] { 255, 255, 255, 0 }, new byte[] { 192, 168, 0, 254 })]
        [TestCase(new byte[] { 172, 30, 50, 1 }, new byte[] { 255, 0, 0, 0 }, new byte[] { 172, 255, 255, 254 })]
        [TestCase(new byte[] { 85, 45, 10, 0 }, new byte[] { 255, 255, 255, 240 }, new byte[] { 85, 45, 10, 14 })]
        public void LastIPTest(byte[] ip, byte[] subnetmask, byte[] expected)
        {
            IpAddress ipAdress = new IpAddress(ip);
            Subnetmask snm = new Subnetmask(subnetmask);


            Network network = new Network(ipAdress, snm);
            Assert.That(network.LastHost.Octets, Is.EqualTo(expected));
        }

        [TestCase(new byte[] { 192, 168, 0, 1 }, new byte[] { 255, 255, 255, 0 }, 254)]
        [TestCase(new byte[] { 172, 30, 50, 1 }, new byte[] { 255, 0, 0, 0 }, 16777214)]
        [TestCase(new byte[] { 85, 45, 10, 0 }, new byte[] { 255, 255, 255, 240 }, 14)]
        public void SumOfHosts(byte[] ip, byte[] subnetmask,int expected)
        {
            IpAddress ipAdress = new IpAddress(ip);
            Subnetmask snm = new Subnetmask(subnetmask);


            Network network = new Network(ipAdress, snm);
            Assert.That(network.SumHosts, Is.EqualTo(expected));
        }

    }
}

using SubnettingCalculator.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SubnettingCalculater.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
    }
}


//get{return subnetMask;}
//set {
//    if (subnetMask == value) return;
//subnetmask = value;
//try
//{
//    network = new Network(new IPAdress(IPAdress), new Subnetmask(Subnetmask));
//    CidrSuffix = Network.Subnetmask.ToString();
//    OnPropertyChanged();
//}

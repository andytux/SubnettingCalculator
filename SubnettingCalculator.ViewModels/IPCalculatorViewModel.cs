using SubnettingCalculator.Models;
using SubnettingCalculator.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace SubnettingCalculator.ViewModels
{
    public class IPCalculatorViewModel : ObservableObject
    {
        private Network network;

        public Network Network
        {
            get { return network; }
            set
            {
                network = value;
                OnPropertyChanged();
            }
        }

    }
}

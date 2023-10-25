﻿using SubnettingCalculator.Models;
using SubnettingCalculator.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace SubnettingCalculator.ViewModels;

    public class IPCalculatorViewModel : ObservableObject
    {
        private bool _snmWarning = false;
        private string _warning = string.Empty;
        private bool _ipWarning = false;
        private int _cidrSuffix;
        private string _ipAddress;
        private string _subnetMask;
        private Network _network;

        public string Warning
        {
            get { return _warning; }
            set
            {
                _warning = value;
                OnPropertyChanged();
            }
        }

        public Network Network
        {
            get { return _network; }
            set
            {
                _network = value;
                OnPropertyChanged();
            }
        }

        public string IpAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                try
                {
                    Network = new Network(new IpAddress(IpAddress), new SubnetMask(SubnetMask));
                }
                catch (Exception)
                {
                    _warning = "Inkorrekte Ip Adresse";
                }
                OnPropertyChanged();
            }
        }

        public string SubnetMask
        {
            get { return _subnetMask; }
            set
            {
                if (_subnetMask == value) return;
                _subnetMask = value;

                try
                {
                    Network = new Network(new IpAddress(IpAddress), new SubnetMask(SubnetMask));
                    CidrSuffix = Network.SubnetMask.CidrSuffix;
                    _snmWarning = false;
                    if (!_snmWarning && !_ipWarning) _warning = "";
                }
                catch (Exception)
                {

                    _warning += "Achtung! Format der Subnetzmaske inkorrekt";
                }
                OnPropertyChanged();
            }
        }


        public int CidrSuffix
        {
            get { return _cidrSuffix; }
            set
            {
                if (value == _cidrSuffix) return;
                _cidrSuffix = value;
                Network = new Network(new IpAddress(_ipAddress), new SubnetMask(_cidrSuffix));
                SubnetMask = Network.SubnetMask.ToString();
                OnPropertyChanged();
            }
        }

    public IPCalculatorViewModel()
    {
        _ipAddress = "192.168.15.0";
        _cidrSuffix = 24;
        _network = new Network(new IpAddress(IpAddress), new SubnetMask(CidrSuffix));
        _subnetMask = Network.SubnetMask.ToString();
    }
}


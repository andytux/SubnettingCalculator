using SubnettingCalculator.Models;
using SubnettingCalculator.ViewModels.Core;

namespace SubnettingCalculator.ViewModels;

public class IPCalculatorViewModel : ObservableObject
{
    private bool _snmWarning = false;
    private string _warning = string.Empty;
    private bool _ipWarning = false;
    private int _cidrSuffix;
    private string _ipAddress;
    private string _subnetMask;
    private NetworkIPv4 _network;

    public string Warning
    {
        get { return _warning; }
        set
        {
            _warning = value;
            OnPropertyChanged();
        }
    }

    public NetworkIPv4 Network
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
                Network = new NetworkIPv4(new IPv4Address(IpAddress), new SubnetMaskIPv4(SubnetMask));
                _ipWarning = false;
                Warning = string.Empty;
            }
            catch (Exception)
            {
                if (!_ipWarning)
                {
                    Warning += "Inkorrekte Ip Adresse \n";
                }

                _ipWarning = true;
            }
            if (!_snmWarning && !_ipWarning) _warning = string.Empty;

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
                CidrSuffix = Network.SubnetMask.CidrSuffix;
                Network = new NetworkIPv4(new IPv4Address(IpAddress), new SubnetMaskIPv4(SubnetMask));
                _snmWarning = false;
                Warning = string.Empty;
            }
            catch (Exception)
            {
                if (!_snmWarning)
                    Warning += "Achtung! Format der Subnetzmaske inkorrekt \n";

                _snmWarning = true;
            }
            if (!_snmWarning && !_ipWarning) _warning = string.Empty;
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

            try
            {
                _cidrSuffix = value;
                Network = new NetworkIPv4(new IPv4Address(_ipAddress), new SubnetMaskIPv4(_cidrSuffix));
                _snmWarning = false;
                SubnetMask = Network.SubnetMask.ToString();
                Warning = string.Empty;
            }
            catch
            {
                if (!_snmWarning)
                {
                    Warning += "Achtung! Format der Subnetzmaske inkorrekt \n";
                    _snmWarning = true;
                }
                OnPropertyChanged();
            }
        }
    }

    public IPCalculatorViewModel()
    {
        _ipAddress = "192.168.15.0";
        _cidrSuffix = 24;
        _network = new NetworkIPv4(new IPv4Address(IpAddress), new SubnetMaskIPv4(CidrSuffix));
        _subnetMask = Network.SubnetMask.ToString();
    }
}


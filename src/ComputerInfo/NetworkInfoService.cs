using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace ComputerInfo
{
    public class NetworkInfoService
    {
        public string GetNetworkAdapterInfo()
        {
            try
            {
                var adapters = NetworkInterface.GetAllNetworkInterfaces()
                    .Where(ni =>
                        ni.OperationalStatus == OperationalStatus.Up &&
                        (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                         ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211));

                var active = adapters.FirstOrDefault();
                return active != null ? $"{active.Description}" : "No active adapter";
            }
            catch (Exception ex)
            {
                return $"Adapter Info Error: {ex.Message}";
            }
        }

        public string GetIpAddress()
        {
            try
            {
                var interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (var ni in interfaces)
                {
                    if (ni.OperationalStatus == OperationalStatus.Up)
                    {
                        var props = ni.GetIPProperties();
                        var ip = props.UnicastAddresses
                            .FirstOrDefault(ua => ua.Address.AddressFamily == AddressFamily.InterNetwork);
                        if (ip != null)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
                return "No active IP address found";
            }
            catch (Exception ex)
            {
                return $"IP Address Error: {ex.Message}";
            }
        }
    }
}

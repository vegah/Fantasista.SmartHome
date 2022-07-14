using System.Text;
using Fantasista.SmartHome;
using Fantasista.SmartHome.Scanners;
using NetworkManager.DBus;
using Tmds.DBus;

namespace WifiScanner;
public class WifiScanner : IScanner
{
    public async Task<IEnumerable<IScannable>> Scan()
    {
        Log.Info("Scanning for wifi access points");
        var allAps = new List<WifiScanItem>();
        var systemConnection = Connection.System;        
        var networkManager = systemConnection.CreateProxy<INetworkManager>("org.freedesktop.NetworkManager", "/org/freedesktop/NetworkManager");
        var devices = await networkManager.GetDevicesAsync();
        

        foreach (var device in devices) 
        {
            var ifaceType = await device.GetDeviceTypeAsync();
            if (ifaceType == 2) 
            {
                var iface = await device.GetInterfaceAsync();
                var wifiManager = systemConnection.CreateProxy<IWireless>("org.freedesktop.NetworkManager",device.ObjectPath);
                Log.Info($"Found wifi interface : {iface} - {device.ObjectPath}");
                await wifiManager.RequestScanAsync(new Dictionary<string,object>());
                var aps = await wifiManager.GetAllAccessPointsAsync();
                foreach (var ap in aps)
                {
                        var accessPoint = systemConnection.CreateProxy<IAccessPoint>("org.freedesktop.NetworkManager",ap);
                        var data = await accessPoint.GetAllAsync();
                        var name = Encoding.UTF8.GetString(data.Ssid);
                        allAps.Add(new WifiScanItem() { Name = name});
                }
            }             
        }
        Log.Info($"Wifi scanning found {allAps.Count} access points");
        return allAps;

    }
}

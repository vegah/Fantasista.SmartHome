using Fantasista.SmartHome.Scanners;
using NetworkManager.DBus;
using Tmds.DBus;

namespace WifiScanner;
public class WifiScanner : IScanner
{
    public IEnumerable<IScannable> Scan()
    {
        Console.WriteLine("Running scan from plugin");
        var systemConnection = Connection.System;        
        var networkManager = systemConnection.CreateProxy<INetworkManager>("org.freedesktop.NetworkManager", "/org/freedesktop/NetworkManager");    
        return new List<WifiScanItem>();

    }
}

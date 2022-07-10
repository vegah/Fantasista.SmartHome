using Fantasista.SmartHome.Scanners;

namespace WifiScanner
{
    public class WifiScanItem : IScannable
    {
        public WifiScanItem()
        {
            Name = "";
        }
        
        public string Name { get ; set; }
    }
}
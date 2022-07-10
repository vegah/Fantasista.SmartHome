// See https://aka.ms/new-console-template for more information
using Fantasista.SmartHome;

var config = new DefaultConfig();
var scanners = new AllScanners(config);
Log.Info("Searching for scanners : ");
scanners.FindScanners();
scanners.RunScanner("WifiScanner");

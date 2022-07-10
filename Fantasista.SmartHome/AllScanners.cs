using System.Runtime.Serialization;
using Fantasista.SmartHome.Scanners;

namespace Fantasista.SmartHome
{
    public class AllScanners
    {
        List<AssemblyInformation<IScanner>> _scanners;
        private DefaultConfig _config;

        public AllScanners(DefaultConfig config)
        {
            _scanners = new List<AssemblyInformation<IScanner>>();
            this._config = config;
        }

        public void FindScanners()
        {
            foreach (var path in _config.ScannersPaths)
                _scanners.AddRange(AssemblyUtil.FindByInterface<IScanner>(path));
        }

        public void RunScanner(string name)
        {
            var scanner = _scanners.FirstOrDefault(x=>x.Type.Name==name);
            if (scanner==null) throw new ScannerNotFoundException(name);
            scanner.Instance.Scan();
        }
        
    }

    [Serializable]
    internal class ScannerNotFoundException : Exception
    {

        public ScannerNotFoundException(string message) : base(message)
        {
        }
   }
}
namespace Fantasista.SmartHome.Scanners
{
    public interface IScanner
    {
         IEnumerable<IScannable> Scan();
    }
}
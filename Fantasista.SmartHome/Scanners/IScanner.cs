namespace Fantasista.SmartHome.Scanners
{
    public interface IScanner
    {
         Task<IEnumerable<IScannable>> Scan();
    }
}
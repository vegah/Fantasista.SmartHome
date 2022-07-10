using System.Runtime.Serialization;

namespace Fantasista.SmartHome
{
    [Serializable]
    internal class AssemblyUtilException : Exception
    {

        public AssemblyUtilException(string? message) : base(message)
        {
        }

    }
}
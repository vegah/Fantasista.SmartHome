using System.Reflection;
using System.Runtime.Loader;

namespace Fantasista.SmartHome
{

    public class AssemblyInformation<T>
    {
        public AssemblyInformation(string file,T instance, Type type)
        {   
            File = file;
            Instance = instance;
            Type = type;
        }

        public string File {get;set;}
        public T Instance {get;set;}
        public Type Type {get;set;}
    }


    public static class AssemblyUtil
    {
        public static IEnumerable<AssemblyInformation<T>> FindByInterface<T>(string path)  where T: class
        {
            var allFoundInterfaces = new List<AssemblyInformation<T>>();
            if (!typeof(T).IsInterface) throw new AssemblyUtilException("FindByInterface must have an interface as generic");
            var files = Directory.GetFiles(path).Where(x=>x.EndsWith(".dll")).ToArray();
            var fullPath = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location),path);
            Log.Debug($"Checking {fullPath} : {files.Length} files");

            foreach (var file in files)
            {
                allFoundInterfaces.AddRange(ReadAssembly<T>(fullPath,file));
            }
            return allFoundInterfaces;
        }

        private static IEnumerable<AssemblyInformation<T>> ReadAssembly<T>(string path,string file)  where T : class
        {
                Log.Debug($"Checking {file} for {typeof(T).Name}");
                var asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.Combine(path,Path.GetFileName(file)));                
                foreach (var type in asm.GetExportedTypes())
                {
                    if (typeof(T).IsAssignableFrom(type) && !type.IsInterface)
                    {
                        Log.Info($"Found : {type.Name} from {typeof(T).Name} in {file}");                        
                        var instance = Activator.CreateInstance(type) as T;
                        if (instance!=null) yield return new AssemblyInformation<T>(file,instance,type);
                    }
                }

        }
    }
}
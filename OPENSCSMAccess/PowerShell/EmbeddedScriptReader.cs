using System.Reflection;

namespace OPENSCSMAccessM.PowerShell
{

    public static class EmbeddedScriptReader
    {
        private static readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        public static string GetScript(string scriptName)
        {
            string resourceName = BuildResourceName(scriptName);

            using (Stream stream = _assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    string available = string.Join("\n", GetAvailableScripts());
                    throw new FileNotFoundException(
                        $"Script '{resourceName}' no encontrado.\nDisponibles:\n{available}");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static IEnumerable<string> GetAvailableScripts()
        {
            return _assembly.GetManifestResourceNames()
                .Where(r => r.EndsWith(".ps1") || r.EndsWith(".psm1"));
        }

        private static string BuildResourceName(string scriptName)
        {

            string normalized = scriptName
                .Replace("/", ".")
                .Replace("\\", ".");

            string _namespace = typeof(EmbeddedScriptReader).Namespace
                .Replace(".PowerShell", "");

            if (!normalized.StartsWith(_namespace))
                normalized = $"{_namespace}.PowerShell.{normalized}";

            if (!normalized.EndsWith(".ps1") && !normalized.EndsWith(".psm1"))
                normalized += ".ps1"; 

            return normalized;
        }

        public static string GetModule(string moduleName)
        {
            string _namespace = typeof(EmbeddedScriptReader).Namespace
                .Replace(".PowerShell", "");

            string normalized = moduleName
                .Replace("/", ".")
                .Replace("\\", ".");

            if (!normalized.StartsWith(_namespace))
                normalized = $"{_namespace}.PowerShell.{normalized}";

            if (!normalized.EndsWith(".psm1"))
            {
                normalized = normalized.EndsWith(".ps1")
                    ? normalized.Replace(".ps1", ".psm1")
                    : normalized + ".psm1";
            }

            using (Stream stream = _assembly.GetManifestResourceStream(normalized))
            {
                if (stream == null)
                {
                    string available = string.Join("\n", GetAvailableScripts());
                    throw new FileNotFoundException(
                        $"Módulo '{normalized}' no encontrado.\nDisponibles:\n{available}");
                }

                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}

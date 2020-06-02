using System.Collections.Generic;

namespace Knet.Kudu.Binary
{
    public class ExecutableInfo
    {
        /// <summary>
        /// Path to the executable.
        /// </summary>
        public string ExePath { get; }

        /// <summary>
        /// Any environment variables that should be set when running the executable.
        /// </summary>
        public IReadOnlyDictionary<string, string> EnvironmentVariables { get; }

        public ExecutableInfo(string exePath, IReadOnlyDictionary<string, string> environmentVariables)
        {
            ExePath = exePath;
            EnvironmentVariables = environmentVariables;
        }
    }
}

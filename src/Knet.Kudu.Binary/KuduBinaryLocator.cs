using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Knet.Kudu.Binary
{
    public static class KuduBinaryLocator
    {
        private const string SaslPathName = "SASL_PATH";

        /// <summary>
        /// Retrieve info needed to start Kudu.
        /// </summary>
        /// <param name="exeName">The binary to look for (eg 'kudu-tserver').</param>
        public static ExecutableInfo FindBinary(string exeName)
        {
            var artifactInfo = FindBinaryLocation();

            var executable = Path.Combine(artifactInfo.BinDir, exeName);
            if (!File.Exists(executable))
            {
                throw new FileNotFoundException($"Cannot find executable {exeName} " +
                    $"in binary directory {artifactInfo.BinDir}");
            }

            var env = new Dictionary<string, string>();
            if (artifactInfo.SaslDir != null)
            {
                env.Add(SaslPathName, artifactInfo.SaslDir);
            }

            return new ExecutableInfo(executable, env);
        }

        private static KuduBinaryInfo FindBinaryLocation()
        {
            var appPath = AppContext.BaseDirectory;
            var rid = GetRid();

            var artifactRoot = Path.Combine(appPath, "runtimes", rid, "native", "kudu");
            var binPath = Path.Combine(artifactRoot, "bin");
            if (Directory.Exists(binPath))
            {
                // Only set the saslDir property if we find it in the artifact,
                // since that affects whether the caller needs to set SASL_PATH
                // when executing the binaries.
                var saslDir = Path.Combine(artifactRoot, "lib", "sasl2");

                if (!Directory.Exists(saslDir))
                {
                    saslDir = null;
                }

                return new KuduBinaryInfo(binPath, saslDir);
            }

            throw new Exception("Unable to find Kudu");
        }

        private static string GetRid()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) &&
                RuntimeInformation.OSArchitecture == Architecture.X64)
            {
                return "linux-x64";
            }

            throw new NotSupportedException($"{RuntimeInformation.OSDescription} " +
                $"{RuntimeInformation.OSArchitecture} is not supported");
        }
    }
}

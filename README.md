# Native Apache Kudu binaries for .NET testing

**Important:** This nuget package contains Kudu binaries for use in a "mini cluster" environment for **testing only**.

The binaries in this archive should never be deployed to run an actual Kudu
service, whether in production or development, because many security-related
dependencies are copied from the build system and will not be patched when the
operating system on the runtime host is patched.

This is the .NET equivalent of the Java binary jar as described here, https://kudu.apache.org/2019/03/19/testing-apache-kudu-applications-on-the-jvm.html

## Usage

Add the nuget package Knet.Kudu.Binary. This package will add the Apache Kudu binaries to your application's build output.
This library also comes with a helper method to locate an Apache Kudu binary, `KuduBinaryLocator.FindBinary("kudu")`.
This also returns environment variables that should be set when starting the Kudu process:

```csharp
var kuduExe = KuduBinaryLocator.FindBinary("kudu");
var workingDirectory = Path.GetDirectoryName(kuduExe.ExePath);

var startInfo = new ProcessStartInfo
{
    FileName = kuduExe.ExePath,
    WorkingDirectory = workingDirectory,
    RedirectStandardInput = true,
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    UseShellExecute = false,
    CreateNoWindow = true
};

startInfo.ArgumentList.Add("test");
startInfo.ArgumentList.Add("mini_cluster");
startInfo.ArgumentList.Add("--serialization=pb");

foreach (var env in kuduExe.EnvironmentVariables)
{
    startInfo.EnvironmentVariables.Add(env.Key, env.Value);
}

var process = new ProcessEx(startInfo);
process.Start();
```

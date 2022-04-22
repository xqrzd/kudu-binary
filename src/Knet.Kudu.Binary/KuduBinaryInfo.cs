namespace Knet.Kudu.Binary
{
    /// <summary>
    /// Simple struct to provide various properties of a binary artifact to callers.
    /// </summary>
    public class KuduBinaryInfo
    {
        /// <summary>
        /// Return the binary directory of an extracted artifact.
        /// </summary>
        public string BinDir { get; }

        /// <summary>
        /// Return the SASL module directory of an extracted artifact.
        /// May be null if unknown.
        /// </summary>
        public string? SaslDir { get; }

        public KuduBinaryInfo(string binDir)
            : this(binDir, null)
        {
        }

        public KuduBinaryInfo(string binDir, string? saslDir)
        {
            BinDir = binDir;
            SaslDir = saslDir;
        }
    }
}

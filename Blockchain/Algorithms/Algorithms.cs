using System.ComponentModel;

namespace Blockchain.Algorithms
{
    /// <summary>
    /// Поддерживаемые алгоритмы хеширования.
    /// </summary>
    public enum Algorithms : int
    {
        /// <summary>
        /// SHA256.
        /// </summary>
        [Description("SHA 256")]
        SHA256 = 1
    }
}

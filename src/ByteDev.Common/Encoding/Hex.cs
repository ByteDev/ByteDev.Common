using System;
using System.Globalization;

namespace ByteDev.Common.Encoding
{
    /// <summary>
    /// Represents a set of Hex related operations.
    /// </summary>
    public class Hex
    {
        /// <summary>
        /// Converts <paramref name="bytes" /> to a hex encoded string.
        /// </summary>
        /// <param name="bytes">The byte data to convert to hex.</param>
        /// <returns>A hex string (base16 encoded).</returns>
        public static string ConvertToHex(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        /// <summary>
        /// Converts <paramref name="hex" /> to a byte array.
        /// </summary>
        /// <param name="hex">A hex string (base16 encoded)</param>
        /// <returns>A byte array.</returns>
        public static byte[] ConvertToBytes(string hex)
        {
            var bytes = new byte[hex.Length / 2];

            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);
            }

            return bytes;
        }
    }
}

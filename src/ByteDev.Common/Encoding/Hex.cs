using System;
using System.Globalization;

namespace ByteDev.Common.Encoding
{
    public class Hex
    {
        /// <summary>
        /// Creates a hex string from byte array
        /// </summary>
        /// <param name="bytes">The byte data</param>
        /// <returns>A hex string (base16 encoded)</returns>
        public static string ConvertToHex(byte[] bytes)
        {
            return BitConverter.ToString(bytes).Replace("-", "");
        }

        /// <summary>
        /// Creates a byte array from a hex (base16)
        /// encoded string
        /// </summary>
        /// <param name="hex">A hex string (base16 encoded)</param>
        /// <returns>Byte array</returns>
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

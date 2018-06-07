using System;
namespace ByteDev.Common
{
    public static class GuidExtensions
    {
        public static bool IsEmpty(this Guid source)
        {
            return source == Guid.Empty;
        }

        /// <summary>
        /// Makes the Guid into a a unique sequential guid by
        /// replacing the last 7 bytes
        /// </summary>
        public static Guid Comb(this Guid source)
        {
            if(source == default(Guid))
                throw new ArgumentException("Value is default Guid.", nameof(source));

            byte[] dateBytes = BitConverter.GetBytes(DateTime.Now.Ticks);
            byte[] guidBytes = source.ToByteArray();

            // copy the last six bytes from the date to the last six bytes of the GUID
            Array.Copy(dateBytes, dateBytes.Length - 7, guidBytes, guidBytes.Length - 7, 6);
            return new Guid(guidBytes);
        }
    }
}

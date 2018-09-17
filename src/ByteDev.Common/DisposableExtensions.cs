using System;

namespace ByteDev.Common
{
    public static class DisposableExtensions
    {
        public static void DisposeIfNotNull(this IDisposable source)
        {
            source?.Dispose();
        }
    }
}
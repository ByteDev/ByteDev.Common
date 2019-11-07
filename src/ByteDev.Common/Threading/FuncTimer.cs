using System;
using System.Diagnostics;

namespace ByteDev.Common.Threading
{
    /// <summary>
    /// Allows a Func to be called repeatedly until it returns true or a timeout is reached.
    /// </summary>
    public static class FuncTimer
    {
        /// <summary>
        /// Provided Func will keep getting called until it returns true or the timeout is reached
        /// (which ever occurs first).
        /// </summary>
        /// <param name="func">The Func to call.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns>True if the Func return true in time; otherwise returns false.</returns>
        public static bool WaitFuncReturnsTrueOrTimeout(Func<bool> func, TimeSpan timeout)
        {
            if(func == null)
                throw new ArgumentNullException(nameof(func));

            var source = new Stopwatch();
            source.Start();

            while (source.Elapsed < timeout)
            {
                if (func())
                    return true;
            }

            return false;
        }
    }
}
using System;
using System.Diagnostics;

namespace ByteDev.Common.Time
{
    public class FuncTimer
    {
        /// <summary>
        /// Provided func will keep getting called until it returns true or the timeout is reached
        /// (which ever occurs first).
        /// </summary>
        /// <returns>True, the func returned true in time. False, the func timed out.</returns>
        public static bool WaitFuncReturnsTrueOrTimeout(Func<bool> func, TimeSpan timeout)
        {
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
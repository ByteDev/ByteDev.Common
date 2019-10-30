using System;
using System.Diagnostics;

namespace ByteDev.Common.Threading
{
    public static class FuncTimer
    {
        /// <summary>
        /// Provided func will keep getting called until it returns true or the timeout is reached
        /// (which ever occurs first).
        /// </summary>
        /// <param name="func"></param>
        /// <param name="timeout"></param>
        /// <returns>True if the func return true in time; otherwise returns false.</returns>
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
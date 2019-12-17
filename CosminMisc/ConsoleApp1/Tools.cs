using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    public static class Tools
    {
        /// <summary>
        /// GetCaller
        /// </summary>
        /// <param name="caller"></param>
        /// <returns></returns>
        public static string GetCaller([CallerMemberName] string caller = null)
        {
            return caller;
        }

        public static int ComputeHashCode<T>(this IEnumerable<T> list) {
            unchecked {
                int hash = 19;
                foreach (var x in list)
                    hash = hash * 31 + x.GetHashCode();
                return hash;
            }
        }
    }
}

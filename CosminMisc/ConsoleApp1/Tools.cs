using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Misc
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

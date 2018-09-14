using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Misc
{
    public class Tools
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
    }
}

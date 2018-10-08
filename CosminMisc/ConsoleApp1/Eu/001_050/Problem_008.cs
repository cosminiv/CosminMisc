using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_008
    {
        readonly static string _data = @"";  // TODO: put the digits here

        public static long Solve()
        {
            int factorCount = 13;
            byte[] data = _data.Where(d => char.IsDigit(d)).Select(d => byte.Parse(d.ToString())).ToArray();

            return Tools.MaxProduct(data, factorCount);
        }
    }
}

using System.Linq;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_008
    {
        readonly static string _data = @"";  // TODO: put the digits here

        public static long Solve()
        {
            int factorCount = 13;
            byte[] data = _data.Where(d => char.IsDigit(d)).Select(d => byte.Parse(d.ToString())).ToArray();

            return _Common.Tools.MaxProduct(data, factorCount);
        }
    }
}

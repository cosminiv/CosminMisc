using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Eu
{
    public class Problem_049
    {
        public static long Solve()
        {
            HashSet<long> primes = Tools.GetPrimesUpTo(9999);

            foreach (long prime in primes)
            {
                if (prime < 1000)
                    continue;

                List<long> permutations = Problem_024.GeneratePermutations(prime.ToString())
                    .Select(p => long.Parse(p))
                    .Where(p => p >= 1000 && primes.Contains(p))
                    .Distinct()
                    .OrderBy(p => p)
                    .ToList();
                
                List<long> latestPerm = new List<long>();

                foreach (long perm in permutations)
                {
                    if (latestPerm.Count == 3)
                        latestPerm.RemoveAt(0);

                    latestPerm.Add(perm);

                    if (latestPerm.Count == 3 &&
                        latestPerm[2] - latestPerm[1] > 0 &&
                        latestPerm[2] - latestPerm[1] == latestPerm[1] - latestPerm[0])
                    {
                        if (primes.Contains(latestPerm[0]) && 
                            primes.Contains(latestPerm[1]) && 
                            primes.Contains(latestPerm[2]))
                        {   
                            return (long)(latestPerm[2] + 1E+4 * latestPerm[1] + 1E+8 * latestPerm[0]);
                        }
                    }

                }
            }

            return 0;
        }
    }
}

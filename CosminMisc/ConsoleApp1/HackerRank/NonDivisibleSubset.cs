using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    
    public class NonDivisibleSubset
    {
        public void Solve() {

            string text = "61197933 56459859 319018589 271720536 358582070 849720202 481165658 675266245 541667092 615618805 129027583 755570852 437001718 86763458 791564527 163795318 981341013 516958303 592324531 611671866 157795445 718701842 773810960 72800260 281252802 404319361 757224413 682600363 606641861 986674925 176725535 256166138 827035972 124896145 37969090 136814243 274957936 980688849 293456190 141209943 346065260 550594766 132159011 491368651 3772767 131852400 633124868 148168785 339205816 705527969 551343090 824338597 241776176 286091680 919941899 728704934 37548669 513249437 888944501 239457900 977532594 140391002 260004333 911069927 586821751 113740158 370372870 97014913 28011421 489017248 492953261 73530695 27277034 570013262 81306939 519086053 993680429 599609256 639477062 677313848 950497430 672417749 266140123 601572332 273157042 777834449 123586826";

            int[] numbers = text.Split(' ').Select(n => int.Parse(n)).ToArray();

            int res = nonDivisibleSubset(9, numbers);
        }

        public static int nonDivisibleSubset(int k, int[] S) {
            List<Sol> solutions = InitSolutions(k, S);

            for (int i = 0; i < solutions.Count; i++) {
                Sol sol = solutions[i];
                GenerateBiggerSolutions(sol, solutions, k, S);
                int maxSize = solutions[solutions.Count - 1].Numbers.Count;
            }

            if (solutions.Count > 0)
                return solutions[solutions.Count - 1].Numbers.Count;
            else
                return 0;
        }

        private static void GenerateBiggerSolutions(Sol sol, List<Sol> solutions, int k, int[] S) {
            for (int j = sol.LastIndex + 1; j < S.Length; j++) {
                bool isSolution = true;
                int i;

                for (i = 0; i < sol.Numbers.Count; i++) {
                    long n1 = sol.Numbers[i];
                    long n2 = S[j];

                    if ((n1 + n2) % k == 0) {
                        isSolution = false;
                        break;
                    }
                }

                if (isSolution) {
                    var n = new List<int>(sol.Numbers);
                    n.Add(S[j]);

                    solutions.Add(new Sol {
                        Numbers = n,
                        LastIndex = j
                    });
                }
            }
        }

        static List<Sol> InitSolutions(int k, int[] S) { 
            List<Sol> solutions = new List<Sol>();

            for (int i = 0; i < S.Length; i++) {
                for (int j = i + 1; j < S.Length; j++) {
                    if (((long)S[i] + (long)S[j]) % k != 0) {
                        solutions.Add(new Sol {
                            Numbers = new List<int> { S[i], S[j] },
                            LastIndex = j
                        });
                    }
                }
            }

            Debug.Print($"Added {solutions.Count} of size 2");
            return solutions;
        }

        class Sol
        {
            public List<int> Numbers { get; set; }
            public int LastIndex { get; set; }
        }
    }
}

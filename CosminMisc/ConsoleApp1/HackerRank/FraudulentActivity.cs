using ConsoleApp1.Trees;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.HackerRank
{
    //
    // If the amount spent by a client on a particular day is greater than or equal to 2x the client's 
    // median spending for a trailing number of days, they send the client a notification about 
    // potential fraud.
    //
    // Compute the total number of notifications sent.
    //
    class FraudulentActivity
    {
        readonly static int MAX_VAL = 200;

        public static void Test() {
            //var (arr, trailSize) = (new int[] { 2, 3, 4, 2, 3, 6, 8, 4, 5 }, 5);
            //var (arr, trailSize) = (new int[] { 10, 20, 30, 40, 50 }, 3);
            //var (arr, trailSize) = (new int[] { 1, 2, 3, 4, 4 }, 4);
            //var (arr, trailSize) = (new int[] { 6, 2, 5, 2, 2, 5, 3, 4, 7, 10, 3 }, 4);
            //var (arr, trailSize) = (new int[] { 6, 13, 26 }, 1);
            
            int[] fileArray = File.ReadAllLines(@"C:\Users\ivanc\Downloads\fraud-input01.txt")[1]
                .Split(' ').Select(x => int.Parse(x)).ToArray();
            var (arr, trailSize) = (fileArray, 10000);

            //Console.WriteLine(string.Join(" ", arr));
            int res = activityNotifications(arr, trailSize);

            Console.WriteLine(res);
        }

        static int activityNotifications(int[] expenditure, int d) {
            int[] exp = expenditure;
            int trailSize = d;
            int result = 0;
            Queue<int> queueByAge = new Queue<int>(trailSize);
            int[] counts = new int[MAX_VAL + 1];

            for (int i = 0; i < exp.Length; i++) {
                int n = exp[i];

                if (i + 1 > trailSize) {
                    if (IsNotification(counts, trailSize, n, i))
                        result++;

                    int oldValue = queueByAge.Dequeue();
                    counts[oldValue]--;
                }

                counts[n]++;
                queueByAge.Enqueue(n);
            }

            return result;
        }

        private static bool IsNotification(int[] counts, int trailSize, int val, int iter) {
            double median = -1;
            double halfTrailSize = trailSize / 2.0;
            int prevNumber = -1;

            for (int i = 0; i < counts.Length; i++) {
                halfTrailSize -= counts[i];

                if (halfTrailSize < 0) {
                    if (halfTrailSize == 0 - counts[i]) // prev was zero
                        median = (i + prevNumber) / 2.0;
                    else
                        median = i;

                    break;
                }

                if (counts[i] > 0)
                    prevNumber = i;
            }

            bool isNotif = val >= 2 * median;

            return isNotif;
        }

        private static string CountsToList(int[] counts) {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < counts.Length; i++) {
                for (int j = 0; j < counts[i]; j++) {
                    sb.Append($"{i} ");
                }
            }
            return sb.ToString();
        }
    }
}

using System;
using System.Diagnostics;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_014
    {
        public static long Solve()
        {
            long max = 1000000;
            long maxSequenceLength = 0;
            long numberForMaxSequenceLength = 0;
            long opsSaved = 0;
            long maxNumber = 0;

            int[] lengths = new int[max + 1];

            for (long i = 1; i < max; i++)
            {
                int sequenceLength = 0;
                //Debug.Write(i.ToString() + " ");

                for (long n = i; n > 1; )
                {
                    if (n <= max && lengths[n] > 0)
                    {
                        sequenceLength += lengths[n] - 1;
                        opsSaved += lengths[n];
                        break;
                    }

                    if (n % 2 == 0)
                        n = n / 2;
                    else
                        n = 3 * n + 1;

                    if (n > maxNumber)
                        maxNumber = n;

                    sequenceLength++;
                    //Debug.Write(n.ToString() + " ");
                }

                sequenceLength++;
                lengths[i] = sequenceLength;

                if (sequenceLength > maxSequenceLength)
                {
                    maxSequenceLength = sequenceLength;
                    numberForMaxSequenceLength = i;
                }

                //Debug.Write("  LEN = " + sequenceLength.ToString());
                //Debug.Print("");
            }

            Console.WriteLine($"MAX = {numberForMaxSequenceLength}, MAX LEN = {maxSequenceLength}");
            Console.WriteLine($"Ops saved = {opsSaved}");
            Console.WriteLine($"Max number reached = {maxNumber}");
            Debug.Print($"Ops saved = {opsSaved}");

            return numberForMaxSequenceLength;
        }
    }
}

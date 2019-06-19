using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.LinkedLists
{
    class Palindrome
    {
        public static void Test() {
            int[][] arrays = new int[][] {
                new int[] { 4 },
                new int[] { 4, 7 },
                new int[] { 4, 4 },
                new int[] { 4, 7, 7, 3 },
                new int[] { 4, 7, 7, 4 },
                new int[] { 4, 1, 2, 4, 0, 1, 4 },
                new int[] { 4, 1, 0, 4, 0, 1, 4 },
            };

            foreach (var arr in arrays) {
                Node list = LinkedList.MakeList(arr);
                Console.WriteLine(IsPalindrome(list));
            }

            //PrintList(result);
        }

        public static bool IsPalindrome(Node list) {
            if (list == null)
                return true;

            int size = 0;
            for (Node crt = list; crt != null; crt = crt.Next) {
                size++;
            }

            Node secondHalf = list;
            for (int i = 0; i < size / 2; i++) {
                secondHalf = secondHalf.Next;
            }

            secondHalf = LinkedList.ReverseList(secondHalf);

            for (Node n1 = list, n2= secondHalf; 
                n1 != null && n2 != null; 
                n1 = n1.Next, n2 = n2.Next) {

                if (n1.Value != n2.Value)
                    return false;
            }

            return true;
        }
    }
}

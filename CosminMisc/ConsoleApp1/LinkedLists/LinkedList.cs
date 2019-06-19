using System;
using System.Diagnostics;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.LinkedLists
{
    static class LinkedList
    {
        public static void Test() {
            Node list1 = MakeList(5, 6, 7, 8);

            Node list = new Node(1, new Node(2, new Node(3, new Node(4))));

            Node result = list.Filter(n => n.Value % 2 == 0);

            Debug.Assert(result != null);
            Debug.Assert(result.Value == 2);
            Debug.Assert(result.Next.Value == 4);
            Debug.Assert(result.Next.Next == null);

            PrintList(list);
        }

        public static Node MakeList(params int[] arr) {
            Node head = null;
            Node tail = null;

            for (int i = 0; i < arr.Length; i++) {
                Node node = new Node(arr[i]);
                if (head == null) {
                    head = node;
                    tail = node;
                }
                else {
                    tail.Next = node;
                    tail = node;
                }
            }
            return head;
        }

        public static Node ReverseList(Node list) {
            Node prev = null;
            Node next = null;

            for (Node current = list; current != null; current = next) {
                next = current.Next;
                current.Next = prev;
                prev = current;
            }

            return prev;
        }

        // Modifies the initial list
        public static Node Filter(this Node list, Func<Node, bool> pred) {
            Node result = null;
            Node resultTail = null;

            for (Node crt = list; crt != null; ) {
                Node next = crt.Next;

                if (pred(crt)) {
                    if (resultTail == null) {
                        resultTail = crt;
                        result = crt;
                    }
                    else {
                        resultTail.Next = crt;
                        resultTail = crt;
                    }
                    crt.Next = null;
                }

                crt = next;
            }

            return result;
        }

        public static void PrintList(Node list) {
            StringBuilder sb = new StringBuilder();
            for (Node crt = list; crt != null; crt = crt.Next) {
                sb.Append(crt.Value);
                sb.Append(" ");
            }
            Console.WriteLine(sb.ToString());
        }
    }
}

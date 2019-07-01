using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    //
    // Add Two Numbers
    // The digits are stored in reverse order and each of their nodes contain a single digit. Add the two numbers and return it as a linked list.
    //
    class Leet_002
    {
        public void Test() {
            // 809 + 5981 = 6790
            ListNode l1 = new ListNode(new[] { 9, 0, 8 });
            ListNode l2 = new ListNode(new[] { 1, 8, 9, 5 });
            ListNode res = AddTwoNumbers(l1, l2);
        }

        public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
            ListNode resHead = null, resTail = null;
            int carry = 0;

            while (true) {
                if (l1 == null && l2 == null)
                    break;

                int digit1 = 0, digit2 = 0;

                if (l1 != null) {
                    digit1 = l1.val;
                    l1 = l1.next;
                }

                if (l2 != null) {
                    digit2 = l2.val;
                    l2 = l2.next;
                }

                int n = digit1 + digit2 + carry;
                int digit = n % 10;
                carry = n / 10;

                ListNode newNode = new ListNode(digit);

                if (resHead == null) {
                    resHead = newNode;
                    resTail = resHead;
                }
                else {
                    resTail.next = newNode;
                    resTail = newNode;
                }
            }

            if (carry != 0)
                resTail.next = new ListNode(carry);

            return resHead;
        }

        public class ListNode {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }

            public ListNode(IEnumerable<int> digitsRev) {
                ListNode tail = null;

                foreach (int digit in digitsRev) {
                    if (tail == null) {
                        this.val = digit;
                        tail = this;
                    }
                    else {
                        tail.next = new ListNode(digit);
                        tail = tail.next;
                    }
                }
            }

            public override string ToString() {
                return val.ToString();
            }
        }
    }
}

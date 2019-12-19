
using System.Collections.Generic;

namespace ConsoleApp1.Leet
{
    class Leet_061
    {
        public void Solve()
        {
            int[] nums = { 1, 2, 3, 4, 5};

            ListNode head = MakeList(nums);

            ListNode newHead = RotateRight(head, 2);
        }

        ListNode MakeList(IEnumerable<int> nums)
        {
            ListNode head = null, tail = null;

            foreach (int num in nums)
            {
                ListNode node = new ListNode(num);
                if (head == null)
                {
                    head = node;
                    tail = node;
                }
                else
                {
                    tail.next = node;
                    tail = node;
                }
            }

            return head;
        }

        public ListNode RotateRight(ListNode head, int k)
        {
            if (head == null) return null;

            int length = GetLength(head, out ListNode tail);
            k %= length;

            // make circular
            tail.next = head;
            
            // find new head and tail
            ListNode node = head;

            for (int i = 0; i < length - k; i++)
            {
                node = node.next;
                tail = tail.next;
            }

            head = node;
            tail.next = null;

            return head;
        }

        private int GetLength(ListNode head, out ListNode tail)
        {
            int length = 0;
            tail = head;
            ListNode node = head;

            while (true)
            {
                length++;
                if (node.next != null)
                {
                    node = node.next;
                    tail = node;
                }
                else
                    break;
            }

            return length;
        }

        public class ListNode
        {
            public int val;
            public ListNode next;

            public ListNode(int x)
            {
                val = x;
            }

            public override string ToString()
            {
                return val.ToString();
            }
        }
    }
}

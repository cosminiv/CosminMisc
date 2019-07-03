using ConsoleApp1.LinkedLists;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Leet
{
    public class Leet_019
    {
        public void Test() {
            ListNode list = new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5)))));
            ListNode result = RemoveNthFromEnd_Dictionary(list, 1);
            string resultStr = ListToString(result);
        }

        //public ListNode RemoveNthFromEnd(ListNode head, int n) {
        //    ListNode nodeToDelete = head;
        //    int diff = 1;
        //    int totalNodes = 0;

        //    for (ListNode node = head; node != null; node = node.next) {
        //        totalNodes++;

        //        if (diff < n) diff++;
        //        else nodeToDelete = nodeToDelete.next;
        //    }

        //    if (n == totalNodes) {
        //        if (totalNodes == 1) return null;
        //        else return nodeToDelete.next;
        //    }
        //    else {
        //        if (nodeToDelete.next != null)
        //            nodeToDelete.next = nodeToDelete.next.next;
        //    }

        //    return head;
        //}

        public ListNode RemoveNthFromEnd_Dictionary(ListNode head, int n) {
            Dictionary<int, ListNode> nodes = new Dictionary<int, ListNode>();
            int nodeCount = 0;

            for (ListNode node = head; node != null; node = node.next) {
                nodes.Add(nodeCount, node);
                nodeCount++;
            }

            int nodeIndexToDelete = nodeCount - n;

            if (nodeIndexToDelete == 0) {
                if (nodeCount == 1) return null;
                else return nodes[1];
            }
            else {
                nodes[nodeIndexToDelete - 1].next = nodes[nodeIndexToDelete].next;
            }

            return head;
        }

        string ListToString(ListNode lst) {
            StringBuilder sb = new StringBuilder();
            for (ListNode node = lst; node != null; node = node.next) {
                sb.Append($"{node.val} ");
            }
            return sb.ToString();
        }
        
        //Definition for singly-linked list.
        //public class ListNode {
        //    public int val;
        //    public ListNode next;

        //    public ListNode(int x) { val = x; }
        //    public ListNode(int x, ListNode nxt) { val = x; next = nxt; }
        //}
    }
}

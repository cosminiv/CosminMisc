using System;
using System.Collections.Generic;

namespace ConsoleApp1.Algorithms
{
    internal class InsertionSort
    {
        public void Sort(LinkedList<int> list) {
            for (LinkedListNode<int> node = list.First; node != null; ) {

                LinkedListNode<int> next = node.Next;
                if (node.Previous != null) {   // skip first node    
                    LinkedListNode<int> insertionPoint = node.Previous;

                    while (insertionPoint != null && insertionPoint.Value > node.Value)
                        insertionPoint = insertionPoint.Previous;

                    if (insertionPoint != node.Previous) {
                        if (insertionPoint != null)
                            list.AddAfter(insertionPoint, node.Value);
                        else
                            list.AddFirst(node.Value);

                        list.Remove(node);
                    }
                }

                node = next;
            }
        }
    }
}
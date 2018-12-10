// Swap pairs of elements in a linked list
//
package JavaTest.Leet;

public class Leet_024 { 
	public void test(){ 
    ListNode lst = new ListNode(1);
    lst.next = new ListNode(2);
    lst.next.next = new ListNode(3);

    ListNode res = swapPairs(lst);
    for (ListNode n = res; n != null; n = n.next)
		  System.out.printf("%d ", n.val);
 	}

  public ListNode swapPairs(ListNode list) {
    if (list == null) return null;
    if (list.next == null) return list;
    ListNode result = list.next;
    ListNode prev = list;
    
    for (ListNode n = list; n != null && n.next != null; n = n.next){
      ListNode temp = n.next.next;
      prev.next = n.next;
      n.next.next = n;
      n.next = temp;
      prev = n;
    }
    
    return result;
  } 
  
  static class ListNode { 
    int val; 
    ListNode next; 
    ListNode(int x) {val=x;} 
  }
}
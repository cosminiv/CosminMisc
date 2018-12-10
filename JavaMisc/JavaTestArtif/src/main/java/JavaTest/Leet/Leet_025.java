// Reverse groups of k elements in a linked list
//
package JavaTest.Leet;

public class Leet_025 { 
	public void test(){ 
		ListNode lst = new ListNode(1); 
		lst.next = new ListNode(2);
		lst.next.next = new ListNode(3);
		lst.next.next.next = new ListNode(4);
		lst.next.next.next.next = new ListNode(5);	
 	  lst.next.next.next.next.next = new ListNode(6);

		// for (ListNode n = lst; n != null; n = n.next)
		// 	System.out.printf("%d ", n.val);
		//  System.out.printf("\n\n");
		
		ListNode res = reverseKGroup(lst, 4);
		for (ListNode n = res; n != null; n = n.next)
			System.out.printf("%d ", n.val);
 	}

	// Reverse groups of k elements in list
	public ListNode reverseKGroup(ListNode head, int k) {
		if (head == null) return null;
		if (k == 0) return head;
		int size = 1;
		for (ListNode n = head.next; n != null; n = n.next) ++size;
		ListNode revStart = head, result = head;
		RevResult prevRevRes = null;
		
		for (int i = 0; i < size / k && revStart != null; i++){
			RevResult revRes = reverseList(revStart, k);
			if (i == 0) result = revRes.first;
			if (prevRevRes != null) prevRevRes.last.next = revRes.first;
			prevRevRes = revRes;
			revStart = revRes.last.next;
		}
		
		return result;
	}
			
	// Reverse a linked list using constant memory (no stack)
	public RevResult reverseList(ListNode list, int howMany) {
		if (list == null) return null;
		if (howMany <= 1) return new RevResult(list, list);
		
		ListNode first = list, N1 = list;
		RevResult result = new RevResult();
		result.last = list;
		int nodeIndex = 1;
		
		// Starting with the second element, move them at the beginning of the list
		for (ListNode crtNode = list.next; crtNode != null && nodeIndex < howMany; nodeIndex++){
			ListNode next = crtNode.next;
			crtNode.next = first;
			N1.next = next;
			first = crtNode;
			crtNode = next;
			result.first = first;
		}
		
		return result;
	}
 
	static class ListNode { int val; ListNode next; ListNode(int x) {val=x;} }
	
  static class RevResult { 
  	ListNode first, last; 
  	RevResult(){}
  	RevResult(ListNode f, ListNode l) { first = f; last = l; }
  }
	}

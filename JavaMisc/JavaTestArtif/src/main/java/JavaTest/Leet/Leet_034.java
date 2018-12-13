// Find First and Last Position of Element in Sorted Array
//
package JavaTest.Leet;


public class Leet_034 {
	public void test() {
        int[] nums = new int[] {1, 3, 3, 5, 5, 6, 8, 10, 10, 10, 12, 14, 14};
        //int[] nums = new int[] {1};
        
        int[] res = searchRange(nums, 10);
        System.out.printf("%d %d", res[0], res[1]);
    }

    public int[] searchRange(int[] nums, int target) {
        if (nums == null || nums.length == 0) return new int[] { -1, -1 };

        int first = binarySearchFirst(nums, target);
        int last = binarySearchLast(nums, target);

        return new int[] {first, last};
    }

	int binarySearchFirst(int[] nums, int target) {
        int start = 0, end = nums.length - 1;

		while (start <= end) {
            int mid = (start + end) / 2;
			if (nums[mid] > target) end = mid - 1;
            else if (nums[mid] < target) start = mid + 1;
            else { 
                if (mid == 0 || nums[mid-1] < nums[mid]) return mid;
                else end = mid - 1;
            }
        }
        
		return -1;
    }
    
	int binarySearchLast(int[] nums, int target) {
        int start = 0, end = nums.length - 1;

		while (start <= end) {
            int mid = (start + end) / 2;
			if (nums[mid] > target) end = mid - 1;
            else if (nums[mid] < target) start = mid + 1;
            else { 
                if (mid == nums.length - 1 || nums[mid+1] > nums[mid]) return mid;
                else start = mid + 1;
            }
        }
        
		return -1;
    }  
}
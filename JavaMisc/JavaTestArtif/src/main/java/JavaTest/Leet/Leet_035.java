// Given a sorted array and a target value, return the index if the target is found. 
// If not, return the index where it would be if it were inserted in order.
//
package JavaTest.Leet;


public class Leet_035 {
	public void test() {
        int[] nums = new int[] {1, 3};
        //int[] nums = new int[] {1};
        
        int res = searchInsert(nums, 1);
        System.out.printf("%d", res);
    }

    public int searchInsert(int[] nums, int target) {
        if (nums == null || nums.length == 0) return 0;
        int res = binarySearchInsert(nums, target);
        return res;
    }

	int binarySearchInsert(int[] nums, int target) {
        int start = 0, end = nums.length - 1;

		while (start <= end) {
            int mid = (start + end) / 2;
            if (nums[mid] == target) return mid;
            if (mid == 0 && nums[mid] > target) return 0;
            if (mid > 0 && nums[mid-1] < target && nums[mid] > target) return mid;
            if (mid == nums.length - 1 && nums[mid] < target) return mid + 1;
            if (mid < nums.length - 1 && nums[mid] < target && nums[mid+1] > target) return mid + 1;

			if (nums[mid] > target) end = mid - 1;
            else if (nums[mid] < target) start = mid + 1;
        }
        
		return -1;
    } 
}
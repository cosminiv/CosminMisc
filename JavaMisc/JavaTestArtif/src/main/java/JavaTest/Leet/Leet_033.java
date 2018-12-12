// Search in rotated sorted array
//
package JavaTest.Leet;


public class Leet_033 {
	public void test() {
        int[] nums = new int[] {1, 3};
        int i = binarySearchPivot(nums);
        int res = search(nums, 1);
        System.out.println(res);
    }

    public int search(int[] nums, int target) {
        if (nums == null) return -1;
        if (nums.length == 0) return -1;
        if (nums.length == 1 && nums[0] == target) return 0;
        if (nums.length == 1 && nums[0] != target) return -1;

        int result = -1;
        int pivotIndex = binarySearchPivot(nums);
        if (pivotIndex == -1) 
            result = binarySearch(nums, target, 0, nums.length - 1);
        else if (target >= nums[0])
            result = binarySearch(nums, target, 0, pivotIndex);
        else
            result = binarySearch(nums, target, pivotIndex + 1, nums.length - 1);

        return result;
    }

	int binarySearch(int[] nums, int target, int start, int end) {
		while (start <= end) {
            int mid = (start + end) / 2;
			if (nums[mid] > target) end = mid - 1;
            else if (nums[mid] < target) start = mid + 1;
            else return mid;
        }
        
		return -1;
	}

	int binarySearchPivot(int[] nums) {
        int start = 0, end = nums.length - 1;
        if (nums.length == 2 && nums[0] > nums[1]) return 0;
        int len = nums.length;

		while (start < end) {
            int mid = (start + end) / 2;
            if (mid > 0 && mid <= len - 1 && nums[mid-1] > nums[mid]) return mid-1;
            if (mid >= 0 && mid <= len - 2 && nums[mid] > nums[mid+1]) return mid;

			int diff = nums[mid] - nums[start];
			if (diff < 0) end = mid - 1;
			else start = mid + 1;
        }
        
		return -1;
	}  
}
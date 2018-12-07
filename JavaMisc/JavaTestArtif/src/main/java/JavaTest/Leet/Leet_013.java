package JavaTest.Leet;

class Leet_013{
    public void test(){
        int[] nums5 = new int[] { 2, 3, 1, 3, 3};       // 2, 3, 1, 3, 3
        nextPermutation(nums5);

        int[] nums4 = new int[] { 2, 3, 1};
        nextPermutation(nums4);

        int[] nums = new int[] { 1, 40, 60, 55, 50};    // 1, 50, 60, 55, 40
        nextPermutation(nums);

        int[] nums0 = new int[] { 1, 3, 2};
        nextPermutation(nums0);

        int[] nums1 = new int[] { 1, 2, 3};
        nextPermutation(nums1);

        int[] nums2 = new int[] { 3, 2, 1};
        nextPermutation(nums2);  

        int[] nums3 = new int[] { 1, 1, 5};
        nextPermutation(nums3);        
    }

    public void nextPermutation(int[] nums) {
        if (nums.length <= 1) return;
        if (makeBiggerPermutation(nums)) return;
        reverseNums(nums, 0, nums.length - 1);
    }

    private boolean makeBiggerPermutation(int[] nums) {
		for (int i = nums.length - 2; i >= 0; i--){
            if (nums[i] < nums[i + 1]){
                int j = getIndexOfSmallestElemBiggerThan(nums, i);
                swap(nums, i, j);
                reverseNums(nums, i + 1, nums.length - 1);
                return true;
            }
        }

        return false;
    }

    private void swap(int[] nums, int startIndex, int endIndex) {
        int lastValue = nums[endIndex];
        nums[endIndex] = nums[startIndex];
        nums[startIndex] = lastValue;
    }

    private int getIndexOfSmallestElemBiggerThan(int[] nums, int index) {
        int n = nums[index];

        for (int i = nums.length - 1; i > index + 1; i--)
            if (nums[i] > n)
                return i;

        return index + 1;
    }

    private void reverseNums(int[] nums, int startIndex, int endIndex) {
        int len = endIndex - startIndex + 1;
        for (int i = 0; i < len / 2; i++) {
            int k = startIndex + i;
            int j = startIndex + (len - i - 1);

            int temp = nums[k];
            nums[k] = nums[j];
            nums[j] = temp;
        }
    }
}
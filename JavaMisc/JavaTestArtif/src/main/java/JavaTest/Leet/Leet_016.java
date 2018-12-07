package JavaTest.Leet;
import java.util.*;

public class Leet_016 {
    public void test(){
        //int[] nums = new int[] { -1, 2, 1, -4 };
        int[] nums = new int[] { 1, 1, 1, 0 };
        int res = threeSumClosest(nums, -100);
        System.out.println("--------");
        System.out.println(res);
    }

    public int threeSumClosest(int[] nums, int target) {
        Arrays.sort(nums);
        int closestSum = 0;
        int minDelta = Integer.MAX_VALUE;

        for (int i = 0; i + 2 < nums.length; i++) {
            int j = i + 1, k = nums.length - 1;  
            int target2 = target - nums[i];
            int sum2;

            while (j < k) {
                sum2 = nums[j] + nums[k];
                int sum = nums[i] + sum2;
                int delta = Math.abs(target - sum);

                if (delta < minDelta) {
                    minDelta = delta;
                    closestSum = sum;
                }

                if (sum2 == target2) return target;
                else if (sum2 > target2) k--;
                else j++;
            }

        }

        return closestSum;
    }
}
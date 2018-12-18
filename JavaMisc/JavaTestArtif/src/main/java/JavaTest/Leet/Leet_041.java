package JavaTest.Leet;

//import java.util.Arrays;
import java.util.HashSet;

public class Leet_041 {
    public void test(){
        int[][] nums = { 
            {1}, 
            {1, 2},
            {2, 1},
            {1, 3},
            {3, 1},
            {1, 2, 7}, 
            {3, -1, 2, 4, -3, 1}
        };

        for (int[] n: nums)
            System.out.println(firstMissingPositive(n));
    }

    public int firstMissingPositive(int[] nums) {
        HashSet<Integer> numsSet = new HashSet<Integer>(nums.length);
        for (int n: nums) 
            if (n > 0)
                numsSet.add(n);
        
        for (int i = 1; ; i++)
            if (!numsSet.contains(i))
                return i;
    }
}
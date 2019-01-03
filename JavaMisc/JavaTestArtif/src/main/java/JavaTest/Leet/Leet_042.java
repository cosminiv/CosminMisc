package JavaTest.Leet;

public class Leet_042 {
    public void test() {
        int[] height = new int[] {0,1,0,2,1,0,1,3,2,1,2,1};
        int res = trap(height);
        System.out.println(res);
    }

    public int trap(int[] height) {
        if (height == null || height.length < 3) return 0;

        int start = 0, end = 0;
        int result = 0; 

        while (true){
            int tempSum = 0;
            while (start < height.length - 1 && height[start+1] >= height[start]) 
                start++;

            if (start >= height.length - 1) break;

            end = start + 1;
            while (end < height.length - 1 && height[end+1] <= height[end]) {
                tempSum += height[start] - height[end];
                end++;
            }
            
            if (end >= height.length - 1) break;

            start = end;
        }
        
        return result;
    }
}
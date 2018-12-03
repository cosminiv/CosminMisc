package JavaTest;

// Get maximum sum of non-consecutive numbers in an array
// Numbers can be negative as well
//
class DCP_009{
    void test(){
        //int[] arr = new int[] { -3, -6, 0, -19, -2, -10, -20};  // 0
        //int[] arr = new int[] { 3, 6, 7, 19, 2, -10, -20};  // 6 + 19 = 25
        //int[] arr = new int[] { -3, -6, -1, -19, -2, 4, -20};  // 4
        //int[] arr = new int[] { -3, -6, -1, -19, -2, 4, 20};  // 20
        //int[] arr = new int[] { -3, -6, -1, -19, -2, 4, 20, 1};  // 20
        //int[] arr = new int[] { -3, 3, -1, -19, -2, 4, 20, 1};  // 3 + 20 = 23
        int[] arr = new int[] { -3, 8, -1, -19, -2, 8, 20, 8};  // 8 + 20 = 28

        try{
            int s1 = getMaxSum(arr);
            System.out.printf("Max sum: %d", s1);
        }
        catch (Exception ex){
            System.out.println(ex);
        }
    }

    int getMaxSum(int[] arr) throws Exception {
        if (arr == null) throw new Exception("Null argument");
        if (arr.length == 0) return 0;
        if (arr.length == 1) return arr[0];

        int maxNegative = getMaxIfAllNegative(arr);
        if (maxNegative < 0) return maxNegative;

        Sum s1 = getFirstPositive(arr, 0);
        Sum s2 = getFirstPositive(arr, s1.LastIndex + 1);
        if (s2 == null) return s1.Value;

        for (int i = s2.LastIndex + 1; i < arr.length; i++) {
            int n = arr[i];
            if (n <= 0) continue;
            Sum s = s1.LastIndex < i - 1 ? s1 : s2;
            s.Value += n;
            s.LastIndex = i;
        }

        if (s1.Value > s2.Value) return s1.Value;
        else return s2.Value;
    }

    private int getMaxIfAllNegative(int[] arr) {
        int max = arr[0];
        for (int n: arr){
            if (n >= 0) return 0;
            if (n > max) max = n;
        }
        return max;
    }

    Sum getFirstPositive(int[] arr, int startIndex){
        for (int i = startIndex; i < arr.length; i++)
            if (arr[i] >= 0) 
                return new Sum(arr[i], i);

        return null;
    }

    static class Sum {
        int Value;
        int LastIndex;

        Sum(int value, int lastIndex) {
            Value = value;
            LastIndex = lastIndex;
        }
    }
}
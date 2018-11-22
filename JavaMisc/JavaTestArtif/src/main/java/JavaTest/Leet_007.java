package JavaTest;

class Leet_007 {
    public void test(){
        Test[] tests = new Test[]{
            new Test(1534236469, 0),
            new Test(1, 1),
            new Test(0, 0),
            new Test(12, 21),
            new Test(712, 217),
            new Test(700, 7),
            new Test(-30040, -4003),
        };

        for (int i = 0; i < tests.length; i++){
            Test test = tests[i];
            int n = reverse(test.a);

            if (n == test.b)
                System.out.println(String.format("%d -> %d OK", test.a, n));
            else
                System.out.println(String.format("%d -> %d failed; should be %d", test.a, n, test.b));
        }
    }

    public int reverse(int n) {
        int[] digits = new int[50];
        int i = 0;
        long powerOfTen = 0;
        boolean isNegative = n < 0;
        if (isNegative)
            n = -n;

        for (int n2 = n; n2 > 0; n2 = n2 / 10) {
            digits[i++] = n2 % 10;
            powerOfTen = powerOfTen == 0 ? 1 : powerOfTen * 10;  
        }

        long result = 0;
        
        for (int digitIndex = 0; powerOfTen > 0; powerOfTen /= 10){
            result += digits[digitIndex] * powerOfTen;
            digitIndex++;
        }

        if (isNegative)
            result = -result;

        if (result < Integer.MIN_VALUE || result > Integer.MAX_VALUE)
            return 0;

        return (int)result;
    }    

    static class Test{
        public int a;
        public int b;
        
        public Test(int aa, int bb){
            a = aa;
            b = bb;
        }
    }
}
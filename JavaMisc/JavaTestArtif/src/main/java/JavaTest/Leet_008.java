// Implement atoi which converts a string to an integer.
// Discard leading spaces and trailing text
//
package JavaTest;

class Leet_008 {
    void test(){
        // int a = myAtoi("");
        // int b = myAtoi("1");
        // int c = myAtoi("45");
        // int d = myAtoi("+3");
        // int e = myAtoi("-999");
        // int f = myAtoi(" ");
        // int g = myAtoi("  76");
        // int h = myAtoi(" -4 ");
        // int i = myAtoi("f3");
        // int j = myAtoi("--6");
        // int k = myAtoi(" 501 -4");
        // int l = myAtoi(" 501y");
        // int m = myAtoi("+qwerty6");
        // int n = myAtoi("1q2w3e4r5ty6");
        // int o = myAtoi("x1q2w3e4r5ty6");
        // int p = myAtoi("  987654398765439876543  xx");
        // int q = myAtoi("  -987654398765439876543#######");
    }

    public int myAtoi(String str) {
        long result = 0;
        boolean trimSpaces = true;
        boolean foundSign = false;
        boolean foundDigits = false;
        int sign = 1;

        for (int i = 0; i < str.length(); i++) {
            char c = str.charAt(i);
            boolean isDigit = c >= '0' && c <= '9';
            boolean isSign = c == '-' || c == '+';
            boolean isOther = !isDigit && !isSign && c != ' ';
            
            if (isOther) break;
            if (!isDigit && foundDigits) break;
            if (!isDigit && foundSign) break;

            if (c == ' '){
                if (trimSpaces) continue;
                else break;
            }
            else trimSpaces = false;

            if (c == '-') {
                if (foundSign) break;
                sign = -1;
                foundSign = true;
            }
            
            if (c == '+') {
                if (foundSign) break;
                sign = 1;
                foundSign = true;
            }
            
            if (isDigit) {
                foundDigits = true;
                int digit = Character.getNumericValue(c);
                result = result * 10 + sign * digit;
                if (result < Integer.MIN_VALUE) return Integer.MIN_VALUE;
                if (result > Integer.MAX_VALUE) return Integer.MAX_VALUE;
            }
        }

        return (int)result;
    }    
}
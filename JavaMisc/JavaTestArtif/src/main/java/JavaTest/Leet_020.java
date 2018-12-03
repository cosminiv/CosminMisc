// Validate Parentheses: (), [], {}
//
package JavaTest;
import java.util.Stack;

class Leet_020{
    public void test(){
        // boolean a = isValid("");
        // boolean b = isValid("(");
        // boolean c = isValid(")");
        // boolean d = isValid(")(");
        // boolean e = isValid("()");
        // boolean f = isValid("(()");
        // boolean g = isValid("([])");
        // boolean h = isValid("[(])");
        // boolean i = isValid("[]{}");
        // boolean j = isValid("{([]{})()}");
    }

    public boolean isValid(String s) {
        Stack<Character> stack = new Stack<Character>();
        for (int i = 0; i < s.length(); i++){
            char c = s.charAt(i);
            if (c == '(' || c == '[' || c == '{')
                stack.push(c);
            else {
                if (stack.size() == 0) return false;

                char prevChar = stack.peek();
                if (c == ')' && prevChar != '(') return false;
                if (c == ']' && prevChar != '[') return false;
                if (c == '}' && prevChar != '{') return false;

                stack.pop();
            }
        }

        return stack.size() == 0;
    }    
}
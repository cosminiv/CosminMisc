package JavaTest.Leet;
import java.util.*;

public class Leet_017 {
    public void test(){
        List<String> c = letterCombinations("293");
        for (String c2: c)
            System.out.printf("%s, ", c2);
    }

    public List<String> letterCombinations(String digits) {
        if (digits.isEmpty()) return new ArrayList<String>();
        HashMap<Character, String> map = makeLetterMap();
        List<String> prevSizeComb = new ArrayList<String>();

        // init list with first letter
        Character inputDigit = digits.charAt(0);
        String firstLetters = map.get(inputDigit);
        for (int i = 0; i < firstLetters.length(); i++)
            prevSizeComb.add(((Character)firstLetters.charAt(i)).toString());

        // Add the other letters
        for (int i = 1; i < digits.length(); i++) {
            List<String> crtSizeComb = new ArrayList<String>();
            inputDigit = digits.charAt(i);
            String letters = map.get(inputDigit);

            for (int j = 0; j < prevSizeComb.size(); j++){
                for (int l = 0; l < letters.length(); l++){
                    String value = prevSizeComb.get(j) + letters.charAt(l);
                    crtSizeComb.add(value);
                }
            }

            prevSizeComb = crtSizeComb;
        }

        return prevSizeComb;
    }

    HashMap<Character, String> makeLetterMap(){
        HashMap<Character, String> map = new HashMap<Character, String>();
        map.put('2', "abc");
        map.put('3', "def");
        map.put('4', "ghi");
        map.put('5', "jkl");
        map.put('6', "mno");
        map.put('7', "pqrs");
        map.put('8', "tuv");
        map.put('9', "wxyz");
        return map;
    }
}
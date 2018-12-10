// Find all words in a list in a string
//
package JavaTest.Leet;
import java.util.*;

public class Leet_030{ 
	public void test(){ 
		//String[] words = new String[] {"abba", "ahahve", "bar", "foo", "xbar"};
		//int i = new Leet_030().substrBinarySearch(words, "a");
 	 //System.out.printf("%d", i);
 	 
 	 String[] words = new String[] {"Dog", "Cat", "Horse", "Bird"};
 	 String text = "--CatHorseCatBirdCatDog---CatHorseDogBird-";
		System.out.println(text);
		System.out.println(); 		
		 		
		List<Integer> res = new Leet_030().findSubstring(text, words);
		
		for (Integer i: res) System.out.printf("%d ", i);
		if (res.isEmpty()) System.out.println("Not found!");
 	}
 
  	// Return list of indices where all the words are present exactly once in string s
    public List<Integer> findSubstring(String s, String[] words) {
      List<Integer> result = new ArrayList<Integer>();
      if (s == null || s.length() == 0 || words == null || words.length == 0)
      	return result;
      int wordsLen = 0;
      for (String word: words) wordsLen += word.length();
      if (wordsLen > s.length()) return result;
      
      Arrays.sort(words);
      //HashSet<String> wordsFound = new HashSet<String>(words.length);
      int[] wordsFound = makeIndexArray(words.length);

			// iterate through the string and try to match words 			   
			int candidateResult = 0, wordStart = 0, wordEnd = 1, countWordsFound = 0;
			
			for (; wordStart < s.length() && wordEnd <= s.length(); ){
				String substr = s.substring(wordStart, wordEnd);
				int i = substrBinarySearch(words, substr);
				boolean foundSubstring = i >= 0;
				
				if (!foundSubstring) {
					wordStart++;
					wordEnd = wordStart + 1;
 				 candidateResult = wordStart;
 				 wordsFound = makeIndexArray(words.length);
 				 continue;
				}				
				
				// Check if we found a full word which is not a substring of a bigger word
				boolean foundFullWord = substr.length() == words[i].length() ;
				boolean foundBiggerWord = false;
				if (foundFullWord) {
					int wordEnd2 = wordEnd + 1;
					if (wordEnd2 <= s.length()) {
   			 	String substr2 = s.substring(wordStart, wordEnd2);
	  				int j = substrBinarySearch(words, substr2);
	  				if (j >= 0 && wordsFound[j] < 0)
	  					foundBiggerWord = true;
					}
				}
								
				boolean foundDuplicate = wordsFound[i] >= 0;
					
				if (foundFullWord && !foundBiggerWord && !foundDuplicate){
  				System.out.printf("found word \"%s\" (%d) at index %d\n", substr, i, wordStart);
  				countWordsFound++;
	  			wordsFound[i] = wordStart;
		  		wordStart = wordEnd;
		  		wordEnd = wordStart + 1;
		  			
			  	if (countWordsFound == words.length){
 		  		 System.out.printf("--------------\nMatched all at index %d\n\n", candidateResult);
				  	result.add(candidateResult);
 				   candidateResult = wordEnd;
					  wordsFound = makeIndexArray(words.length);
					}
				}else{
  			//System.out.printf("found substring \"%s\" at index %d\n", substr, wordStart); 						
				wordEnd++;
				}				
			}
			
      return result;
    }
  
    int[] makeIndexArray(int size){
    	int[] result = new int[size];
    	for (int i = 0; i < size; i++)
    		result[i] = -1;
    	return result;
    }
  
  	int substrBinarySearch(String[] words, String prefix){
  		int start = 0, end = words.length - 1;
 		 //System.out.printf("Searching for: %s\n", prefix);
 		 
  		while (start <= end){
 			 int mid = (start + end) / 2;
  			//System.out.printf("start = %d, end = %d, mid = %d\n", start, end, mid);
  			String word = words[mid];
  			if (word.indexOf(prefix) == 0){/*System.out.printf("%s in %s\n", prefix, word);*/return mid;}
  			//if (start == end) return -1;
  			int c = word.compareTo(prefix);
  			if (c < 0) { start = mid + 1; /*System.out.printf("%s < %s\n", word, prefix);*/ }
  			else { end = mid - 1; /* System.out.printf("%s > %s\n", word, prefix); */ }
  		}
  		return -1;
  	}
}
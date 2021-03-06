// Find all words in a list in a string
//
package JavaTest.Leet;

import java.util.*;

public class Leet_030 {
	public void test() {
		String text = "wordgoodgoodgoodbestword";
		String[] words = new String[] { "word","good","best","good" };

		System.out.println(text);
		System.out.println("01234567890123456789012345678901234567890123456");
		System.out.println();

		List<Integer> res = new Leet_030().findSubstring(text, words);

		for (Integer i : res)
			System.out.printf("%d ", i);
		if (res.isEmpty())
			System.out.println("Not found!");
	}

	// Return list of indices where all the words are present exactly once in string s
	//
	public List<Integer> findSubstring(String s, String[] wordsX) {
		List<Integer> result = new ArrayList<Integer>();
		if (s == null || s.length() == 0 || wordsX == null || wordsX.length == 0)
			return result;
		int wordsLen = 0;
		for (String word : wordsX)
			wordsLen += word.length();
		if (wordsLen > s.length())
			return result;

		Arrays.sort(wordsX);
		List<String> words = Arrays.asList(Arrays.copyOf(wordsX, wordsX.length));
		int[] wordsFound = makeIndexArray(words.size());

		// iterate through the string and try to match words
		int candidateResult = 0, wordStart = 0, wordEnd = 1, countWordsFound = 0;

		for (; wordStart < s.length() && wordEnd <= s.length();) {
			String substr = s.substring(wordStart, wordEnd);
			// First try to find full word
			int wordIndex = binarySearch(words, substr);
			// If full word not found, search only for prefix
			if (wordIndex < 0) 
				wordIndex = substrBinarySearch(words, substr);

			boolean foundSubstring = wordIndex >= 0;

			if (!foundSubstring) {
				wordStart++;
				wordEnd = wordStart + 1;
				candidateResult = wordStart;
				wordsFound = makeIndexArray(words.size());
				countWordsFound = 0;
				//words = Arrays.asList(Arrays.copyOf(wordsX, wordsX.length));
				printf("Have not found substring %s, resetting to index %d \n", substr, wordStart);
				continue;
			}

			// Check if we found a full word which is not a substring of a bigger word
			boolean foundFullWord = substr.length() == words.get(wordIndex).length();
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

			if (!foundFullWord || foundBiggerWord){
				wordEnd++;
				continue;
			}

			countWordsFound++;
			boolean foundDuplicate = foundFullWord && wordsFound[wordIndex] >= 0;

			// Remove the words found which appear before the duplicate
			if (foundDuplicate){
				printf("found duplicate %s\n", words.get(wordIndex));
				int indexCutoff = wordsFound[wordIndex];

				for (int j = 0; j < wordsFound.length; j++){
					if (wordsFound[j] >= 0 && wordsFound[j] <= indexCutoff){
						String word = words.get(j);
						wordsFound[j] = -1;
						countWordsFound--;
						int wordLen = word.length();
						candidateResult += wordLen;

						printf("removing word \"%s\" from match\n", word);
						printf("candidateResult = %d\n", candidateResult);
						printf("countWordsFound = %d\n\n", countWordsFound);
					}
				}
			}
			
			printf("found word \"%s\" (%d) at index %d; countWordsFound = %d\n", 
				substr, wordIndex, wordStart, countWordsFound);
			wordsFound[wordIndex] = wordStart;
			wordStart = wordEnd;
			wordEnd = wordStart + 1;
			//words.remove(wordIndex);

			if (countWordsFound == words.size()) {
				//printf("--------------\nMatched all at index %d\n\n", candidateResult);
				result.add(candidateResult);

				// Move the start index only one word to the right, in case we can match the following word
				int charCountOffset = 0;
				for (int j = 0; j < wordsFound.length; j++) {
					if (wordsFound[j] == candidateResult){
						wordsFound[j] = -1;
						charCountOffset = words.get(j).length();
						break;
					}
				}

				candidateResult += charCountOffset;
				//wordsFound[] = makeIndexArray(words.length);
				countWordsFound--;
			}
		}

		return result;
	}

	void printf(String format, Object...params){
		System.out.printf(format, params);
	}

	int[] makeIndexArray(int size) {
		int[] result = new int[size];
		for (int i = 0; i < size; i++)
			result[i] = -1;
		return result;
	}

	int substrBinarySearch(List<String> words, String prefix) {
		int start = 0, end = words.size() - 1;
		// System.out.printf("Searching for: %s\n", prefix);

		while (start <= end) {
			int mid = (start + end) / 2;
			String word = words.get(mid);
			if (word.indexOf(prefix) == 0) {
				return mid;
			}
			int c = word.compareTo(prefix);
			if (c < 0) {
				start = mid + 1;
			} else {
				end = mid - 1;
			}
		}
		return -1;
	}

	int binarySearch(List<String> words, String toFind) {
		int start = 0, end = words.size() - 1;
		// System.out.printf("Searching for: %s\n", prefix);

		while (start <= end) {
			int mid = (start + end) / 2;
			String word = words.get(mid);
			int res = word.compareTo(toFind);
			
			if (res == 0) return mid;
			else if (res < 0) start = mid + 1;
			else end = mid - 1;
		}
		return -1;
	}	
}
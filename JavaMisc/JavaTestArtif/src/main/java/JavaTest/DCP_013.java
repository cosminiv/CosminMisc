// Find the longest substring formed of at most X distinct characters
//
package JavaTest;

import java.util.*;

class DCP_013 {
	void test() {
		String[] input = new String[] { "xxbbaaQxxbbabaxadddy", "ytbbaaadaddty" };
		int maxChars = 2;

		DCP_013 solver = new DCP_013();

		for (String str : input) {
			String solution = solver.getLongestSubstring(str, maxChars);
			System.out.printf("%s: %s \n", str, solution);
		}
	}

	String getLongestSubstring(String input, int maxChars) {
		if (input.length() == 0)
			return "";
		int maxLenFound = 0, start = 0, end = 0, maxLenStart = 0, maxLenEnd = 0;
		HashMap<Character, Integer> map = new HashMap<Character, Integer>();

		for (; end < input.length(); end++) {
			Character c = input.charAt(end);
			Integer count = map.get(c);

			if (count != null) count++;
			else count = 1;

			map.put(c, count);
			int len = end - start + 1;
			int mapSize = map.size();

			if (mapSize <= maxChars) {
				if (len > maxLenFound) {
					maxLenFound = len;
					maxLenStart = start;
					maxLenEnd = end;
				}
			} else {
				do {
					Character c2 = input.charAt(start);
					Integer oldVal = map.get(c2);
					map.put(c2, oldVal - 1);
					if (map.get(c2) == 0) {
						map.remove(c2);
					}
					start++;
				} while (map.size() > maxChars);
			}
		}

		return input.substring(maxLenStart, maxLenEnd + 1);
	}
}
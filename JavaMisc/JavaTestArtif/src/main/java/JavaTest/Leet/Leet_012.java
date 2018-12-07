// Integer to Roman number
//
package JavaTest.Leet;


public class Leet_012 {
	public void test() {
		int[] n = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25,
				30, 31, 34, 36, 39, 40, 49, 50, 99, 100, 101, 174, 199, 200, 248, 399, 433, 2949 };

		Leet_012 solver = new Leet_012();
		int i = 0;

		for (int a : n) {
			System.out.printf("%d = %s, ", a, solver.intToRoman(a));
			if (++i % 10 == 0)
				System.out.println();
		}
	}

	String intToRoman(int n) {
		StringBuilder result = new StringBuilder(16);
		int thousands = n / 1000;
		if (thousands > 0)
			for (int i = 1; i <= thousands; i++)
				result.append("M");

		int hundreds = (n % 1000) / 100;
		int tens = (n % 100) / 10;
		int units = (n % 10);

		addRomanDigits(result, hundreds, "C", "D", "M");
		addRomanDigits(result, tens, "X", "L", "C");
		addRomanDigits(result, units, "I", "V", "X");

		return result.toString();
	}

	void addRomanDigits(StringBuilder result, int n, String romanUnit, String romanFiveUnits, String romanTenUnits) {
		if (n == 0)
			return;

		if (n == 1) {
			result.append(romanUnit);
		} else if (n == 2) {
			result.append(romanUnit).append(romanUnit);
		} else if (n == 3) {
			result.append(romanUnit).append(romanUnit).append(romanUnit);
		} else if (n == 4) {
			result.append(romanUnit).append(romanFiveUnits);
		} else if (n < 9) {
			result.append(romanFiveUnits);
			if (n == 6) {
				result.append(romanUnit);
			} else if (n == 7) {
				result.append(romanUnit).append(romanUnit);
			} else if (n == 8) {
				result.append(romanUnit).append(romanUnit).append(romanUnit);
			}
		} else
			result.append(romanUnit).append(romanTenUnits);
	}
}

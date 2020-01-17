
namespace ConsoleApp1.Leet
{
    class Leet_066
    {
        public int[] PlusOne(int[] digits)
        {
            int[] result = new int[digits.Length];

            int carry = 0;
            int resultLastDigit = digits[digits.Length - 1] + 1;

            if (resultLastDigit >= 10)
            {
                resultLastDigit -= 10;
                carry = 1;
            }

            result[digits.Length - 1] = resultLastDigit;

            for (int i = digits.Length - 2; i >= 0; i--)
            {
                result[i] = digits[i] + carry;

                if (result[i] >= 10)
                {
                    result[i] -= 10;
                    carry = 1;
                }
                else
                    carry = 0;
            }

            if (carry > 0)
            {
                // Make a bigger array
                int[] result2 = new int[digits.Length + 1];
                result2[0] = carry;
                for (int i = 0; i < result.Length; i++)
                    result2[i + 1] = result[i];
                result = result2;
            }

            return result;
        }
    }
}

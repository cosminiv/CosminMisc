using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleApp1.Eu._001_050
{
    public class Problem_019
    {
        static readonly Dictionary<int, int> _monthLenghts = new Dictionary<int, int> {
            { 0, 31 }, { 2, 31 }, { 3, 30 }, { 4, 31 }, { 5, 30 }, { 6, 31 }, { 7, 31 }, { 8, 30 }, { 9, 31 }, { 10, 30 }, { 11, 31 },
        };

        public static int Solve()
        {
            int SundayCount = 0;
            int dayOfWeek = 0;

            for (int year = 1900; year <= 2000; year++)
            {
                for (int month = 0; month < 12; month++)
                {
                    int monthLength = GetMonthLength(month, year);
                    dayOfWeek = (dayOfWeek + (monthLength % 7)) % 7;

                    if (dayOfWeek == 6 && year > 1900)
                    {
                        bool isOutsideInterval = (year == 2000 && month == 11);  // this is actually 1 Jan 2001
                        if (!isOutsideInterval)
                            SundayCount++;
                        else
                            Debug.Print("Nice try :)");
                    }
                }
            }

            return SundayCount;
        }

        private static int GetMonthLength(int month, int year)
        {
            if (month != 1) return _monthLenghts[month];
            
            // February
            if (year % 400 == 0) return 29;
            else if (year % 100 == 0) return 28;
            else if (year % 4 == 0) return 29;
            else return 28;
        }
    }
}

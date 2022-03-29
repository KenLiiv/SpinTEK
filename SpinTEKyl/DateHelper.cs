using System;
using System.Collections.Generic;

namespace SpinTEKyl
{
    public class DateHelper
    {
        private static string YearBeingCalculated { get; set; } = null!;

        private static List<DateTime> HolidayList = new()
        {
            new DateTime(DateTime.Now.Year, 1, 1),
            new DateTime(DateTime.Now.Year, 2, 24),
            new DateTime(DateTime.Now.Year, 5, 1),
            new DateTime(DateTime.Now.Year, 5, 23),
            new DateTime(DateTime.Now.Year, 6, 23),
            new DateTime(DateTime.Now.Year, 6, 24),
            new DateTime(DateTime.Now.Year, 8, 20),
            new DateTime(DateTime.Now.Year, 12, 24),
            new DateTime(DateTime.Now.Year, 12, 25),
            new DateTime(DateTime.Now.Year, 12, 26)
        };

        public DateHelper(string year)
        {
            YearBeingCalculated = year;
            
            DateTime easterDate = EasterSunday(int.Parse(year));
            
            HolidayList.Add(easterDate); // Add easter sunday for that year, as it is a moving holiday
            HolidayList.Add(easterDate.AddDays(-2)); // Suur reede
        }

        //  Algorithm for calculating moving holiday
        //  https://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays
        private static DateTime EasterSunday(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day   = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day);
        }

        private static bool IsHoliday(DateTime date) => HolidayList.Contains(date);

        private static bool IsWeekend(DateTime date) => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;

        private static bool IsWorkingDay(DateTime date) => !IsHoliday(date) && !IsWeekend(date);

        public int GetYear() => int.Parse(YearBeingCalculated);
        
        public string GetDateTimeDateString(DateTime date) => date.ToString("dd/MM/yyyy");

        public DateTime GetClosestWorkingDayDateTime(DateTime date)
        {
            while (!IsWorkingDay(date))
            {
                date = date.AddDays(-1);
            }

            return date;
        }
    }
}



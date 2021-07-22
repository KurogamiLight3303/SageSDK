using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.CIUtils
{
    public struct Week
    {
        public Week(int weekNo, int year)
        {
            WeekNo = weekNo;
            Year = year;
        }
        public int WeekNo { get; set; }
        public int Year { get; set; }
    }

    public static class DateUtils
    {
        public static DateTime GetDateByYearDay(int day, int year = -1)
        {
            if (year <= DateTime.MinValue.Year)
                year = DateTime.Now.Year;
            return new DateTime(year, 1, 1).AddDays(day - 1);
        }

        public static int GetWeekNumber(DateTime date)
        {
            int week = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            if (week == 53)
                return 1;
            else
                return week;
        }

        public static Week GetWeek(DateTime date)
        {
            int week = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
            if (week == 53)
                return new Week(1, date.Year + 1);
            else
                return new Week(week, date.Year);
        }

        public static DateTime GetWeekStartDate(Week week)
        {
            return GetWeekStartDate(week.WeekNo, week.Year);
        }

        public static DateTime GetWeekStartDate(int weekNo, int year)
        {
            DateTime temp = new DateTime(year, 1, 1);
            while (temp.DayOfWeek != DayOfWeek.Monday)
                temp = temp.AddDays(1);
            int WeekOffSet = CultureInfo.CurrentUICulture.Calendar.GetWeekOfYear(temp, CalendarWeekRule.FirstDay, DayOfWeek.Monday);

            return temp.AddDays((weekNo - WeekOffSet) * 7);
        }

        public static DateTime GetFirtsMondayMonth(int month, int year)
        {
            DateTime date = new DateTime(year, month, 1);
            while (date.DayOfWeek != DayOfWeek.Monday) date = date.AddDays(1);
            return date;
        }
        public static DateTime GetLastSundayMonth(int month, int year)
        {
            if (month == 12)
                return GetFirtsMondayMonth(1, year + 1).AddDays(-1);
            return GetFirtsMondayMonth(month + 1, year).AddDays(-1);
        }
    }
}

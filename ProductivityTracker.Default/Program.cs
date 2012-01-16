using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client.Document;

namespace ProductivityTracker.Default
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoadIndustries();
            //LoadStatuses();
            Console.ReadLine();
        }

        public static int GetWeekOfMonth(DateTime date)
        {
            var day = date.Day;
            if (day >= 1 && day <= 7) return 1;
            if (day >= 8 && day <= 14) return 2;
            if (day >= 15 && day <= 28) return 3;
            return 4;
        }

        public static int GetWeekNumber(DateTime date)
        {
            // Updated 2004.09.27. Cleaned the code and fixed abug. Compared the algorithm with
            // code published here . Tested code successfully against the other algorithm 
            // for all dates in all years between 1900 and 2100.
            // Thanks to Marcus Dahlberg for pointing out the deficient logic.

            // Calculates the ISO 8601 Week Number
            // In this scenario the first day of the week is monday, 
            // and the week rule states that:
            // [...] the first calendar week of a year is the one 
            // that includes the first Thursday of that year and 
            // [...] the last calendar week of a calendar year is 
            // the week immediately preceding the first 
            // calendar week of the next year.
            // The first week of the year may thus start in the 
            // preceding year

            const int JAN = 1;
            const int DEC = 12;
            const int LASTDAYOFDEC = 31;
            const int FIRSTDAYOFJAN = 1;
            const int THURSDAY = 4;
            bool ThursdayFlag = false;

            // Get the day number since the beginning of the year
            int DayOfYear = date.DayOfYear;

            // Get the numeric weekday of the first day of the 
            // year (using sunday as FirstDay)
            var StartWeekDayOfYear =
                (int)(new DateTime(date.Year, JAN, FIRSTDAYOFJAN)).DayOfWeek;
            var EndWeekDayOfYear =
                (int)(new DateTime(date.Year, DEC, LASTDAYOFDEC)).DayOfWeek;

            // Compensate for the fact that we are using monday
            // as the first day of the week
            if (StartWeekDayOfYear == 0)
                StartWeekDayOfYear = 7;
            if (EndWeekDayOfYear == 0)
                EndWeekDayOfYear = 7;

            // Calculate the number of days in the first and last week
            int DaysInFirstWeek = 8 - (StartWeekDayOfYear);

            // If the year either starts or ends on a thursday it will have a 53rd week
            if (StartWeekDayOfYear == THURSDAY || EndWeekDayOfYear == THURSDAY)
                ThursdayFlag = true;

            // We begin by calculating the number of FULL weeks between the start of the year and
            // our date. The number is rounded up, so the smallest possible value is 0.
            var FullWeeks = (int)Math.Ceiling((DayOfYear - (DaysInFirstWeek)) / 7.0);

            int WeekNumber = FullWeeks;

            // If the first week of the year has at least four days, then the actual week number for our date
            // can be incremented by one.
            if (DaysInFirstWeek >= THURSDAY)
                WeekNumber = WeekNumber + 1;

            // If week number is larger than week 52 (and the year doesn't either start or end on a thursday)
            // then the correct week number is 1.
            if (WeekNumber > 52 && !ThursdayFlag)
                WeekNumber = 1;

            // If week number is still 0, it means that we are trying to evaluate the week number for a
            // week that belongs in the previous year (since that week has 3 days or less in our date's year).
            // We therefore make a recursive call using the last day of the previous year.
            if (WeekNumber == 0)
                WeekNumber = GetWeekNumber(new DateTime(date.Year - 1, DEC, LASTDAYOFDEC));
            return WeekNumber;
        }

        static void LoadIndustries()
        {
            var store = new DocumentStore {Url = "http://localhost:8080"};
            store.Initialize();

            Console.WriteLine("Start loading industries");
            using(var session = store.OpenSession())
            {
                var ad = new Domain.Model.Industry {Name = "Advertising"};
                var it = new Domain.Model.Industry { Name = "IT Service" };
                var sw = new Domain.Model.Industry { Name = "Software" };
                var ind = new Domain.Model.Industry { Name = "Industrial" };
                var hc = new Domain.Model.Industry { Name = "HealthCare" };
                var ins = new Domain.Model.Industry { Name = "Insurance" };

                session.Store(ad);
                session.Store(it);
                session.Store(sw);
                session.Store(ind);
                session.Store(hc);
                session.Store(ins);

                session.SaveChanges();
            }
            Console.WriteLine("End loading industries");
        }

        static void LoadStatuses()
        {
            var store = new DocumentStore { Url = "http://localhost:8080" };
            store.Initialize();

            Console.WriteLine("Start loading statuses");
            using (var session = store.OpenSession())
            {
                var pending = new Domain.Model.Status {Name = "Pending Feedback"};
                var shortListed = new Domain.Model.Status { Name = "Shortlisted" };
                var rejected = new Domain.Model.Status {Name = "Rejected"};
                var intrv = new Domain.Model.Status { Name = "Interview" };
                var intrv1 = new Domain.Model.Status { Name = "Interview 1" };
                var intrv2 = new Domain.Model.Status { Name = "Interview 2" };

                session.Store(pending);
                session.Store(shortListed);
                session.Store(rejected);
                session.Store(intrv);
                session.Store(intrv1);
                session.Store(intrv2);

                session.SaveChanges();
            }
            Console.WriteLine("End loading statuses");
        }
    }
}

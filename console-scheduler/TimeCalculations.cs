using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleScheduler
{
    public class TimeCalculations
    {   
        public static TimeSpan MsTillNextInterval(M2MSchedule schedule)
        {
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
            // Adds however many minutes are specified in the intervals and sets the seconds to 00
            TimeOnly nowPlusInterval = new(currentTime.Hour, currentTime.Minute + schedule.Interval, 00);
            TimeSpan difference = nowPlusInterval - currentTime;
            return difference;
        }
        public static TimeSpan MsTillNextScheduledHour(M2MSchedule schedule)
        {
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
            TimeSpan difference = schedule.StartTime - currentTime;
            return difference;
        }
        public static TimeSpan MsTillNextScheduledDay(M2MSchedule schedule)
        {
            List<int> scheduledDays = new();
            foreach (var day in schedule.Days)
            {
                int dayOfWeekAsNum = Convert.ToInt16(GetDayOfWeekNumber(day));
                scheduledDays.Add(dayOfWeekAsNum);
            }

            // now, order the scheduled days by number in ascending order (small to big)
            scheduledDays.Sort();

            // Now find currentDay's number
            string currentShortDayName = DateTime.Now.DayOfWeek.ToString().Remove(3);
            int currentDayNumber = Convert.ToInt16(GetDayOfWeekNumber(currentShortDayName));

            int? nearestScheduledDay;
            int daysUntilNextRun = 0;
            try {
                nearestScheduledDay = scheduledDays.Where(x => x > currentDayNumber).First();
                daysUntilNextRun = Convert.ToInt32(nearestScheduledDay) - currentDayNumber;
            }
            catch { nearestScheduledDay = null; }
            if (!nearestScheduledDay.HasValue) { 
                nearestScheduledDay = scheduledDays.Where(x => x > 0).First();
                daysUntilNextRun = Convert.ToInt32(nearestScheduledDay) - (currentDayNumber - 7);
            }

            // Now create a new datetime object which is currentDateTime.days + daysUntilNextRun and then change .time to schedule.startTime
            DateOnly dateOfNextRun = DateOnly.FromDateTime(DateTime.Now.AddDays(daysUntilNextRun));
            DateTime nextScheduledStart = new (dateOfNextRun.Year,
                                         dateOfNextRun.Month,
                                         dateOfNextRun.Day,
                                         schedule.StartTime.Hour,
                                         schedule.StartTime.Minute,
                                         00);

            TimeSpan difference = nextScheduledStart - DateTime.Now;
            return difference;
        }
        private static int? GetDayOfWeekNumber(string day) => (day) switch
        {
            "Mon" => 1,
            "Tue" => 2,
            "Wed" => 3,
            "Thu" => 4,
            "Fri" => 5,
            "Sat" => 6,
            "Sun" => 7,
            _ => null
        };
    }
}

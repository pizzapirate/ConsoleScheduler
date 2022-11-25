using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleScheduler
{
    public static class ScheduleChecks
    {
        /// <summary>
        /// Checks if the current day of the week is a scheduled day.
        /// </summary>
        /// <param name="scheduledDays"></param>
        /// <returns></returns>
        public static bool ScheduledDayCheck(M2MSchedule schedule)
        {
            DateTime now = DateTime.Now;
            // Retrieves the current day of the week in three letters, the first of which being capital. 
            string currentShortDayName = now.DayOfWeek.ToString().Remove(3);

            // Check if the currentShortDayName exists in the schedule array
            if (schedule.Days.Contains(currentShortDayName)) { return true; }
            else { return false; }
        }
        /// <summary>
        /// Checks if the current hour is within the scheduled hours to run.
        /// </summary>
        /// <param name="scheduledDays"></param>
        /// <returns></returns>
        public static bool ScheduledHoursCheck(M2MSchedule schedule)
        {
            DateTime now = DateTime.Now;
            TimeOnly currentTime = TimeOnly.FromDateTime(now);
            if (currentTime >= schedule.StartTime && currentTime <= schedule.EndTime)
            {
                return true;
            }
            else { return false; }
        }
        /// <summary>
        /// Checks if it is the start of a minute.
        /// </summary>
        /// <returns></returns>
        public static bool OnTheMinuteCheck()
        {
            DateTime now = DateTime.Now;
            if (now.Second == 0) { return true; }
            else { return false; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleScheduler
{
    /// <summary>
    /// A struct to schedule daily automation of a program.
    /// </summary>
    public struct DailySchedule
    {
        /// <summary>
        /// Set the days of the week that you want the schedule to run. 
        /// </summary>
        public string[] Days { get; set; }
        /// <summary>
        /// Set the desired time to run for the schedule on each of the scheduled days. 
        /// </summary>
        public TimeOnly Time { get; set; }
        /// <summary>
        /// Create a new daily schedule for an event to occur. 
        /// </summary>
        /// <param name="days"></param>
        /// <param name="time"></param>
        public DailySchedule(string[] days, TimeOnly time)
        {
            Days = days;
            Time = time;
        }
        /// <summary>
        /// Set the task for the specified M2M schedule
        /// </summary>
        /// <param name="schedule"></param>
        public static void CreateTask(DailySchedule schedule, Delegate func)
        {
            while (true)
            {
                TimeSpan timeToWait;
                // Check if the current day of the week is a scheduled day:
                if (ScheduleChecks.ScheduledDayCheck(schedule))
                {   

                    // Checks if it is the scheduled time to run:
                    if (ScheduleChecks.ScheduledTimeCheck(schedule))
                    {
                        // This is where the program will be executed. 
                        func.DynamicInvoke();
                        timeToWait = TimeCalculations.MsTillNextScheduledTimeAndDay(schedule);
                        Console.WriteLine("Waiting " + Math.Round(timeToWait.TotalHours, 2) + " hours until next runtime");
                    }
                    // Calculate how many milliseconds until the next scheduled run time
                    else
                    {   
                        // Checks if the scheduled time has already been hit for today
                        if(schedule.Time > TimeOnly.FromDateTime(DateTime.Now))
                        {
                            timeToWait = TimeCalculations.MsTillNextScheduledTime(schedule);
                            Console.WriteLine("Waiting " + Math.Round(timeToWait.TotalMinutes, 2) + " minutes until next runtime");
                        }
                        // if it has been hit already, calculate the amount of ms until the next scheduled time and day
                        else
                        {
                            timeToWait = TimeCalculations.MsTillNextScheduledTimeAndDay(schedule);
                            Console.WriteLine("Waiting " + Math.Round(timeToWait.TotalMinutes, 2) + " minutes until next runtime");
                        }
                    }
                }
                // Calculate how many milliseconds until the next scheduled day and start time
                else
                {
                    timeToWait = TimeCalculations.MsTillNextScheduledTimeAndDay(schedule);
                    Console.WriteLine("Waiting " + Math.Round(timeToWait.TotalDays, 2) + " days until next runtime");
                }
                // Will pause the while loop for amount of milliseconds specified, so that it isn't constantly cycling through the while loop
                new ManualResetEvent(false).WaitOne(Convert.ToInt32(timeToWait.TotalMilliseconds));
            }
        }
    }
}

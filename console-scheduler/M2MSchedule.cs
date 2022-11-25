using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleScheduler
{   
    /// <summary>
    /// A struct to schedule minute to minute automation of a program.
    /// </summary>
    public struct M2MSchedule
    {   
        /// <summary>
        /// Set the days of the week that you want the schedule to run. 
        /// </summary>
        public string[] Days { get; set; }
        /// <summary>
        /// Set the desired start time for the schedule on each of the scheduled days. 
        /// </summary>
        public TimeOnly StartTime { get; set; }
        /// <summary>
        /// Set the desired end time for the schedule on each of the scheduled days.
        /// </summary>
        public TimeOnly EndTime { get; set;}
        /// <summary>
        /// Set the interval of minutes until the next scheduled event. Default is 0, which indicates an event will occur every minute.  
        /// </summary>
        public int Interval { get; set; }
        /// <summary>
        /// Create a new minute to minute schedule for an event to occur. 
        /// </summary>
        /// <param name="days"></param>
        /// <param name="st"></param>
        /// <param name="et"></param>
        /// <param name="interval"></param>
        public M2MSchedule(string[] days, TimeOnly st, TimeOnly et, int interval)
        {
            Days = days;
            StartTime = st;
            EndTime = et;
            Interval = interval;
        }
        /// <summary>
        /// Set the task for the specified M2M schedule
        /// </summary>
        /// <param name="schedule"></param>
        public static void CreateTask(M2MSchedule schedule, Delegate func)
        {
            while (true)
            {
                TimeSpan timeToWait;
                // Check if the current day of the week is a scheduled day:
                if (ScheduleChecks.ScheduledDayCheck(schedule))
                {
                    // Check if it is within scheduled hours:
                    if(ScheduleChecks.ScheduledHoursCheck(schedule))
                    {
                        // Check if it is the beginning of a minute
                        if (ScheduleChecks.OnTheMinuteCheck())
                        {
                            // This is where the program will be executed. 
                            func.DynamicInvoke();
                            timeToWait = TimeCalculations.MsTillNextInterval(schedule);
                            Console.WriteLine("Waiting " + Math.Round(timeToWait.TotalSeconds, 2) + " seconds until next runtime");
                        }
                        // Calculate how many milliseconds until the next scheduled minute
                        else { timeToWait = TimeCalculations.MsTillNextInterval(schedule);
                            Console.WriteLine("Waiting " + Math.Round(timeToWait.TotalSeconds, 2) + " seconds until next runtime");
                        }
                    }
                    // Calculate how many milliseconds until the next scheduled hour window
                    else { timeToWait = TimeCalculations.MsTillNextScheduledHour(schedule);
                        Console.WriteLine("Waiting " + Math.Round(timeToWait.TotalHours, 2) + " hours until next runtime");
                    }
                }
                // Calculate how many milliseconds until the next scheduled day and start time
                else { timeToWait = TimeCalculations.MsTillNextScheduledDay(schedule);
                    Console.WriteLine("Waiting " + Math.Round(timeToWait.TotalDays, 2) + " days until next runtime");
                }
                
                // Will pause the while loop for amount of milliseconds specified, so that it isn't constantly cycling through the while loop
                new ManualResetEvent(false).WaitOne(Convert.ToInt32(timeToWait.TotalMilliseconds));

                #region OLD CODE
                //if (currentTime >= schedule.StartTime && currentTime <= schedule.EndTime)
                //{
                //    // Every minute on the minute: 
                //    if (currentTime.Second == 0)
                //    {
                //        // Task to be completed here <------
                //        timeToWait = TimeCalculations.MsTillNextInterval(currentTime, schedule.Interval);
                //        Console.WriteLine("Waiting for " + timeToWait + " milliseconds before next task will be ran...");
                //    }
                //    // if it is not the beginning of a minute:
                //    else
                //    {
                //        timeToWait = TimeCalculations.MsTillNextInterval(currentTime, schedule.Interval);
                //        Console.WriteLine("Waiting for " + timeToWait + " milliseconds before next task will be ran...");
                //    }
                //}
                //// if it is not within the scheduled hours to run: 
                //else
                //{
                //    timeToWait = TimeCalculations.MsTillNextStart(currentTime, schedule.StartTime);
                //    Console.WriteLine("Waiting for " + timeToWait + " milliseconds before next task will be ran...");
                //}

                //// Will pause the while loop for amount of milliseconds specified, so that it isn't constantly cycling through the while loop
                //new ManualResetEvent(false).WaitOne(timeToWait);
                //Console.WriteLine("While loop completed a cycle.");
                #endregion
            }
        }
    }
}

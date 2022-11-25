﻿using ConsoleScheduler;

// First, create a function that you would like to be ran:
static void TaskToSchedule()
{
    // Inset code here to be carried out for the schedule
    Console.WriteLine("Task just completed @ " + TimeOnly.FromDateTime(DateTime.Now));
}

// Next, create a schedule for the function: 

M2MSchedule schedule = new();

schedule.Days = new string[] {"Mon","Tue","Wed","Thu","Fri"};
schedule.StartTime = new(08, 30, 00);
schedule.EndTime = new(17, 00, 00);
schedule.Interval = 1;

// Set the schedule

M2MSchedule.CreateTask(schedule, TaskToSchedule);

#region PROTOTYPE CODE


//static int msTillNextMinute(TimeOnly now)
//{
//    int minute = now.Minute;
//    int hour = now.Hour;

//    TimeOnly nowPlusOneMin = new(hour, minute+1, 00);

//    TimeSpan difference = nowPlusOneMin - now;
//    return Convert.ToInt32(difference.TotalMilliseconds);
//}
//static int msTillNextStartTime(TimeOnly now, TimeOnly start)
//{
//    TimeSpan difference = now - start;
//    return Convert.ToInt32(difference.TotalMilliseconds);
//}

//// This currently runs forever, as there is no boolean condition evaluated to false to break the while loop (which is expected behavior)
//while (true)
//{
//    int timeToWait;
//    TimeOnly timeToStart = new(08,30,00);
//    TimeOnly timeToStop = new(15,30,00);
//    TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);

//    if (currentTime >= timeToStart && currentTime <= timeToStop)
//    {
//        // Every minute on the minute: 
//        if (currentTime.Second == 0)
//        {
//            TestTask();
//            timeToWait = msTillNextMinute(currentTime);
//            Console.WriteLine("Waiting for " + timeToWait + " milliseconds before next task will be ran...");
//        }
//        // if it is not the beginning of a minute:
//        else
//        {   
//            timeToWait = msTillNextMinute(currentTime);
//            Console.WriteLine("Waiting for " + timeToWait + " milliseconds before next task will be ran...");
//        }
//    }
//    // if it is not within the scheduled hours to run: 
//    else
//    {
//        timeToWait = msTillNextStartTime(currentTime, timeToStart);
//        Console.WriteLine("Waiting for " + timeToWait + " milliseconds before next task will be ran...");
//    }

//    // Will pause the while loop for amount of milliseconds specified, so that it isn't constantly cycling through the while loop
//    new ManualResetEvent(false).WaitOne(timeToWait);
//    Console.WriteLine("While loop completed a cycle.");

//}

#endregion
using Cronos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftBuilder
{

    public class WorkingShiftOperator
    {
        public TimeKeeping Calculate(IEnumerable<WorkingShift> workingShifts, IEnumerable<WorkingDate> workingDates, ShiftOption? shiftOption = null)
        {
            TimeKeeping total = new();

            foreach (var workingDate in workingDates)
            {
                total += Calculate(workingShifts, workingDate, shiftOption);
            }

            return total;
        }
        public TimeKeeping Calculate(IEnumerable<WorkingShift> workingShifts, WorkingDate workingDate, ShiftOption? shiftOption = null)
        {
            TimeKeeping timeKeeping = new();

            DateTime? earliestShiftStartTime;
            DateTime? latestShiftEndTime;
            IEnumerable<WorkingShift> workingShiftsInDate = FilterWorkingShiftByDate(workingShifts, workingDate, out earliestShiftStartTime, out latestShiftEndTime);

            foreach (WorkingShift workingShiftInDate in workingShiftsInDate)
            {
                if (workingShiftInDate.WorkingStartTime is null)
                {
                    continue;
                }


                if (workingShiftInDate.WorkingStartTime > workingShiftInDate.StartTime)
                {
                    // work late
                    if (earliestShiftStartTime.HasValue && earliestShiftStartTime == workingShiftInDate.StartTime)
                    {
                        timeKeeping.WorkLate = workingShiftInDate.WorkingStartTime.Value - workingShiftInDate.StartTime;
                    }
                }
                else if (workingShiftInDate.WorkingStartTime == workingShiftInDate.StartTime)
                {
                    // work early or on time
                    if (earliestShiftStartTime.HasValue && earliestShiftStartTime == workingShiftInDate.StartTime)
                    {
                        timeKeeping.WorkEarly = workingShiftInDate.StartTime - workingDate.CheckIn;
                    }
                }


                if (workingDate.CheckOut < workingShiftInDate.EndTime)
                {
                    // leave early
                    if (latestShiftEndTime.HasValue && latestShiftEndTime == workingShiftInDate.EndTime)
                    {
                        timeKeeping.LeaveEarly = workingShiftInDate.EndTime - workingDate.CheckOut;
                    }

                    timeKeeping.WorkingTime += workingDate.CheckOut - workingShiftInDate.WorkingStartTime!.Value;
                }
                else
                {
                    // leave late or on time
                    if (latestShiftEndTime.HasValue && latestShiftEndTime == workingShiftInDate.EndTime)
                    {
                        timeKeeping.LeaveLate = workingDate.CheckOut - workingShiftInDate.EndTime;
                    }

                    timeKeeping.WorkingTime += workingShiftInDate.EndTime - workingShiftInDate.WorkingStartTime!.Value;
                }
            }


            if (shiftOption != null)
            {
                if (timeKeeping.WorkLate.TotalSeconds > 0 && shiftOption.MaxWorkLateSeconds >= timeKeeping.WorkLate.TotalSeconds)
                {
                    timeKeeping.WorkingTime = timeKeeping.WorkingTime.Subtract(TimeSpan.FromSeconds(timeKeeping.WorkLate.TotalSeconds));
                    timeKeeping.WorkLate = new TimeSpan(0);
                }

                if (timeKeeping.LeaveEarly.TotalSeconds > 0 && shiftOption.MaxLeaveEarlySeconds >= timeKeeping.WorkLate.TotalSeconds)
                {
                    timeKeeping.WorkingTime = timeKeeping.WorkingTime.Subtract(TimeSpan.FromSeconds(timeKeeping.LeaveEarly.TotalSeconds));
                    timeKeeping.LeaveEarly = new TimeSpan(0);
                }
            }

            return timeKeeping;
        }

        /// <summary>
        /// Filter working shift by date.
        /// </summary>
        /// <param name="workingShifts"></param>
        /// <param name="workingDate"></param>
        /// <param name="earliestShiftStartTime"></param>
        /// <param name="latestShiftEndTime"></param>
        /// <returns></returns>
        public IEnumerable<WorkingShift> FilterWorkingShiftByDate(IEnumerable<WorkingShift> workingShifts, WorkingDate workingDate, out DateTime? earliestShiftStartTime, out DateTime? latestShiftEndTime)
        {
            List<WorkingShift> list = [];
            earliestShiftStartTime = null;
            latestShiftEndTime = null;

            var shiftStartTimeInOrder = workingShifts.OrderBy(ws => ws.StartTime).ToArray();


            for (int i = 0; i < shiftStartTimeInOrder.Length; i++)
            {
                var workingShift = shiftStartTimeInOrder[i];

                workingShift.StartTime = SetTimeToDate(workingShift.StartTime, workingDate.CheckIn);
                workingShift.EndTime = SetTimeToDate(workingShift.EndTime, workingDate.CheckIn);

                var expressions = GenerateShiftCronExpressionBy(workingShift.StartTime, workingShift.EndTime, workingShift.Expression);
                // Checking that date with the shift. If ocurrence is not null, it means valid shift for calculation.
                var occurrence = GetShiftOccurrence(expressions, workingDate.CheckIn, workingDate.CheckOut);
                if (occurrence == null)
                {
                    continue;
                }

                // Correct shift start time if it conflicts previous shift.
                if (i >= 1 && i + 1 < shiftStartTimeInOrder.Length)
                {
                    var previousShift = shiftStartTimeInOrder[i + 1];
                    if (workingShift.StartTime > previousShift.StartTime && workingShift.StartTime < previousShift.EndTime)
                    {
                        workingShift.StartTime = previousShift.EndTime;
                        if (workingShift.EndTime <= workingShift.StartTime)
                        {
                            continue;
                        }

                        expressions = GenerateShiftCronExpressionBy(workingShift.StartTime, workingShift.EndTime, workingShift.Expression);
                        occurrence = GetShiftOccurrence(expressions, workingDate.CheckIn, workingDate.CheckOut);
                        if (occurrence == null)
                        {
                            continue;
                        }
                    }
                }

                workingShift.WorkingStartTime = occurrence;

                if (earliestShiftStartTime == null || earliestShiftStartTime > workingShift.StartTime)
                {
                    earliestShiftStartTime = workingShift.StartTime;
                }

                if (latestShiftEndTime == null || latestShiftEndTime < workingShift.EndTime)
                {
                    latestShiftEndTime = workingShift.EndTime;
                }

                list.Add(workingShift);
            }

            return list;
        }

        public DateTime SetTimeToDate(DateTime time, DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }

        public DateTime SetTimeToDate(TimeSpan time, DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }

        public DateTime? GetShiftOccurrence(IEnumerable<string> shiftExpressions, DateTime checkIn, DateTime checkOut)
        {
            //IEnumerable<DateTime> shiftOccurrences = [];
            DateTime? shiftOccurrence = null;
            foreach (var expression in shiftExpressions)
            {
                shiftOccurrence = CronExpression.Parse(expression).GetNextOccurrence(checkIn, true);
                if (shiftOccurrence != null && shiftOccurrence <= checkOut)
                {
                    break;
                }

                shiftOccurrence = null;
            }

            return shiftOccurrence;
        }

        public IEnumerable<DateTime> GetShiftOccurrences(IEnumerable<string> shiftExpressions, DateTime checkIn, DateTime checkOut)
        {
            IEnumerable<DateTime> shiftOccurrences = [];
            foreach (var expression in shiftExpressions)
            {
                shiftOccurrences = CronExpression.Parse(expression).GetOccurrences(checkIn, checkOut, true, true);
                if (shiftOccurrences.Any())
                {
                    break;
                }
            }
            return shiftOccurrences;
        }
        public IEnumerable<string> GenerateShiftCronExpressionBy(DateTime shiftStartTime, DateTime shiftEndTime, string expression)
        {
            // ERROR. End time cannot lower than start time. EX: Start time 10h10 > End time 9h
            if (shiftEndTime < shiftStartTime)
            {
                return [];
            }

            var expressions = new List<string>();


            var deviatedHours = shiftEndTime.Hour - shiftStartTime.Hour;
            if (deviatedHours == 0)
            {
                // 9h - 9h30
                // 9h10 - 9h50
                expressions.Add(SetTimeExpression(expression, $"{shiftStartTime.Minute}-{shiftEndTime.Minute}", shiftStartTime.Hour.ToString()));
            }
            else
            {
                // 10m-59m
                var minute = $"{shiftStartTime.Minute}-59";
                expressions.Add(SetTimeExpression(expression, minute, shiftStartTime.Hour.ToString()));

                if (deviatedHours >= 2)
                {
                    minute = "*";
                    // 1h or 1h-2h
                    var hour = deviatedHours == 2 ? $"{shiftStartTime.Hour + 1}" : $"{shiftStartTime.Hour + 1}-{shiftEndTime.Hour - 1}";
                    expressions.Add(SetTimeExpression(expression, minute, hour));
                }
                // 0m or 0m-39m 
                minute = shiftEndTime.Minute == 0 ? "0" : $"0-{shiftEndTime.Minute}";
                expressions.Add(SetTimeExpression(expression, minute, shiftEndTime.Hour.ToString()));
            }
            // 9h - 9h30 = 0-30 9 * * 1,2,3,4,5
            // 9h10 - 9h50 = 10-50 9 * * 1,2,3,4,5
            // 9h10 - 10h = 10-59 9 * * 1,2,3,4,5   +   0 10 * * 1,2,3,4,5
            // 9h10 - 10h30 = 10-59 9 * * 1,2,3,4,5   +   0-30 10 * * 1,2,3,4,5 
            // 9h10 - 10h40 = 10-59 9 * * 1,2,3,4,5   +   0-40 10 * * 1,2,3,4,5 
            // 9h10 - 11h0 = 10-59 9 * * 1,2,3,4,5   +   * 10 * * 1,2,3,4,5   +   0 11 * * 1,2,3,4,5
            // 9h10 - 11h10 = 10-59 9 * * 1,2,3,4,5   +   * 10 * * 1,2,3,4,5   +   0-10 11 * * 1,2,3,4,5
            return expressions;
        }
        public string SetTimeExpression(string repeat, string minute, string hour)
        {
            var parts = repeat.Split(' ');
            parts[0] = minute;
            parts[1] = hour;
            return string.Join(" ", parts);
        }

    }


    public class TimeKeeping
    {
        public TimeSpan WorkingTime { get; set; } = new TimeSpan(0);
        public TimeSpan WorkEarly { get; set; } = new TimeSpan(0);
        public TimeSpan WorkLate { get; set; } = new TimeSpan(0);
        public TimeSpan LeaveEarly { get; set; } = new TimeSpan(0);
        public TimeSpan LeaveLate { get; set; } = new TimeSpan(0);

        public static TimeKeeping operator +(TimeKeeping timeKeeping1, TimeKeeping timeKeeping2)
        {
            TimeKeeping total = new()
            {
                WorkEarly = timeKeeping1.WorkEarly + timeKeeping2.WorkEarly,
                WorkLate = timeKeeping1.WorkLate + timeKeeping2.WorkLate,
                WorkingTime = timeKeeping1.WorkingTime + timeKeeping2.WorkingTime,
                LeaveEarly = timeKeeping1.LeaveEarly + timeKeeping2.LeaveEarly,
                LeaveLate = timeKeeping1.LeaveLate + timeKeeping2.LeaveLate
            };

            return total;
        }

    }

    public class WorkingDate
    {
        public WorkingDate()
        {
            
        }

        public WorkingDate(DateTime checkIn, DateTime checkOut)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }

    public class WorkingShift
    {
        public WorkingShift(string expression, DateTime startTime, DateTime endTime)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Expression = expression;
            StartTime = startTime;
            EndTime = endTime;
        }

        public WorkingShift(string expression, DateTime startTime, DateTime endTime, ShiftOption? options)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Expression = expression;
            StartTime = startTime;
            EndTime = endTime;
            Options = options;
        }

        public WorkingShift(string expression, DateTime startTime, DateTime endTime, DateTime? workingStartTime, ShiftOption? options)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }

            Expression = expression;
            StartTime = startTime;
            EndTime = endTime;
            WorkingStartTime = workingStartTime;
            Options = options;
        }


        public string Expression { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public ShiftOption? Options { get; set; } = null;

        public DateTime? WorkingStartTime { get; set; } = null;

    }

    public class ShiftOption
    {
        public int MaxWorkLateSeconds { get; set; } = 0; // seconds
        public int MaxLeaveEarlySeconds { get; set; } = 0; // seconds;

    }


}

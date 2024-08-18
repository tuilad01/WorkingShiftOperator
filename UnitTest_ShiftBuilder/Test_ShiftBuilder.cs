using ShiftBuilder;

namespace UnitTest_ShiftBuilder
{
    [TestClass]
    public class Test_ShiftBuilder
    {
        [TestMethod]
        public void Test_8h_8h50()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 0, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 8, 50, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");

            Assert.IsTrue(expressions.Count() == 1);
            Assert.IsTrue(expressions.ElementAt(0) == $"0-50 {shiftStart.Hour} * * 1,2,3,4,5");
        }

        [TestMethod]
        public void Test_8h10_8h50()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 10, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 8, 50, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            Assert.IsTrue(expressions.Count() == 1);
            Assert.IsTrue(expressions.ElementAt(0) == $"10-50 {shiftStart.Hour} * * 1,2,3,4,5");
        }

        [TestMethod]
        public void Test_8h_9h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 0, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 9, 0, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            Assert.IsTrue(expressions.Count() == 2);
            Assert.IsTrue(expressions.ElementAt(0) == $"0-59 {shiftStart.Hour} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(1) == $"0 {shiftEnd.Hour} * * 1,2,3,4,5");
        }

        [TestMethod]
        public void Test_8h10_9h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 10, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 9, 0, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            Assert.IsTrue(expressions.Count() == 2);
            Assert.IsTrue(expressions.ElementAt(0) == $"10-59 {shiftStart.Hour} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(1) == $"0 {shiftEnd.Hour} * * 1,2,3,4,5");
        }

        [TestMethod]
        public void Test_8h10_9h15()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 10, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 9, 15, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            Assert.IsTrue(expressions.Count() == 2);
            Assert.IsTrue(expressions.ElementAt(0) == $"10-59 {shiftStart.Hour} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(1) == $"0-15 {shiftEnd.Hour} * * 1,2,3,4,5");
        }

        [TestMethod]
        public void Test_8h_10h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 0, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 10, 0, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            Assert.IsTrue(expressions.Count() == 3);
            Assert.IsTrue(expressions.ElementAt(0) == $"0-59 {shiftStart.Hour} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(1) == $"* {shiftEnd.Hour - 1} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(2) == $"0 {shiftEnd.Hour} * * 1,2,3,4,5");
        }

        [TestMethod]
        public void Test_8h10_10h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 10, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 10, 0, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            Assert.IsTrue(expressions.Count() == 3);
            Assert.IsTrue(expressions.ElementAt(0) == $"10-59 {shiftStart.Hour} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(1) == $"* {shiftEnd.Hour - 1} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(2) == $"0 {shiftEnd.Hour} * * 1,2,3,4,5");
        }

        [TestMethod]
        public void Test_8h10_10h30()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 10, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 10, 30, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            Assert.IsTrue(expressions.Count() == 3);
            Assert.IsTrue(expressions.ElementAt(0) == $"10-59 {shiftStart.Hour} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(1) == $"* {shiftEnd.Hour - 1} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(2) == $"0-30 {shiftEnd.Hour} * * 1,2,3,4,5");
        }

        [TestMethod]
        public void Test_8h10_11h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 10, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 11, 0, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            Assert.IsTrue(expressions.Count() == 3);
            Assert.IsTrue(expressions.ElementAt(0) == $"10-59 {shiftStart.Hour} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(1) == $"* {shiftStart.Hour + 1}-{shiftEnd.Hour - 1} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(2) == $"0 {shiftEnd.Hour} * * 1,2,3,4,5");
        }
        [TestMethod]
        public void Test_8h10_11h35()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 10, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 11, 35, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            Assert.IsTrue(expressions.Count() == 3);
            Assert.IsTrue(expressions.ElementAt(0) == $"10-59 {shiftStart.Hour} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(1) == $"* {shiftStart.Hour + 1}-{shiftEnd.Hour - 1} * * 1,2,3,4,5");
            Assert.IsTrue(expressions.ElementAt(2) == $"0-35 {shiftEnd.Hour} * * 1,2,3,4,5");
        }

        [TestMethod]
        public void Test_WorkEarly()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 30, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 11, 45, 0).ToUniversalTime();

            var checkIn = new DateTime(2024, 8, 12, 7, 10, 0).ToUniversalTime();
            var checkOut = new DateTime(2024, 8, 12, 17, 30, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            var occurrences = workingShiftOperator.GetShiftOccurrences(expressions, checkIn, checkOut);

            Assert.IsTrue(occurrences.ElementAt(0) == shiftStart);
        }

        [TestMethod]
        public void Test_WorkLate()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 30, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 11, 45, 0).ToUniversalTime();

            var checkIn = new DateTime(2024, 8, 12, 8, 50, 0).ToUniversalTime();
            var checkOut = new DateTime(2024, 8, 12, 17, 30, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            var occurrences = workingShiftOperator.GetShiftOccurrences(expressions, checkIn, checkOut);

            Assert.IsTrue(occurrences.ElementAt(0) > shiftStart);
        }

        [TestMethod]
        public void Test_FindShiftOccurrence_8h30()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 30, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 11, 45, 0).ToUniversalTime();

            var checkIn = new DateTime(2024, 8, 12, 8, 20, 0).ToUniversalTime();
            var checkOut = new DateTime(2024, 8, 12, 17, 30, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            var occurrence = workingShiftOperator.GetShiftOccurrence(expressions, checkIn, checkOut);

            Assert.IsTrue(occurrence != null && occurrence == shiftStart);
        }

        [TestMethod]
        public void Test_FindShiftOccurrence_9h35()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 30, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 11, 45, 0).ToUniversalTime();

            var checkIn = new DateTime(2024, 8, 12, 9, 35, 0).ToUniversalTime();
            var checkOut = new DateTime(2024, 8, 12, 17, 30, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            var occurrence = workingShiftOperator.GetShiftOccurrence(expressions, checkIn, checkOut);

            Assert.IsTrue(occurrence != null && occurrence == checkIn);
        }

        [TestMethod]
        public void Test_FindShiftOccurrence_11h45()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 30, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 11, 45, 0).ToUniversalTime();

            var checkIn = new DateTime(2024, 8, 12, 11, 45, 0).ToUniversalTime();
            var checkOut = new DateTime(2024, 8, 12, 17, 30, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            var occurrence = workingShiftOperator.GetShiftOccurrence(expressions, checkIn, checkOut);

            Assert.IsTrue(occurrence != null && occurrence == shiftEnd);
        }

        [TestMethod]
        public void Test_FindShiftOccurrence_OutOfShift()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var shiftStart = new DateTime(2024, 8, 12, 8, 30, 0).ToUniversalTime();
            var shiftEnd = new DateTime(2024, 8, 12, 11, 45, 0).ToUniversalTime();

            var checkIn = new DateTime(2024, 8, 12, 15, 45, 0).ToUniversalTime();
            var checkOut = new DateTime(2024, 8, 12, 17, 30, 0).ToUniversalTime();

            IEnumerable<string> expressions = workingShiftOperator.GenerateShiftCronExpressionBy(shiftStart, shiftEnd, "* * * * 1,2,3,4,5");
            var occurrence = workingShiftOperator.GetShiftOccurrence(expressions, checkIn, checkOut);

            Assert.IsTrue(occurrence == null);
        }

        [TestMethod]
        public void Test_Calculate_1day_WorkingTime_7h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var now = DateTime.Now;


            WorkingShift[] workingShifts = [
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(9, 0, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(11, 30, 0), now).ToUniversalTime()),
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(13, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                ];
            WorkingDate[] workingDates = [
                new WorkingDate(new DateTime(2024, 8,12,8,10,5).ToUniversalTime(), new DateTime(2024, 8,12,18,10,15).ToUniversalTime())
                ];

            var timeKeeping = workingShiftOperator.Calculate(workingShifts, workingDates);


            Assert.IsTrue(timeKeeping.WorkingTime == new TimeSpan(7,0,0));
            Assert.IsTrue(timeKeeping.WorkEarly == new TimeSpan(0, 49, 55));
            Assert.IsTrue(timeKeeping.WorkLate == new TimeSpan(0));
            Assert.IsTrue(timeKeeping.LeaveEarly == new TimeSpan(0));
            Assert.IsTrue(timeKeeping.LeaveLate == new TimeSpan(0, 10, 15));
        }

        [TestMethod]
        public void Test_Calculate_2day_WorkingTime_14h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var now = DateTime.Now;


            WorkingShift[] workingShifts = [
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(9, 0, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(11, 30, 0), now).ToUniversalTime()),
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(13, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                ];
            WorkingDate[] workingDates = [
                new WorkingDate(new DateTime(2024, 8,12,8,10,5).ToUniversalTime(), new DateTime(2024, 8,12,18,10,15).ToUniversalTime()),
                new WorkingDate(new DateTime(2024, 8,13,8,10,5).ToUniversalTime(), new DateTime(2024, 8,13,18,10,15).ToUniversalTime())
                ];

            var timeKeeping = workingShiftOperator.Calculate(workingShifts, workingDates);


            Assert.IsTrue(timeKeeping.WorkingTime == new TimeSpan(14, 0, 0));
            //Assert.IsTrue(timeKeeping.WorkEarly == new TimeSpan(0, 49, 55));
            //Assert.IsTrue(timeKeeping.WorkLate == new TimeSpan(0));
            //Assert.IsTrue(timeKeeping.LeaveEarly == new TimeSpan(0));
            //Assert.IsTrue(timeKeeping.LeaveLate == new TimeSpan(0, 10, 15));
        }

        [TestMethod]
        public void Test_Calculate_2day_SundayShift_WorkingTime_14h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var now = DateTime.Now;


            WorkingShift[] workingShifts = [
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(9, 0, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(11, 30, 0), now).ToUniversalTime()),
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(13, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                new WorkingShift("* * * * 0",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(8, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                ];

            WorkingDate[] workingDates = [
                new WorkingDate(new DateTime(2024, 8,12,8,10,5).ToUniversalTime(), new DateTime(2024, 8,12,18,10,15).ToUniversalTime()),
                new WorkingDate(new DateTime(2024, 8,13,8,10,5).ToUniversalTime(), new DateTime(2024, 8,13,18,10,15).ToUniversalTime())
                ];

            var timeKeeping = workingShiftOperator.Calculate(workingShifts, workingDates);


            Assert.IsTrue(timeKeeping.WorkingTime == new TimeSpan(14, 0, 0));
            //Assert.IsTrue(timeKeeping.WorkEarly == new TimeSpan(0, 49, 55));
            //Assert.IsTrue(timeKeeping.WorkLate == new TimeSpan(0));
            //Assert.IsTrue(timeKeeping.LeaveEarly == new TimeSpan(0));
            //Assert.IsTrue(timeKeeping.LeaveLate == new TimeSpan(0, 10, 15));
        }

        [TestMethod]
        public void Test_Calculate_Late1Hour__WorkingTime_13h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var now = DateTime.Now;


            WorkingShift[] workingShifts = [
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(9, 0, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(11, 30, 0), now).ToUniversalTime()),
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(13, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                new WorkingShift("* * * * 0",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(8, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                ];

            WorkingDate[] workingDates = [
                new WorkingDate(new DateTime(2024, 8,12,10,0,0).ToUniversalTime(), new DateTime(2024, 8,12,18,10,15).ToUniversalTime()),
                new WorkingDate(new DateTime(2024, 8,13,8,10,5).ToUniversalTime(), new DateTime(2024, 8,13,18,10,15).ToUniversalTime())
                ];

            var timeKeeping = workingShiftOperator.Calculate(workingShifts, workingDates);


            Assert.IsTrue(timeKeeping.WorkingTime == new TimeSpan(13, 0, 0));
            
            //Assert.IsTrue(timeKeeping.WorkEarly == new TimeSpan(0, 49, 55));
            Assert.IsTrue(timeKeeping.WorkLate == new TimeSpan(1,0,0));
            //Assert.IsTrue(timeKeeping.LeaveEarly == new TimeSpan(0));
            //Assert.IsTrue(timeKeeping.LeaveLate == new TimeSpan(0, 10, 15));
        }

        [TestMethod]
        public void Test_Calculate_Late3Hour_For2Days__WorkingTime_11h()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var now = DateTime.Now;


            WorkingShift[] workingShifts = [
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(9, 0, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(11, 30, 0), now).ToUniversalTime()),
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(13, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                new WorkingShift("* * * * 0",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(8, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                ];

            WorkingDate[] workingDates = [
                new WorkingDate(new DateTime(2024, 8,12,10,30,0).ToUniversalTime(), new DateTime(2024, 8,12,18,10,15).ToUniversalTime()),
                new WorkingDate(new DateTime(2024, 8,13,10,30,0).ToUniversalTime(), new DateTime(2024, 8,13,18,10,15).ToUniversalTime())
                ];

            var timeKeeping = workingShiftOperator.Calculate(workingShifts, workingDates);


            Assert.IsTrue(timeKeeping.WorkingTime == new TimeSpan(11, 0, 0));

            //Assert.IsTrue(timeKeeping.WorkEarly == new TimeSpan(0, 49, 55));
            Assert.IsTrue(timeKeeping.WorkLate == new TimeSpan(3, 0, 0));
            //Assert.IsTrue(timeKeeping.LeaveEarly == new TimeSpan(0));
            //Assert.IsTrue(timeKeeping.LeaveLate == new TimeSpan(0, 10, 15));
        }

        [TestMethod]
        public void Test_Calculate_WorkLate_For3Days_AllowWorkLate30Minutes_AllowLeaveEarly30Minutes()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var now = DateTime.Now;


            WorkingShift[] workingShifts = [
                // Monday to friday 9h00 - 11h30
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(9, 0, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(11, 30, 0), now).ToUniversalTime()),
                // Monday to Friday 13h30 - 18h00
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(13, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                // Sunday only 8h30 - 18h00
                //new WorkingShift("* * * * 0",
                //    workingShiftOperator.SetTimeToDate(new TimeSpan(8, 30, 0), now).ToUniversalTime(),
                //    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                ];

            WorkingDate[] workingDates = [
                // 8/12/2024 9h30 - 18h10 15 seconds
                new WorkingDate(new DateTime(2024, 8,12,9,30,0).ToUniversalTime(), new DateTime(2024, 8,12,18,10,15).ToUniversalTime()),
                // 8/13/2024 9h31 - 18h10 15 seconds
                new WorkingDate(new DateTime(2024, 8,13,9,31,0).ToUniversalTime(), new DateTime(2024, 8,13,18,10,15).ToUniversalTime()),
                // 8/12/2024 10h30 - 18h10 15 seconds
                new WorkingDate(new DateTime(2024, 8,14,10,30,0).ToUniversalTime(), new DateTime(2024, 8,14,18,10,15).ToUniversalTime())
                ];

            var timeKeeping = workingShiftOperator.Calculate(workingShifts, workingDates, new ShiftOption() { MaxLeaveEarlySeconds = 30 * 60, MaxWorkLateSeconds = 30 * 60});

            // 6:00 + 6:29 + 5:30 = 17:59
            Assert.IsTrue(timeKeeping.WorkingTime == new TimeSpan(17, 59, 0));

            // 0 + 31 + 1:30 = 2:01
            Assert.IsTrue(timeKeeping.WorkLate == new TimeSpan(2, 1, 0));

            // 0
            Assert.IsTrue(timeKeeping.LeaveEarly == new TimeSpan(0));

            // 0
            Assert.IsTrue(timeKeeping.WorkEarly == new TimeSpan(0));

            // 10m15s + 10m15s + 10m15s = 30m45 s
            Assert.IsTrue(timeKeeping.LeaveLate == new TimeSpan(0, 30, 45));
        }

        [TestMethod]
        public void Test_Calculate_WorkLate_LeaveEarly_AllowWorkLate30Minutes_AllowLeaveEarly30Minutes()
        {
            var workingShiftOperator = new WorkingShiftOperator();
            var now = DateTime.Now;


            WorkingShift[] workingShifts = [
                // Monday to friday 9h00 - 11h30
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(9, 0, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(11, 30, 0), now).ToUniversalTime()),
                // Monday to Friday 13h30 - 18h00
                new WorkingShift("* * * * 1,2,3,4,5,6",
                    workingShiftOperator.SetTimeToDate(new TimeSpan(13, 30, 0), now).ToUniversalTime(),
                    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                // Sunday only 8h30 - 18h00
                //new WorkingShift("* * * * 0",
                //    workingShiftOperator.SetTimeToDate(new TimeSpan(8, 30, 0), now).ToUniversalTime(),
                //    workingShiftOperator.SetTimeToDate(new TimeSpan(18, 00, 0), now).ToUniversalTime()),
                ];

            WorkingDate[] workingDates = [
                // 8/12/2024 9h30 - 18h10 15 seconds
                new WorkingDate(new DateTime(2024, 8,12,9,30,0).ToUniversalTime(), new DateTime(2024, 8,12,17,30,00).ToUniversalTime()),
                // 8/13/2024 9h31 - 18h10 15 seconds
                new WorkingDate(new DateTime(2024, 8,13,9,31,0).ToUniversalTime(), new DateTime(2024, 8,13,17,20,00).ToUniversalTime()),
                // 8/12/2024 10h30 - 18h10 15 seconds
                new WorkingDate(new DateTime(2024, 8,14,10,30,0).ToUniversalTime(), new DateTime(2024, 8,14,18,10,15).ToUniversalTime())
                ];

            var timeKeeping = workingShiftOperator.Calculate(workingShifts, workingDates, new ShiftOption() { MaxLeaveEarlySeconds = 30 * 60, MaxWorkLateSeconds = 30 * 60 });

            // 5:00 + (7:00 - 1:11) = 5:49 + 5:30 = 16:19
            Assert.IsTrue(timeKeeping.WorkingTime == new TimeSpan(16, 19, 0));

            // 0 + 31 + 1:30 = 2:01
            Assert.IsTrue(timeKeeping.WorkLate == new TimeSpan(2, 1, 0));

            // 0 + 40 + 0
            Assert.IsTrue(timeKeeping.LeaveEarly == new TimeSpan(0,40,0));

            // 0
            Assert.IsTrue(timeKeeping.WorkEarly == new TimeSpan(0));

            // 0 + 0 + 10m15s
            Assert.IsTrue(timeKeeping.LeaveLate == new TimeSpan(0, 10, 15));
        }

    }
}
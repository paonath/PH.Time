using System;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace PH.Time.XUnitTest
{


    public class UnitTest1
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public UnitTest1(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Test1()
        {
            var t = new Time(9, 0);
            var e = Time.MaxValue;

            var min = Time.MinValue;
            var dt  = DateTime.MinValue.GetTime();
            var of  = DateTimeOffset.MinValue.GetTime();

           

            var format    = $"{t:G}";
            var format1    = $"{t:t}";
            var shortTime = t.GetShortTime();


            var ser = JsonConvert.SerializeObject(t);


            var x = t.ToString();

            var n = Time.MinValue;
            n.Hours   = 8;
            n.Minutes = 9;
            n.Seconds = 10;

            bool minOrEqual  = n <= Time.MaxValue;
            bool grathOrEqul = n >= new Time(8, 9, 10);
            bool greath      = n >= Time.MinValue;


            Exception nExc0 = null;
            try
            {
                Time tt = Time.MaxValue;
                tt.Hours = 41;
            }
            catch (Exception exception)
            {
                nExc0 = exception;
            }

            Exception nExc1 = null;
            try
            {
                Time tt = Time.MaxValue;
                tt.Minutes = 64;
            }
            catch (Exception exception)
            {
                nExc1 = exception;
            }

            Exception nExc2 = null;
            try
            {
                Time tt = Time.MaxValue;
                tt.Seconds = 64;
            }
            catch (Exception exception)
            {
                nExc2 = exception;
            }

            Assert.True(!string.IsNullOrEmpty(x));
            Assert.Equal(dt, min);
            Assert.Equal(of, min);
            Assert.Equal(0, min.Hours);
            Assert.Equal(0, min.Minutes);
            Assert.Equal(0, min.Seconds);

            Assert.Equal(0, dt.Hours);
            Assert.Equal(0, dt.Minutes);
            Assert.Equal(0, dt.Seconds);

            Assert.Equal(0, of.Hours);
            Assert.Equal(0, of.Minutes);
            Assert.Equal(0, of.Seconds);
            Assert.Equal("09:00:00", format);
            Assert.Equal("09:00", shortTime);
            Assert.Equal("09:00", format1);

            Assert.NotNull(nExc0);
            Assert.NotNull(nExc1);
            Assert.NotNull(nExc2);

            Assert.True(minOrEqual);
            Assert.True(grathOrEqul);
            Assert.True(greath);
        }

        [Fact]
        public void TestOrderBy()
        {
            var t0000 = Time.MinValue;
            var t0100 = new Time(1, 0);
            var t0923 = new Time(9, 23);
            var t1010 = new Time(10, 10);
            var t23   = Time.MaxValue;

            var shuffle = new Time[] {t1010, t0923, t0000, t23, t0100};

            var asc  = shuffle.OrderBy(x => x).ToArray();
            var desc = shuffle.OrderByDescending(x => x).ToArray();

            Assert.True(asc[0] == t0000);
            Assert.True(asc[1] == t0100);
            Assert.True(asc[2] == t0923);
            Assert.True(asc[3] == t1010);
            Assert.True(asc[4] == t23);

            Assert.True(desc[0] == t23);
            Assert.True(desc[1] == t1010);
            Assert.True(desc[2] == t0923);
            Assert.True(desc[3] == t0100);
            Assert.True(desc[4] == t0000);
        }

        [Fact]
        public void TestNextValues()
        {

            var t0 = Time.MinValue;
            var r0 = t0.NextSecond(out var onNextDay0);

            var t1 = new Time(9, 59, 59);
            var r1 = t1.NextSecond(out var onNextDay1);

            var t2 = Time.MaxValue;
            var r2 = t2.NextSecond(out var onNextDay2);

            var  byString0 = Time.Parse("09:00");
            var  byString1 = Time.Parse("10:10:10");
            bool t         = Time.TryParse("12:13:14", out Time trueTime);
            bool f         = Time.TryParse("44:13:14", out Time falseTime);

            object timeObj = new Time(9,0);

            bool eq0 = byString0.Equals(new Time(9, 0));
            bool eq1 = byString0.Equals(timeObj);
            bool nq  = byString0.Equals(new Time(10, 0));


            var p0 = Time.MinValue.Next(TimePart.Hours, out bool p0bool);
            var p1 = Time.MinValue.Next(TimePart.Minutes, out bool p1bool);
            var p2 = Time.MinValue.Next(TimePart.Seconds, out bool p2bool);


            Assert.Equal(Time.MinValue, t0);
            Assert.Equal(new Time(9, 59, 59), t1);
            Assert.Equal(Time.MaxValue, t2);


            Assert.False(onNextDay0);
            Assert.Equal(1, r0.Seconds);

            Assert.False(onNextDay1);
            Assert.Equal(10, r1.Hours);
            Assert.Equal(0, r1.Minutes);
            Assert.Equal(0, r1.Seconds);


            Assert.True(onNextDay2);
            Assert.Equal(0, r2.Hours);
            Assert.Equal(0, r2.Minutes);
            Assert.Equal(0, r2.Seconds);

            Assert.Equal(new Time(9,0),byString0);
            Assert.Equal(new Time(10,10,10),byString1);

            Assert.True(t);
            Assert.Equal(new Time(12,13,14) , trueTime);
            Assert.False(f);
            Assert.Equal(Time.MinValue, falseTime);

            Assert.True(eq0);
            Assert.True(eq1);
            Assert.False(nq);

            Assert.False(p0bool);
            Assert.False(p1bool);
            Assert.False(p2bool);

            Assert.Equal(new Time(1,0,0), p0);
            Assert.Equal(new Time(0,1,0), p1);
            Assert.Equal(new Time(0,0,1), p2);


        }

        [Fact]
        public void TestPreviousValues()
        {
            var t0 = Time.MinValue;
            var r0 = t0.Previous(TimePart.Seconds, out bool prevDay0);

            var t1 = new Time(9,0,0);
            var r1 = t1.Previous(TimePart.Hours, out bool prevDay1);

            var t2 = new Time(9,0,0);
            var r2 = t2.Previous(TimePart.Minutes, out bool prevDay2);


            Assert.Equal(Time.MinValue, t0);
            Assert.Equal(new Time(9,0,0), t1);


            Assert.Equal(new Time(23,59,59), r0);
            Assert.True(prevDay0);
            
            Assert.Equal(new Time(8,0,0), r1);
            Assert.False(prevDay1);

            Assert.Equal(new Time(8,59,0), r2);
            Assert.False(prevDay2);


        }

        [Fact]
        public void TestTimeArray()
        {
            

            var arr0 = TimeFactory.BuildTimeArray(Time.MinValue, new Time(0, 2, 0), TimePart.Minutes,
                                                  includeExtremes: false);

            var arr1 = TimeFactory.BuildTimeArray(Time.MinValue, Time.MaxValue);

            var arr3 = TimeFactory.BuildTimeArray(Time.MinValue, Time.MaxValue, TimePart.Seconds,
                                                  includeExtremes: true);

            Exception myException = null;
            try
            {
                var arr4 = TimeFactory.BuildTimeArray(Time.MaxValue, Time.MinValue);
            }
            catch (Exception e)
            {
                myException = e;
            }

            var arr5 = TimeFactory.BuildTimeArray(Time.MinValue, Time.MinValue, TimePart.Seconds);
            var arr6 = TimeFactory.BuildTimeArray(Time.MinValue, Time.MinValue, TimePart.Seconds, false);

            Assert.True(arr0.Length == 1);
            Assert.Equal(new Time(0, 1, 0), arr0[0]);

            Assert.True(arr1.Length == 24);
            Assert.Equal(new Time(1, 0, 0), arr1[1]);
            Assert.Equal(new Time(23, 59, 59), arr1[23]);

            Assert.Equal(86400, arr3.Length);
            Assert.NotNull(myException);
            Assert.True(myException is ArgumentOutOfRangeException);

            Assert.Single(arr5);
            Assert.Empty(arr6);
        }

        [Fact]
        public void TestTimeArrayBySteps()
        {
            var arr0 = TimeFactory.BuildTimeArrayBySteps(Time.MinValue, Time.MaxValue, 30, TimePart.Minutes,
                                                         includeExtremes: true);

            var arr1 = TimeFactory.BuildTimeArrayBySteps(Time.MinValue, new Time(0, 45), 30, TimePart.Minutes,
                                                         includeExtremes: false);


            var arr2 = TimeFactory.BuildTimeArrayBySteps(Time.MinValue, Time.MaxValue, 1, TimePart.Hours);
            var fkk2 = TimeFactory.BuildTimeArray(Time.MinValue, Time.MaxValue);

            var arr3 = TimeFactory.BuildTimeArrayBySteps(Time.MinValue, new Time(0,0,10), 1, TimePart.Seconds);

            Assert.Equal(49, arr0.Length);
            Assert.Single(arr1);
            Assert.Equal(new Time(0, 30, 0), arr1[0]);
            Assert.Equal(24,arr2.Length);
            Assert.Equal(24,fkk2.Length);
            Assert.Equal(arr2,fkk2);
            Assert.Equal(11, arr3.Length);

        }

        [Fact]
        public void DocExample()
        {

            var time                   = Time.MinValue;
            var nine                   = new Time(9,0);
            var midNight               = new Time(0, 0, 0);

            var nineSmallerThanMidnith = nine < time;
            var zeroIsMidnight         = time == midNight;


            _testOutputHelper.WriteLine($"{nineSmallerThanMidnith}");
            _testOutputHelper.WriteLine($"{zeroIsMidnight}");



        }

        
    }
}
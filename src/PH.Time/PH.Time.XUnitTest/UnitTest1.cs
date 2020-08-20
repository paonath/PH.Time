using System;
using System.Linq;
using Xunit;

namespace PH.Time.XUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var t = new Time(9, 0);
            var e = Time.MaxValue;


            var x = t.ToString();

            Assert.True(!string.IsNullOrEmpty(x));

        }

        [Fact]
        public void TestOrderBy()
        {
            var t0000 = Time.MinValue;
            var t0100 = new Time(1,0);
            var t0923 = new Time(9,23);
            var t1010 = new Time(10,10);
            var t23   = Time.MaxValue;

            var shuffle = new Time[] {t1010 ,t0923 , t0000, t23 , t0100};

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

            var t1 = new Time(9,59,59);
            var r1 = t1.NextSecond(out var onNextDay1);

            var t2 = Time.MaxValue;
            var r2 = t2.NextSecond(out var onNextDay2);


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
                var arr4 = TimeFactory.BuildTimeArray(Time.MaxValue,Time.MinValue);
            }
            catch (Exception e)
            {
                myException = e;
            }

            var arr5 = TimeFactory.BuildTimeArray(Time.MinValue, Time.MinValue, TimePart.Seconds);
            var arr6 = TimeFactory.BuildTimeArray(Time.MinValue, Time.MinValue, TimePart.Seconds, false);

            Assert.True(arr0.Length == 1);
            Assert.Equal(new Time(0,1,0), arr0[0] );

            Assert.True(arr1.Length == 24);
            Assert.Equal(new Time(1,0,0), arr1[1] );
            Assert.Equal(new Time(23,59,59), arr1[23] );

            Assert.Equal(86400, arr3.Length);
            Assert.NotNull(myException);
            Assert.True(myException is ArgumentOutOfRangeException);

            Assert.Single(arr5);
            Assert.Empty(arr6);


        }
    }
}

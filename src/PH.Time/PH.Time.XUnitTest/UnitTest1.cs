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
    }
}

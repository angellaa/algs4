using System;
using System.Collections;
using System.Collections.Generic;

namespace Algs4Tests
{
    public class TimeTable : IComparable
    {
        public string City { get; set; }
        public DateTime Time { get; set; }

        public override bool Equals(object obj)
        {
            TimeTable that = (obj as TimeTable);
            if (that == null)
            {
                throw new Exception("Invalid object type. TimeTable is expected!");
            }

            return string.Equals(this.City, that.City) && DateTime.Equals(this.Time, that.Time);
        }


        public static IEnumerable<TimeTable> GetTestSample()
        {
            var list = new List<TimeTable>()
            {
                new TimeTable() { City = "Chicago", Time = new DateTime(2000, 1, 1, 09, 00, 00)},
                new TimeTable() { City = "Chicago", Time = new DateTime(2000, 1, 1, 09, 00, 59)},
                new TimeTable() { City = "Chicago", Time = new DateTime(2000, 1, 1, 09, 03, 13)},
                new TimeTable() { City = "Chicago", Time = new DateTime(2000, 1, 1, 09, 19, 32)},
                new TimeTable() { City = "Chicago", Time = new DateTime(2000, 1, 1, 09, 19, 46)},
                new TimeTable() { City = "Chicago", Time = new DateTime(2000, 1, 1, 09, 21, 05)},
                new TimeTable() { City = "Chicago", Time = new DateTime(2000, 1, 1, 09, 25, 52)},
                new TimeTable() { City = "Chicago", Time = new DateTime(2000, 1, 1, 09, 35, 21)},

                new TimeTable() { City = "Houston", Time = new DateTime(2000, 1, 1, 09, 00, 13)},
                new TimeTable() { City = "Houston", Time = new DateTime(2000, 1, 1, 09, 01, 10)},

                new TimeTable() { City = "Phoenix", Time = new DateTime(2000, 1, 1, 09, 00, 03)},
                new TimeTable() { City = "Phoenix", Time = new DateTime(2000, 1, 1, 09, 14, 25)},
                new TimeTable() { City = "Phoenix", Time = new DateTime(2000, 1, 1, 09, 37, 44)},

                new TimeTable() { City = "Seattle", Time = new DateTime(2000, 1, 1, 09, 10, 11)},
                new TimeTable() { City = "Seattle", Time = new DateTime(2000, 1, 1, 09, 10, 25)},
                new TimeTable() { City = "Seattle", Time = new DateTime(2000, 1, 1, 09, 22, 43)},
                new TimeTable() { City = "Seattle", Time = new DateTime(2000, 1, 1, 09, 22, 54)},
                new TimeTable() { City = "Seattle", Time = new DateTime(2000, 1, 1, 09, 36, 14)},


            };
            return list;
        }

        public int CompareTo(object obj)
        {
            TimeTable that = (obj as TimeTable);
            if (that == null)
            {
                throw new Exception("Invalid object type. TimeTable is expected!");
            }

            return new TimeTableComparerByTime().Compare(this, that);
        }
    }



    public class TimeTableComparerByTime : IComparer, IComparer<TimeTable>
    {

        public int Compare(TimeTable thisObj, TimeTable thatObj)
        {
            return DateTime.Compare( thisObj.Time, thatObj.Time);
        }


        public int Compare(object thisObj, object thatObj)
        {
            return Compare((TimeTable)thisObj, (TimeTable)thatObj);
        }
    }

    public class TimeTableComparerByCity : IComparer, IComparer<TimeTable>
    {

        public int Compare(TimeTable thisObj, TimeTable thatObj)
        {
            return string.Compare(thisObj.City, thatObj.City);
        }

        public int Compare(object thisObj, object thatObj)
        {
            return Compare((TimeTable)thisObj, (TimeTable)thatObj);
        }

    }


}

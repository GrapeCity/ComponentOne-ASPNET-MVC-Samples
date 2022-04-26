using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiRowLOB.Models
{
    public class ValueWrapper<TK, TV>
    {
        public ValueWrapper(TK key, TV value)
        {
            Key = key;
            Value = value;
        }

        public TK Key { get; set; }
        public TV Value { get; set; }
    }

    public static class ValueWrapperExtensions
    {
        public static IEnumerable<ValueWrapper<int, T>> ToValues<T>(this IEnumerable<T> items)
        {
            return items.Select((item, index) => new ValueWrapper<int, T>(index, item));
        }
    }
}
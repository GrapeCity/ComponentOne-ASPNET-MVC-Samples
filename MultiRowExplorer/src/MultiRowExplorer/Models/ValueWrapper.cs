using System;
using System.Collections.Generic;
using System.Linq;

namespace MultiRowExplorer.Models
{
    public class ValueWrapper<T>
    {
        public ValueWrapper(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }

    public static class ValueWrapperExtensions
    {
        public static IEnumerable<ValueWrapper<T>> ToValues<T>(this IEnumerable<T> items)
        {
            return items.Select(v => new ValueWrapper<T>(v));
        }
    }
}
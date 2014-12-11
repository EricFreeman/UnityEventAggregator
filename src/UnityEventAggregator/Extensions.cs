using System;
using System.Collections.Generic;

namespace UnityEventAggregator
{
    public static class Extensions
    {
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (enumerable == null) return;
            foreach (var e in enumerable)
                action(e);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hcqn.Core.Tango.Extensions
{
    public static class IEnumerableExtensions
    {

        /// <summary>
        /// se a 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="enumerable"></param> 
        /// <param name="methodWhenSome"></param>
        /// <param name="methodWhenNone"></param>
        /// <returns></returns>
        public static TResult Math<T, TResult>(this IEnumerable<T> enumerable,
            Func<IEnumerable<T>, TResult> methodWhenSome,
            Func<TResult> methodWhenNone)
            => enumerable.Any() ? methodWhenSome(enumerable) : methodWhenNone();
    }
}

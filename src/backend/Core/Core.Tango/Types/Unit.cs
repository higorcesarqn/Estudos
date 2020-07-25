using System;
using System.Threading.Tasks;

namespace Hcqn.Core.Tango.Types
{
    /// <summary>
    /// The unit type is a type that indicates the absence of a specific value; the unit type has only a single value, 
    /// which acts as a placeholder when no other value exists or is needed.
    /// </summary>
    /// <remarks>
    /// The unit type resembles the void type in languages such as C# and C++.
    /// The value of the unit type is often used to hold the place where a value is required by the language syntax, 
    /// but when no value is needed or desired, like in casts from action to functions that needs to return a value.
    /// This is also used as return value in Iterate and Match methods.
    /// </remarks>
    public struct Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
    {

        public static readonly Unit Value = new Unit();


        public static readonly Task<Unit> Task = System.Threading.Tasks.Task.FromResult(Value);

        public int CompareTo(Unit other)
        {
            return 0;
        }

        int IComparable.CompareTo(object obj)
        {
            return 0;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public bool Equals(Unit other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Unit;
        }

        public static bool operator ==(Unit first, Unit second)
        {
            return true;
        }

        public static bool operator !=(Unit first, Unit second)
        {
            return false;
        }

        public override string ToString()
        {
            return "()";
        }
    }
}
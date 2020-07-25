using System;

namespace Hcqn.Core.Types
{
    public readonly struct UserName : IEquatable<UserName>
    {
        public static ushort MiniumLengh = 3;
        public static ushort MaxLengh = 30;

        private readonly string _value;

        public UserName(string value)
        {
            _value = value.ToLower();
        }

        public static UserName Parse(string value)
        {
            if (TryParse(value, out var result))
            {
                return result;
            }

            throw new ArgumentException("O Username informado é inválido.");
        }

        public static bool TryParse(string value, out UserName username)
        {
            var isValid = IsValid(value);
            username = isValid ? new UserName(value) : default;
            return isValid;
        }

        private static bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && value.Length >= MiniumLengh && value.Length <= MaxLengh;
        }

        public static implicit operator UserName(string value)
            => Parse(value);

        public static implicit operator string(UserName value)
            => value._value;

        public override string ToString() => _value;

        public bool Equals(UserName other) => _value == other._value;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is UserName))
            {
                return false;
            }

            var username = (UserName)obj;

            return Equals(username);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode() * 652;
        }

        public static bool operator ==(UserName left, UserName right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UserName left, UserName right)
        {
            return !(left == right);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Hcqn.Core.Types
{
    public readonly struct Email : IEquatable<Email>
    {
        private const string _regex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                                     @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private readonly string _value;

        private Email(string address)
        {
            _value = address?.ToLower();
        }

        public static Email Parse(string value)
        {
            if (TryParse(value, out var result))
            {
                return result;
            }

            throw new ArgumentException("O E-Mail informado é inválido.");
        }

        public static bool TryParse(string value, out Email cpf)
        {
            var isValid = IsValid(value);
            cpf = isValid ? new Email(value) : default;
            return isValid;
        }

        public static implicit operator Email(string value)
          => Parse(value);

        public static implicit operator string(Email value)
          => value._value;

        public override string ToString() => _value;

        private static bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            return Regex.IsMatch(value, _regex);
        }

        public IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }

        public bool Equals(Email other)
        {
            return other._value == _value;
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode() * 250;
        }

        public static bool operator ==(Email left, Email right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Email left, Email right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Email))
            {
                return false;
            }

            var email = (Email)obj;

            return Equals(email);
        }
    }
}
using System;
using System.Collections.Generic;

namespace Hcqn.Core.Types
{
    public readonly struct Cpf : IEquatable<Cpf>
    {
        private readonly string _value;

        private Cpf(string value)
        {
            _value = value;
        }

        public IEnumerable<object> GetAtomicValues()
        {
            yield return _value;
        }

        public static Cpf Parse(string value)
        {
            if (TryParse(value, out var result))
            {
                return result;
            }

            throw new ArgumentException("O CPF informado é inválido.");
        }

        public static bool TryParse(string value, out Cpf cpf)
        {
            var isValid = IsValid(value);
            cpf = isValid ? new Cpf(value) : default;
            return isValid;
        }

        private static bool IsValid(string value)
        {
            static bool VerificaTodosValoresSaoIguais(ref Span<int> input)
            {
                for (var i = 1; i < 11; i++)
                {
                    if (input[i] != input[0])
                    {
                        return false;
                    }
                }

                return true;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            Span<int> cpfArray = stackalloc int[11];
            var count = GetDigitos(value, ref cpfArray);

            if (count != 11) return false;
            if (VerificaTodosValoresSaoIguais(ref cpfArray)) return false;

            var totalDigitoI = 0;
            var totalDigitoII = 0;
            int modI;
            int modII;

            for (var posicao = 0; posicao < cpfArray.Length - 2; posicao++)
            {
                totalDigitoI += cpfArray[posicao] * (10 - posicao);
                totalDigitoII += cpfArray[posicao] * (11 - posicao);
            }

            modI = totalDigitoI % 11;
            if (modI < 2) { modI = 0; }
            else { modI = 11 - modI; }

            if (cpfArray[9] != modI)
            {
                return false;
            }

            totalDigitoII += modI * 2;

            modII = totalDigitoII % 11;
            if (modII < 2) { modII = 0; }
            else { modII = 11 - modII; }

            return cpfArray[10] == modII;
        }

        public override string ToString() => _value;

        public bool Equals(Cpf other)
        {
            if (other == null)
            {
                return false;
            }

            Span<int> cpfArrayOther = new int[11];
            Span<int> cpfArrayThis = new int[11];

            GetDigitos(other._value, ref cpfArrayOther);
            GetDigitos(_value, ref cpfArrayThis);
            var count = 0;

            foreach (var c in cpfArrayOther)
            {
                if (c != cpfArrayThis[count]) return false;
                count++;
            }

            return true;
        }

        private static int GetDigitos(string value, ref Span<int> cpfArray)
        {
            var count = 0;

            foreach (var c in value)
            {
                if (char.IsDigit(c))
                {
                    cpfArray[count] = c - '0';
                    count++;
                }
            }

            return count;
        }

        public static implicit operator Cpf(string value)
          => Parse(value);

        public static implicit operator string(Cpf value)
            => value._value;
    }
}
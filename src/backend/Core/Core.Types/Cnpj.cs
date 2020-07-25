using System;

namespace Hcqn.Core.Types
{
    public readonly struct Cnpj : IEquatable<Cnpj>
    {
        private readonly string _value;

        private Cnpj(string value)
        {
            _value = value;
        }

        public static Cnpj Parse(string value)
        {
            if (TryParse(value, out var result))
            {
                return result;
            }

            throw new ArgumentException("O CNPJ informado é inválido.");
        }

        public static bool TryParse(string value, out Cnpj cpf)
        {
            var isValid = IsValid(value);
            cpf = isValid ? new Cnpj(value) : default;
            return isValid;
        }

        public bool Equals(Cnpj other)
        {
            if (other == null)
            {
                return false;
            }

            Span<int> cnpjArrayOther = stackalloc int[14];
            Span<int> cnpjArrayThis = stackalloc int[14];

            GetDigitos(other._value, ref cnpjArrayOther);
            GetDigitos(_value, ref cnpjArrayThis);
            var count = 0;

            foreach (var c in cnpjArrayOther)
            {
                if (c != cnpjArrayThis[count]) return false;
                count++;
            }

            return true;
        }

        private static int GetDigitos(string value, ref Span<int> cnpjArray)
        {
            var count = 0;

            foreach (var c in value)
            {
                if (char.IsDigit(c))
                {
                    cnpjArray[count] = c - '0';
                    count++;
                }
            }

            return count;
        }

        private static bool IsValid(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            Span<int> tempCnpj = new int[14];

            int pos = 0;
            var todosIguais = true;

            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsDigit(value[i]))
                {
                    if (pos > 13) return false;
                    tempCnpj[pos] = (value[i] - '0');
                    if (todosIguais && (pos > 0))
                    {
                        todosIguais = tempCnpj[pos] == tempCnpj[pos - 1];
                    }
                    pos++;
                }
            }

            if (pos != 14) return false;
            if (todosIguais) return false;

            Span<int> multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            Span<int> multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma1 = 0;
            int soma2 = 0;

            for (int i = 0; i < 12; i++)
            {
                soma1 += tempCnpj[i] * multiplicador1[i];
                soma2 += tempCnpj[i] * multiplicador2[i];
            }

            int resto = (soma1 % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            int digito1 = resto;

            if (tempCnpj[12] != digito1) return false;

            soma2 += digito1 * multiplicador2[12];

            resto = (soma2 % 11);

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            return tempCnpj[13] == resto;
        }

        public override string ToString() => _value;

        public static implicit operator Cnpj(string value)
          => Parse(value);

        public static implicit operator string(Cnpj value)
            => value._value;
    }
}
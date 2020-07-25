using Hcqn.Core.Types;
using FluentAssertions;
using System;
using Xunit;

namespace Core.Types.Tests
{
    [Trait("Core.Types", "Cnpj")]
    public class CnpjTests
    {
        [Theory]
        [InlineData("30.879.270/0001-85")]
        [InlineData("14.123.778/0001-00")]
        [InlineData("55.529.503/0001-40")]
        [InlineData("30879270000185")]
        [InlineData("14123778000100")]
        [InlineData("55529503000140")]
        public void cnpj_validos(string valor)
        {
            Cnpj.TryParse(valor, out var _).Should().BeTrue();
        }

        [Theory]
        //[InlineData("00.000.000/0000-00")]
        [InlineData("11.111.111/1111-11")]
        [InlineData("22.222.222/2222-22")]
        [InlineData("33.333.333/3333-33")]
        [InlineData("44.444.444/4444-44")]
        [InlineData("55.555.555/5555-55")]
        [InlineData("66.666.666/6666-66")]
        [InlineData("77.777.777/7777-77")]
        [InlineData("88.888.888/8888-88")]
        [InlineData("99.999.999/9999-99")]
        [InlineData("30.879.270/0001-75")]
        [InlineData("14.123.778/0008-00")]
        [InlineData("55.527.503/0001-40")]
        public void cnpj_invalidos(string valor)
        {
            Cnpj.TryParse(valor, out var _).Should().BeFalse();
        }

        [Theory]
        //[InlineData("00.000.000/0000-00")]
        [InlineData("11.111.111/1111-11")]
        [InlineData("22.222.222/2222-22")]
        [InlineData("33.333.333/3333-33")]
        [InlineData("44.444.444/4444-44")]
        [InlineData("55.555.555/5555-55")]
        [InlineData("66.666.666/6666-66")]
        [InlineData("77.777.777/7777-77")]
        [InlineData("88.888.888/8888-88")]
        [InlineData("99.999.999/9999-99")]
        [InlineData("30.879.270/0001-75")]
        [InlineData("14.123.778/0008-00")]
        [InlineData("55.527.503/0001-40")]
        public void cnpj_invalidos_throw_ArgumentException(string valor)
        {
            Action ac = () => { Cnpj cpf = valor; };

            ac.Should().ThrowExactly<ArgumentException>("O CNPJ informado é inválido...");
        }

        [Theory]
        [InlineData("30.879.270/0001-85", "30.879.270/0001-85")]
        [InlineData("14.123.778/0001-00", "14123778000100")]
        [InlineData("55.529.503/0001-40", "55529503/0001-40")]
        [InlineData("30879270000185", "30879270000185")]
        [InlineData("14.123.778/0001-00", "14123778/000100")]
        public void cnpf_comparacoes_verdadeiras(string valor1, string valor2)
        {
            Cnpj cnpj1 = valor1;
            Cnpj cnpj2 = valor2;

            cnpj1.Equals(cnpj2).Should().BeTrue();
        }

        [Theory]
        [InlineData("30.879.270/0001-85", "14.123.778/0001-00")]
        [InlineData("14.123.778/0001-00", "55529503/0001-40")]
        public void cnpf_comparacoes_falsas(string valor1, string valor2)
        {
            Cnpj cnpj1 = valor1;
            Cnpj cnpj2 = valor2;

            cnpj1.Equals(cnpj2).Should().BeFalse();
        }
    }
}
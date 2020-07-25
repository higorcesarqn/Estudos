using Hcqn.Core.Types;
using FluentAssertions;
using System;
using Xunit;

namespace Core.Types.Tests
{
    [Trait("Core.Types", "Cpf")]
    public class CpfTests
    {
        [Theory]
        [InlineData("779.543.906-32")]
        [InlineData("304.169.730-00")]
        [InlineData("602.138.910-77")]
        [InlineData("77954390632")]
        [InlineData("30416973000")]
        [InlineData("60213891077")]
        [InlineData("779543906-32")]
        [InlineData("304169730-00")]
        [InlineData("602138910-77")]
        [InlineData("779.543.90632")]
        [InlineData("304.169.73000")]
        [InlineData("602.138.91077")]
        public void cpf_validos(string valor)
        {
            Cpf.TryParse(valor, out var _).Should().BeTrue();
        }

        [Theory]
        [InlineData("000.000.000-00")]
        [InlineData("111.111.111-11")]
        [InlineData("222.222.222-22")]
        [InlineData("333.333.333-33")]
        [InlineData("444.444.444-44")]
        [InlineData("555.555.555-55")]
        [InlineData("666.666.666-66")]
        [InlineData("777.777.777-77")]
        [InlineData("888.888.888-88")]
        [InlineData("999.999.999-99")]
        [InlineData("779.544.906-32")]
        [InlineData("304.169.731-00")]
        [InlineData("602.138.910-76")]
        public void cpf_invalidos(string valor)
        {
            Cpf.TryParse(valor, out var _).Should().BeFalse();
        }

        [Theory]
        [InlineData("000.000.000-00")]
        [InlineData("111.111.111-11")]
        [InlineData("222.222.222-22")]
        [InlineData("333.333.333-33")]
        [InlineData("444.444.444-44")]
        [InlineData("555.555.555-55")]
        [InlineData("666.666.666-66")]
        [InlineData("777.777.777-77")]
        [InlineData("888.888.888-88")]
        [InlineData("999.999.999-99")]
        [InlineData("779.544.906-32")]
        [InlineData("304.169.731-00")]
        [InlineData("602.138.910-76")]
        public void cpf_invalidos_throw_ArgumentException(string valor)
        {
            Action ac = () => { Cpf cpf = valor; };

            ac.Should().ThrowExactly<ArgumentException>("O CPF informado é inválido...");
        }

        [Theory]
        [InlineData("779.543.906-32", "779.543.906-32")]
        [InlineData("779.543.906-32", "779543906-32")]
        [InlineData("779.543.906-32", "779.543.90632")]
        [InlineData("779.543.906-32", "77954390632")]
        public void cpf_comparacoes_verdadeiras(string valor1, string valor2)
        {
            Cpf cpf1 = valor1;
            Cpf cpf2 = valor2;

            cpf1.Equals(cpf2).Should().BeTrue();
        }

        [Theory]
        [InlineData("779.543.906-32", "602.138.910-77")]
        [InlineData("779.543.906-32", "602138910-77")]
        [InlineData("779.543.906-32", "602.138.91077")]
        [InlineData("779.543.906-32", "60213891077")]
        public void cpf_comparacoes_falsas(string valor1, string valor2)
        {
            Cpf cpf1 = valor1;
            Cpf cpf2 = valor2;

            cpf1.Equals(cpf2).Should().BeFalse();
        }
    }
}
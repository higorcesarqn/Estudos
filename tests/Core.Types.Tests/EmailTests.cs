using Hcqn.Core.Types;
using FluentAssertions;
using Xunit;

namespace Core.Types.Tests
{
    [Trait("Core.Types", "Email")]
    public class EmailTests
    {
        [Theory]
        [InlineData("email@egl.eng.br")]
        [InlineData("email-outro@outlook.com")]
        [InlineData("outroemail@gmail.com")]
        [InlineData("email.email@gov.br")]
        [InlineData("email_email@gov.br")]
        public void emails_validos(string valor)
        {
            Email.TryParse(valor, out var _).Should().BeTrue();

            Email email = valor;

            email.Should().NotBe(default);
        }

        [Theory]
        [InlineData("email@egl..br")]
        [InlineData("email-outro@com")]
        [InlineData("@gmail.com")]
        [InlineData("email.email@")]
        [InlineData("emailegl.eng.br")]
        public void emails_invalidos(string valor)
        {
            Email.TryParse(valor, out var _).Should().BeFalse();
        }

        [Theory]
        [InlineData("email@egl.eng.br")]
        [InlineData("email-outro@outlook.com")]
        [InlineData("outroemail@gmail.com")]
        [InlineData("email.email@gov.br")]
        [InlineData("email_email@gov.br")]
        public void email_comparacoes_verdadeiras(string valor)
        {
            Email email1 = valor;
            Email email2 = valor;

            email1.Equals(email2).Should().BeTrue();

            (email1 == email2).Should().BeTrue();

            (email1 != email2).Should().BeFalse();
        }

        [Theory]
        [InlineData("email@egl.eng.br", "email-outro@outlook.com")]
        [InlineData("outroemail@gmail.com", "email_email@gov.br")]
        [InlineData("email.email@gov.br", "emailemail@gov.br")]
        public void email_comparacoes_falsas(string valor1, string valor2)
        {
            Email email1 = valor1;
            Email email2 = valor2;

            email1.Equals(email2).Should().BeFalse();

            (email1 == email2).Should().BeFalse();

            (email1 != email2).Should().BeTrue();
        }
    }
}
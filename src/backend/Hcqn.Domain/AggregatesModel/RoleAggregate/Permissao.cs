using Hcqn.Core.Models;
using System.Collections.Generic;

namespace Hcqn.Domain.AggregatesModel.RoleAggregate
{
    public class Permissao : ValueObject
    {
        public Permissao(string nome)
        {
            Nome = nome;
        }

        protected Permissao() { }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Titulo { get; protected set; }
        public string Descricao { get; protected set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Nome;
        }
    }
}

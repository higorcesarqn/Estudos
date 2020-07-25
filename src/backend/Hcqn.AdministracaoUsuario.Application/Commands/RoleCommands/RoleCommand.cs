using System;
using System.Collections.Generic;
using System.Linq;

namespace Hcqn.AdministracaoUsuario.Application.Commands.RoleCommands
{
    public abstract class RoleCommand
    {
        protected RoleCommand(string nome, IEnumerable<string> permissions)
        {
            Id = Guid.NewGuid();
            Name = nome;
            Permissions = permissions ?? Enumerable.Empty<string>();
        }

        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public IEnumerable<string> Permissions { get; protected set; }
    }
}
using System;
using System.Collections.Generic;

namespace Hcqn.Domain.AggregatesModel.RoleAggregate.Dtos
{
    public class RoleDetailsDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<string> Permissoes { get; set; }
    }
}

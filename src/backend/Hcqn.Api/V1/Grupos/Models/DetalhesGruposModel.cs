using System.Collections.Generic;

namespace Hcqn.Api.V1.Grupos.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DetalhesGruposModel
    {
        public string Nome { get; set; }
        public IEnumerable<string> Permissoes { get; set; }
    }
}

using Hcqn.Core.Models;

namespace Hcqn.Teste.Geo.Entities
{
    public class Lote : EntityGeo
    {
        public string CodigoPlantaQuadra { get; set; }
        public string InscricaoFiscal { get; set; }
        public string LoteProjetado { get; set; }
        public string NumeroLote { get; set; }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hcqn.Core.Gis
{
    public static class SRID
    {
        private static IDictionary<int, string> _srids;
        //private static readonly object obj = new object();

        public static async Task<string> GetProjcs(int srid)
        {
            if (_srids == null)
            {
                var rootDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.
                GetExecutingAssembly().CodeBase).Remove(0, 5);

                _srids = (await System.IO.File.ReadAllLinesAsync(System.IO.Path.Combine(rootDir, "SRID.csv")))
                    .Select(x => x.Split(';'))
                    .ToDictionary(x => int.Parse(x[0]), x => x[1]);
            }

            return _srids[srid];

        }
    }
}

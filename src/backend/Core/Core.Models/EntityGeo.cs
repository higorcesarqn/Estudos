using NetTopologySuite.Geometries;

namespace Hcqn.Core.Models
{
    public abstract class EntityGeo : Entity
    {
        public Geometry Geom { get; protected set; }

        public void EditarGeom(Geometry geometry)
        {
            Geom = geometry;
        }
    }
}
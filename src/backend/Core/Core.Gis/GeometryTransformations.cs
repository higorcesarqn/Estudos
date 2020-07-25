using Hcqn.Core.Models;
using GeoAPI.CoordinateSystems.Transformations;
using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hcqn.Core.Gis
{
    public class GeometryTransformations
    {
        /// <summary>
        /// Obtem as projeções de transformações a partir dos wkid de entra e saida
        /// </summary>
        /// <param name="wkidSource">wkid de entrada</param>
        /// <param name="wkidTarget">wkid de saida</param>
        /// <returns></returns>
        private async Task<ICoordinateTransformation> GetTransformation(int wkidSource, int wkidTarget)
        {
            var source = SRID.GetProjcs(wkidSource);
            var target = SRID.GetProjcs(wkidTarget);

            var csFact = new CoordinateSystemFactory();
            var ctFact = new CoordinateTransformationFactory();

            var utmSource = csFact.CreateFromWkt(await source);
            var utmTarget = csFact.CreateFromWkt(await target);

            return ctFact.CreateFromCoordinateSystems(utmSource, utmTarget);
        }

        public async Task<EntityGeo> Transform(EntityGeo entityGeo, int wkid)
        {
            entityGeo.EditarGeom(await Transform(entityGeo.Geom, wkid));
            return entityGeo;
        }

        public async IAsyncEnumerable<EntityGeo> Transform(IEnumerable<EntityGeo> entities, int wkid)
        {
            foreach (var item in entities)
            {
                yield return await Transform(item, wkid);
            }
        }

        /// <summary>
        /// Projeta um Point para um nova projeção a partir de um wkid de saida.
        /// </summary>
        /// <param name="point">Geometria a ser projetada na nova projeção</param>
        /// <param name="wkid">wkid de saida</param>
        /// <returns>transformed Point</returns>
        private async Task<Point> Transform(Point point, int wkid)
        {
            var transformation = await GetTransformation(point.SRID, wkid);
            var pt = transformation.MathTransform.Transform(new double[] { point.X, point.Y });
            return new Point(pt[0], pt[1]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geometries"></param>
        /// <param name="wkid"></param>
        /// <returns></returns>
        private async Task<MultiPolygon> Transform(MultiPolygon geometries, int wkid)
        {
            var transformation = await GetTransformation(geometries.SRID, wkid);
            var polygons = geometries.Select(x => x.Coordinates);

            var polygonsTransforme = polygons.Select((polygon) =>
            {
                var cordenadasPre = transformation.MathTransform.TransformList(polygon.Select(x => new[] { x.X, x.Y }).ToList());
                var pt = transformation.MathTransform.TransformList(geometries.Coordinates.Select(x => new[] { x.X, x.Y }).ToList());
                var cordenadas = pt.Select(x => new Coordinate(x[0], x[1])).ToArray();
                var lineRing = new LinearRing(cordenadas);
                return new Polygon(lineRing);
            });

            return new MultiPolygon(polygonsTransforme.Distinct().ToArray());
        }

        private async Task<Polygon> Transform(Polygon polygon, int wkid)
        {
            var transformation = await GetTransformation(polygon.SRID, wkid);
            var pt = transformation.MathTransform.TransformList(polygon.Coordinates.Select(x => new[] { x.X, x.Y }).ToList());
            var cordenadas = pt.Select(x => new Coordinate(x[0], x[1])).ToArray();
            var lineRing = new LinearRing(cordenadas);

            return new Polygon(lineRing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="wkid"></param>
        /// <returns></returns>
        public async Task<Geometry> Transform(Geometry geometry, int wkid)
        {
            if (geometry == default)
            {
                return default;
            }

            return (geometry.GeometryType.ToLower()) switch
            {
                "point" => await Transform((Point)geometry, wkid),
                "multipolygon" => await Transform((MultiPolygon)geometry, wkid),
                "polygon" => await Transform((Polygon)geometry, wkid),
                _ => null,
            };
        }

    }
}

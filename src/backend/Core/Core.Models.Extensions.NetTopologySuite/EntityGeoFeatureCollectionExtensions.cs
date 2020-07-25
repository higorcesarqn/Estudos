using Humanizer;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hcqn.Core.Models.Extensions.NetTopologySuite
{
    public static class EntityGeoFeatureCollectionExtensions
    {
        public static async Task<FeatureCollection> ToFeatureCollection<TEntityGeo>(this IAsyncEnumerable<TEntityGeo> entities) where TEntityGeo : EntityGeo
        {
            var features = ConvertToCollection(entities).ConfigureAwait(false);
            var featureCollection = new FeatureCollection();

            await foreach (var item in features)
            {
                featureCollection.Add(item);
            }

            return featureCollection;
        }


        private static async IAsyncEnumerable<IFeature> ConvertToCollection(this IAsyncEnumerable<EntityGeo> entitysGeo)
        {
            await foreach (var entityGeo in entitysGeo)
            {
                yield return ToFeature(entityGeo);
            }
        }

        public static Feature ToFeature(this EntityGeo entity)
        {
            var type = entity.GetType();

            var attributes = type
                .GetProperties()
                .Where(x => x.PropertyType != typeof(Geometry) && (x.Name != "CreatedAt" && x.Name != "UpdatedAt"))
                .ToDictionary(key => key.Name.Camelize(), value => value.GetValue(entity));

            return new Feature(entity.Geom, new AttributesTable(attributes))
            {
                BoundingBox = entity.Geom.Envelope.EnvelopeInternal
            };
        }
    }
}

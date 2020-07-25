using System.Text.Json;

namespace Hcqn.Caching
{
    internal static class Serialization
    {
        public static byte[] ToByteArray(this object obj)
        {
            if (obj == null)
            {
                return null;
            }

            return JsonSerializer.SerializeToUtf8Bytes(obj);
        }
        public static T FromByteArray<T>(this byte[] byteArray) where T : class
        {
            if (byteArray == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(byteArray);
        }

    }
}
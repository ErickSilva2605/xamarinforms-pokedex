using System.Reflection;

namespace PokedexXF.Extensions
{
    public static class ResourcesExtensions
    {
        public static object FindResource(this ResourceDictionary mainResourceDictionary, string resourceKey)
        {
            var mergedResources = mainResourceDictionary
                .GetType()
                .GetProperty("MergedResources", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(mainResourceDictionary);

            var dict = ((IEnumerable<KeyValuePair<string, object>>)mergedResources)
                .ToDictionary(x => x.Key, y => y.Value);

            if (dict?.ContainsKey(resourceKey) == true)
            {
                return dict[resourceKey];
            }

            return null;
        }
    }
}

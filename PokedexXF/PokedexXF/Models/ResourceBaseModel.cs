using Xamarin.CommunityToolkit.ObjectModel;

namespace PokedexXF.Models
{
    public abstract class ResourceBaseModel : ObservableObject
    {
        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string NameFirstCharUpper { get; }
        public abstract string NameUpperCase { get; }
        public abstract string ApiEndpoint { get; }
    }
}

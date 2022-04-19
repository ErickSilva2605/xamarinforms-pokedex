namespace PokedexXF.Helpers
{
    public class Constants
    {
        internal const string BASE_URL = "https://pokeapi.co/api/v2/";
        internal const string BASE_URL_RESOURCE_LIST = "https://pokeapi.co/api/v2/pokemon/?offset={0}&limit={1}";
        internal const string BASE_IMAGE_URL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/other/official-artwork/{0}.png";
        internal const string ENDPOINT_POKEMON = "pokemon";
        internal const string ENDPOINT_ABILITY = "ability";
        internal const string ENDPOINT_SPECIES = "pokemon-species";
        internal const string ENDPOINT_EVOLUTION_CHAIN = "evolution-chain";
        internal const string ENDPOINT_TYPE = "type";
        internal const string ENDPOINT_EGG_GROUP = "egg-group";
        internal const string ENDPOINT_GROWTH_RATE = "growth-rate";
        internal const string ENDPOINT_POKEDEX = "pokedex";
        internal const string ENDPOINT_LANGUAGE = "language";
        internal const string ENDPOINT_VERSION = "version";
        internal const string ENDPOINT_VERSION_GROUP = "version-group";
        internal const string ENDPOINT_STAT = "stat";
        internal const string ENDPOINT_GENERATION = "generation";
        internal const int EGG_CYCLE_STEPS = 257;
        internal const int POKEMON_MAX_LEVEL = 100;
        internal const int EV_MAX = 252;
        internal const int EV_MIN = 0;
        internal const int IV_MAX = 31;
        internal const int IV_MIN = 0;
        internal const double NATURE_MAX = 1.1;
        internal const double NATURE_MIN = 0.9;
        internal const double KILOGRAM_CONVERTER_VALUE = 10;
        internal const double METERS_CONVERTER_VALUE = 10;
        internal const double POUNDS_CONVERTER_VALUE = 2.2046;
        internal const double FEET_CONVERTER_VALUE = 0.3048;
        internal const double INCH_CONVERTER_VALUE = 0.0833;
        internal const int PAGE_LIMIT = 20;
        internal const int POKEMON_LIMIT = 898;
    }
}

namespace PokedexXF.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            if(string.IsNullOrEmpty(input))
                return input;

            return input[0].ToString().ToUpper() + input.Substring(1);
        }
    }
}

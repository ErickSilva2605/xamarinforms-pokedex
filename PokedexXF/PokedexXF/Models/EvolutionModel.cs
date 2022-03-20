namespace PokedexXF.Models
{
    public class EvolutionModel
    {
        public int Id { get; set; }

        public int EnvolvesToId { get; set; }

        public string Name { get; set; }

        public string EnvolvesToName { get; set; }

        public string Image { get; set; }

        public string EnvolvesToImage { get; set; }

        public string EnvolvesToMinLevel { get; set; }

        public bool HasEvolution { get; set; }

        public bool IsBaby { get; set; }
    }
}

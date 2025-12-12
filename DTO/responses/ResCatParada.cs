namespace UTMA_ROUTES_API.DTO.responses
{
    public class ResCatParada
    {
        public int eCodParada { get; set; }

        public string tRutaNombre { get; set; } = null!;

        public string tNombre { get; set; } = null!;

        public decimal dLatitud { get; set; }

        public decimal dLongitud { get; set; }
    }
}

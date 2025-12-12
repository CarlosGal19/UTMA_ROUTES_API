namespace UTMA_ROUTES_API.DTO.requests
{
    public class ReqCatParada
    {
        public int eCodRuta { get; set; }
        public string tNombre { get; set; } = null!;
        public decimal dLatitud { get; set; }
        public decimal dLongitud { get; set; }
        public int eCodeRuta { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace UTMA_ROUTES_API.DTO.requests
{
    public class ReqCatRuta
    {
        [Required]
        public string tNombre { get; set; }
        
        [Required]
        public int eNumero { get; set; }

        [Required]
        public string tDescripcion { get; set; }

        [Required]
        public int eCodZona { get; set; }
    }
}

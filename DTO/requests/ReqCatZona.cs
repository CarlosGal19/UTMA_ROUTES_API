using System.ComponentModel.DataAnnotations;

namespace UTMA_ROUTES_API.DTO.requests
{
    public class ReqCatZona
    {
        [Required, MaxLength(64)]
        public string tNombre { get; set; }

        [Required, MaxLength(128)]
        public string tDescripcion { get; set; }
    }
}

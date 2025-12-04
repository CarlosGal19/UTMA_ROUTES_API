using System.ComponentModel.DataAnnotations;

namespace UTMA_ROUTES_API.DTO.requests
{
    public class ReqCatHorario
    {
        [Required]
        public int eCodRuta { get; set; }
        [Required]
        public TimeOnly tmHoraSalida { get; set; }
        [Required]
        public TimeOnly tmHoraEntrada{ get; set; }
    }
}

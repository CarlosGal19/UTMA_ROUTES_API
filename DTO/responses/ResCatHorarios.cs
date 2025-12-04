namespace UTMA_ROUTES_API.DTO.responses
{
    public class ResCatHorarios
    {
        public int eCodHorario { get; set; }
        public string tRutaNombre { get; set; }
        public TimeOnly tmHoraSalida { get; set; }
        public TimeOnly tmHoraEntrada { get; set; }
    }
}

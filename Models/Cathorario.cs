using System;
using System.Collections.Generic;

namespace UTMA_ROUTES_API.Models;

public partial class Cathorario
{
    public int ECodHorario { get; set; }

    public int ECodRuta { get; set; }

    public TimeOnly TmHoraSalida { get; set; }

    public TimeOnly TmHoraEntrada { get; set; }

    public virtual Catruta ECodRutaNavigation { get; set; } = null!;
}

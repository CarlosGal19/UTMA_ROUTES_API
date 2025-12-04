using System;
using System.Collections.Generic;

namespace UTMA_ROUTES_API.Models;

public partial class Relrutasparada
{
    public int ECodRutasParadas { get; set; }

    public int ECodRuta { get; set; }

    public int ECodParada { get; set; }

    public virtual Catparada ECodParadaNavigation { get; set; } = null!;

    public virtual Catruta ECodRutaNavigation { get; set; } = null!;
}

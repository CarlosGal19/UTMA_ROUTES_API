using System;
using System.Collections.Generic;

namespace UTMA_ROUTES_API.Models;

public partial class Catparada
{
    public int ECodParada { get; set; }

    public int ECodRuta { get; set; }

    public string TNombre { get; set; } = null!;

    public decimal DLatitud { get; set; }

    public decimal DLongitud { get; set; }

    public virtual Catruta ECodRutaNavigation { get; set; } = null!;

    public virtual ICollection<Relrutasparada> Relrutasparada { get; set; } = new List<Relrutasparada>();
}

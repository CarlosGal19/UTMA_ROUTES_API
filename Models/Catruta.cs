using System;
using System.Collections.Generic;

namespace UTMA_ROUTES_API.Models;

public partial class Catruta
{
    public int ECodRuta { get; set; }

    public string TNombre { get; set; } = null!;

    public int ENumero { get; set; }

    public string TDescripcion { get; set; } = null!;

    public int ECodZona { get; set; }

    public virtual ICollection<Cathorario> Cathorarios { get; set; } = new List<Cathorario>();

    public virtual ICollection<Catparada> Catparada { get; set; } = new List<Catparada>();

    public virtual Catzona ECodZonaNavigation { get; set; } = null!;

    public virtual ICollection<Relrutasparada> Relrutasparada { get; set; } = new List<Relrutasparada>();
}

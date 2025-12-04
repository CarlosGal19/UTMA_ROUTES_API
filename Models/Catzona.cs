using System;
using System.Collections.Generic;

namespace UTMA_ROUTES_API.Models;

public partial class Catzona
{
    public int ECodZona { get; set; }

    public string TNombre { get; set; } = null!;

    public string? TDescripcion { get; set; }

    public virtual ICollection<Catruta> Catruta { get; set; } = new List<Catruta>();
}

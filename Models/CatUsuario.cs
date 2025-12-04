using System;
using System.Collections.Generic;

namespace UTMA_ROUTES_API.Models;

public partial class CatUsuario
{
    public uint ECodUsuario { get; set; }

    public string TNombre { get; set; } = null!;

    public string TCorreo { get; set; } = null!;

    public string TUsuario { get; set; } = null!;

    public string TPassword { get; set; } = null!;

    public string? TPuesto { get; set; }

    public string? TImagen { get; set; }

    public DateTime FhFechaRegistro { get; set; }

    public DateTime? FhFechaActualizacion { get; set; }

    public string TCodEstatus { get; set; } = null!;
}

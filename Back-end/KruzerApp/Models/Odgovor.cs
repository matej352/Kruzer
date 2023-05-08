using System;
using System.Collections.Generic;

namespace KruzerApp.Models;

public partial class Odgovor
{
    public int Id { get; set; }

    public string Sadržaj { get; set; } = null!;

    public DateOnly Vrijeme { get; set; }

    public int ZaposlenikId { get; set; }

    public int UpitId { get; set; }

    public virtual Upit Upit { get; set; } = null!;

    public virtual Zaposlenik Zaposlenik { get; set; } = null!;
}

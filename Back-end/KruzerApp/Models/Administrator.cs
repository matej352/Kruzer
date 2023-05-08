using System;
using System.Collections.Generic;

namespace KruzerApp.Models;

public partial class Administrator
{
    public int Id { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Nadimak { get; set; } = null!;

    public string Lozinka { get; set; } = null!;

    public virtual ICollection<Krstarenje> Krstarenjes { get; set; } = new List<Krstarenje>();
}

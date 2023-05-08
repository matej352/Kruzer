using System;
using System.Collections.Generic;

namespace KruzerApp.Models;

public partial class Lokacija
{
    public int Id { get; set; }

    public string Grad { get; set; } = null!;

    public string Država { get; set; } = null!;

    public virtual ICollection<Krstarenje> Krstarenjes { get; set; } = new List<Krstarenje>();
}

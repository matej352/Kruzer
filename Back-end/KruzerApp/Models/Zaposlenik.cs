using System;
using System.Collections.Generic;

namespace KruzerApp.Models;

public partial class Zaposlenik
{
    public int Id { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Nadimak { get; set; } = null!;

    public string Oib { get; set; } = null!;

    public string Lozinka { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Odgovor> Odgovors { get; set; } = new List<Odgovor>();
}

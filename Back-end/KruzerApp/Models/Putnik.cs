using System;
using System.Collections.Generic;

namespace KruzerApp.Models;

public partial class Putnik
{
    public int Id { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Nadimak { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Lozinka { get; set; } = null!;

    public char Spol { get; set; }

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();

    public virtual ICollection<Upit> Upits { get; set; } = new List<Upit>();
}

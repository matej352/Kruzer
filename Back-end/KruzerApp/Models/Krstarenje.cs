using System;
using System.Collections.Generic;

namespace KruzerApp.Models;

public partial class Krstarenje
{
    public int Id { get; set; }

    public string Naslov { get; set; } = null!;

    public string Opis { get; set; } = null!;

    public DateOnly Datumpocetak { get; set; }

    public DateOnly Datumkraj { get; set; }

    public int Kapacitet { get; set; }

    public int Popunjenost { get; set; }

    public int AdminId { get; set; }

    public virtual Administrator Admin { get; set; } = null!;

    public virtual ICollection<Rezervacija> Rezervacijas { get; set; } = new List<Rezervacija>();

    public virtual ICollection<Upit> Upits { get; set; } = new List<Upit>();

    public virtual ICollection<Lokacija> Lokacijas { get; set; } = new List<Lokacija>();
}

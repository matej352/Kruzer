using System;
using System.Collections.Generic;

namespace KruzerApp.Models;

public partial class Upit
{
    public int Id { get; set; }

    public string Sadržaj { get; set; } = null!;

    public DateOnly Vrijeme { get; set; }

    public int PutnikId { get; set; }

    public int KrstarenjeId { get; set; }

    public virtual Krstarenje Krstarenje { get; set; } = null!;

    public virtual ICollection<Odgovor> Odgovors { get; set; } = new List<Odgovor>();

    public virtual Putnik Putnik { get; set; } = null!;
}

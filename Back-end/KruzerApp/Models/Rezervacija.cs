using System;
using System.Collections.Generic;

namespace KruzerApp.Models;

public partial class Rezervacija
{
    public int Id { get; set; }

    public DateOnly Vrijeme { get; set; }

    public int Brojputnika { get; set; }

    public int KrstarenjeId { get; set; }

    public int PutnikId { get; set; }

    public virtual Krstarenje Krstarenje { get; set; } = null!;

    public virtual Putnik Putnik { get; set; } = null!;
}

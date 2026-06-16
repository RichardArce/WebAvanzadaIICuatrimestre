using System;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class Telefono
{
    public int Id { get; set; }

    public string Numero { get; set; } = null!;

    public int Fkduenno { get; set; }

    public virtual Duenno? FkduennoNavigation { get; set; }
}

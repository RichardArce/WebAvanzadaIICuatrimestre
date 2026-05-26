using System;
using System.Collections.Generic;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class Carro
{
    public int Id { get; set; }

    public string Placa { get; set; } = null!;

    public string Chocado { get; set; } = null!;

    public decimal ValorFiscal { get; set; }

    public string Marca { get; set; } = null!;

    public int? Fkduenno { get; set; }

    public virtual Duenno? FkduennoNavigation { get; set; }
}

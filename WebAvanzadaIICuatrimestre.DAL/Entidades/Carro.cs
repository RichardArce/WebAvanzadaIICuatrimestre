using System;
using System.Collections.Generic;

namespace WebAvanzadaIICuatrimestre.DAL.Entidades;

public partial class Carro
{
    public int Id { get; set; }

    public string Placa { get; set; } = null!;

    // Stored as integer in DB (1 = SI, 0 = NO)
    public int Chocado { get; set; }

    public decimal ValorFiscal { get; set; }

    public string Marca { get; set; } = null!;

    public int? Fkduenno { get; set; }

    public virtual Duenno? FkduennoNavigation { get; set; }
}

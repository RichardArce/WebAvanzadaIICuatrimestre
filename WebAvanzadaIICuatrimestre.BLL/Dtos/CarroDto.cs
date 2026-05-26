using System;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class CarroDto
    {
        public int Id { get; set; }

        public string Placa { get; set; } = null!;

        public string Chocado { get; set; } = null!;

        public decimal ValorFiscal { get; set; }

        public string Marca { get; set; } = null!;

        public int? Fkduenno { get; set; }
    }
}

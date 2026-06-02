using System;

//DataAnotations
namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class CarroDto
    {
        public int Id { get; set; }

        public string Placa { get; set; } = null!;

        public int Chocado { get; set; }

        public decimal ValorFiscal { get; set; }

        public string Marca { get; set; } = null!;

        public int? Fkduenno { get; set; }
        public DuennoDto? Duenno { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class DuennoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Apellido1 { get; set; } = string.Empty;
        public string Apellido2 { get; set; } = string.Empty;
        //public List<CarroDto> Carros { get; set; } = new List<CarroDto>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class DuennoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es requerido")]
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        [Required(ErrorMessage = "El Apellido1 es requerido")]
        public string Apellido1 { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Apellido2 es requerido")]
        public string Apellido2 { get; set; } = string.Empty;
        //public List<CarroDto> Carros { get; set; } = new List<CarroDto>();
    }
}

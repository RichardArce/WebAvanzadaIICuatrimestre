using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class DuennoDto
    {
        public int ?Id { get; set; } //Dejar los identificados de los DTS o ViewModels obligatoriamente es un error, ya que el DTO o ViewModel es un objeto que se utiliza para transferir datos entre capas, y no necesariamente tiene que tener un identificador, esto para mantener una buena separacion de responsabilidades y evitar que el DTO o ViewModel tenga demasiada logica de negocio, sin embargo, en este caso se deja el Id para poder identificar al duenno en caso de ser necesario, pero no es obligatorio.

        [Required(ErrorMessage ="El nombre es requerido")] //Viejito
        public string Nombre { get; set; } = string.Empty;
        public int Edad { get; set; }
        [Required(ErrorMessage = "El Apellido1 es requerido")]
        public string Apellido1 { get; set; } = string.Empty;
        [Required(ErrorMessage = "El Apellido2 es requerido")]
        public string Apellido2 { get; set; } = string.Empty;
        public List<TelefonoDto> Telefonos { get; set; } = new List<TelefonoDto>();
        //public List<CarroDto> Carros { get; set; } = new List<CarroDto>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebAvanzadaIICuatrimestre.BLL.Dtos
{
    public class TelefonoDto
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "El número de teléfono es requerido")]
        public string Numero { get; set; } = string.Empty;
    }
}

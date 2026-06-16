using Microsoft.AspNetCore.Mvc.Rendering;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.Models
{
    public class CarroViewModel //Sin uso por el momento, pero se deja para futuras implementaciones, ya que es una buena práctica utilizar ViewModels para mantener la lógica de presentación separada de la lógica de negocio, aunque en este caso se optó por utilizar ViewBag para pasar los datos de los dueños a la vista, lo cual es una solución más rápida y sencilla para este caso específico, pero no es la mejor práctica a largo plazo.
    {
        public CarroDto Carro { get; set; }
        public List<SelectListItem> Duennos { get; set; }
    }
}

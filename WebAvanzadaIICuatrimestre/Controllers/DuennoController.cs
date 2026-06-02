using Microsoft.AspNetCore.Mvc;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.Duenno;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class DuennoController : Controller
    {
        private readonly IDuennoServicio _duennoServicio;


        public DuennoController(IDuennoServicio duennoServicio)
        {
            _duennoServicio = duennoServicio;
        }

        //Vamos a trabajrForma incorrecta segun la IA, se recomienda utilizar un ViewModel o DTO para pasar la informacion necesaria a la vista, en lugar de cargar la informacion directamente en el controlador y pasarla a la vista a través de ViewBag o ViewData, esto para mantener una buena separacion de responsabilidades y evitar que el controlador tenga demasiada logica de negocio.
        public IActionResult Index() //Se puede llegar a cargar informacion necesario para la vista, como una lista de duennos, etc. utilizando el servicio de duenno para obtener los datos necesarios y pasarlos a la vista a través de un modelo o ViewBag.
        {
            //ViewBag.Duennos = _duennoServicio.GetAllDuennos(); // Ejemplo de carga de datos para la vista, se puede utilizar un modelo o ViewBag para pasar los datos a la vista.

            //Crear un DTO  o ViewModel para la vista, que contenga la informacion necesaria para mostrar en la vista, como una lista de duennos, etc. y pasar ese DTO o ViewModel a la vista.
            return View();
        }

        public async Task<IActionResult> ObtenerDuennos() // Metodos pequeños, el controlador no sabe respuesta de negocio, solo sabe que tiene que llamar al servicio y devolver la respuesta, la logica de negocio se encuentra en el servicio, esto para mantener una buena separacion de responsabilidades y evitar que el controlador tenga demasiada logica de negocio.
        {
            var respuesta = await _duennoServicio.GetDuennos();
            return Json(respuesta);
        }

        public async Task<IActionResult> CrearDuenno(DuennoDto duenno)// Model Binding, reicibir el objeto completo
        {
            var respuesta = await _duennoServicio.CreateDuenno(duenno);
            return Json(respuesta);
        }


        //Por que lo hago así? Por la facilidad de crear la vista y mostrar la informacion, pero no es la forma recomendada por la IA, se recomienda utilizar un ViewModel o DTO para pasar la informacion necesaria a la vista, en lugar de cargar la informacion directamente en el controlador y pasarla a la vista a través de ViewBag o ViewData, esto para mantener una buena separacion de responsabilidades y evitar que el controlador tenga demasiada logica de negocio.
    }
}

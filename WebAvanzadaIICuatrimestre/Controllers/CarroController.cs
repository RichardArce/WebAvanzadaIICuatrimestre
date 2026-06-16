using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.BLL.Services.Carro;
using WebAvanzadaIICuatrimestre.BLL.Services.Duenno;
using WebAvanzadaIICuatrimestre.Models;

namespace WebAvanzadaIICuatrimestre.Controllers
{
    public class CarroController : Controller
    {
        private readonly ICarroServicio _carroServicio;
        private readonly IDuennoServicio _duennoServicio;

        public CarroController(ICarroServicio carroServicio, IDuennoServicio duennoServicio)
        {
            _carroServicio = carroServicio;
            _duennoServicio = duennoServicio;
        }       

        /*public async Task<IActionResult> Index()
        {
            var vm = new CarroViewModel
            {
                Carro = new CarroDto()
            };

            var resp = await _duennoServicio.GetDuennos();
            var duennos = resp.Dato ?? new List<DuennoDto>();

            vm.Duennos = duennos
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Nombre })
                .ToList();

            return View(vm);
        }-*/       

        public async Task<IActionResult> Index() //Que puede llegar a ser m'as facil de entender e implementar, aunque el ViewModel es una buena práctica para mantener la lógica de presentación separada de la lógica de negocio.
        {
            var resp = await _duennoServicio.GetDuennos(); 
            ViewBag.Duennos = resp.Dato;
            return View();
        }
        //Separacion de responsabilidad, capacidad para darle mantenimiento al codigo facilmente y claridad de lo que se hace

        public async Task<IActionResult> GetCarros()
        {
            var respuesta = await _carroServicio.GetCarros();
            return Json(respuesta);
        }

        public async Task<IActionResult> CreateCarro(CarroDto carro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _carroServicio.CreateCarro(carro);
            return Json(respuesta);
        }

        public async Task<IActionResult> GetCarroById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _carroServicio.GetCarroById(id);
            return Json(respuesta);
        }

        public async Task<IActionResult> UpdateCarro(CarroDto carro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _carroServicio.UpdateCarro(carro);
            return Json(respuesta);
        }

        public async Task<IActionResult> DeleteCarro(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var respuesta = await _carroServicio.DeleteCarro(id);
            return Json(respuesta);
        }
    }
}

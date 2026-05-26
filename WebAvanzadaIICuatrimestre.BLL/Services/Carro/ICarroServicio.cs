using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Carro
{
    public interface ICarroServicio
    {
        Task<Respuesta<List<CarroDto>>> GetCarros();
        Task<Respuesta<CarroDto?>> GetCarroById(int id);
        Task<Respuesta<CarroDto>> CreateCarro(CarroDto carro);
        Task<Respuesta<CarroDto>> UpdateCarro(CarroDto carro);
        Task<Respuesta<CarroDto>> DeleteCarro(int id);
    }
}

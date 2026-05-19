using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebAvanzadaIICuatrimestre.DAL.Repositorios.Carro
{
    public interface ICarroRepositorio
    {
        Task<List<Entidades.Carro>> GetCarros();
        Task<Entidades.Carro?> GetCarroById(int id);
        Task<bool> CreateCarro(Entidades.Carro carro);
        Task<bool> UpdateCarro(Entidades.Carro carro);
        Task<bool> DeleteCarro(int id);
    }
}

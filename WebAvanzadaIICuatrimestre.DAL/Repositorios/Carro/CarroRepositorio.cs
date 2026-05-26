using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAvanzadaIICuatrimestre.DAL.Data;

namespace WebAvanzadaIICuatrimestre.DAL.Repositorios.Carro
{
    public class CarroRepositorio : ICarroRepositorio
    {

        //INYECCION DE DEPENDENCIAS
        private readonly ApplicationDbContext _context; 

        public CarroRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCarro(Entidades.Carro carro)
        {
            if (carro == null) return false;
            await _context.Carros.AddAsync(carro);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCarro(int id)
        {
            var entity = await _context.Carros.FindAsync(id);
            if (entity == null) return false;

            _context.Carros.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Entidades.Carro?> GetCarroById(int id)
        {
            // Use FindAsync which returns null if not found.
            return await _context.Carros.FindAsync(id);
        }

        public async Task<List<Entidades.Carro>> GetCarros()
        {
            return await _context.Carros.ToListAsync();
        }

        public async Task<bool> UpdateCarro(Entidades.Carro carro)
        {
            if (carro == null) return false; //Validaciones básicas
            var existing = await _context.Carros.FindAsync(carro.Id);
            if (existing == null) return false; //validaciones básicas

            // Copiar valores actualizados al entity existente
            existing.Placa = carro.Placa ?? existing.Placa;
            existing.Marca = carro.Marca ?? existing.Marca;
            existing.Fkduenno = carro.Fkduenno;
            existing.Chocado = carro.Chocado ?? existing.Chocado;
            existing.ValorFiscal = carro.ValorFiscal;

            _context.Carros.Update(existing);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

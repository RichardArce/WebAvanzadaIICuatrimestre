using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAvanzadaIICuatrimestre.DAL.Data;

namespace WebAvanzadaIICuatrimestre.DAL.Repositorios.Duenno
{
    public class DuennoRepositorio : IDuennoRepositorio
    {
        private readonly ApplicationDbContext _context;

        public DuennoRepositorio(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateDuenno(Entidades.Duenno Duenno)
        {
            if (Duenno == null) return false;

            Duenno.Telefonos = Duenno.Telefonos
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            await _context.Duennos.AddAsync(Duenno);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDuenno(int id)
        {
            var entity = await _context.Duennos.FindAsync(id);
            if (entity == null) return false;

            _context.Duennos.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Entidades.Duenno?> GetDuennoById(int id)
        {
            return await _context.Duennos
                .Include(d => d.Carros)
                .Include(d => d.Telefonos)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<List<Entidades.Duenno>> GetDuennos()
        {
            return await _context.Duennos
                .Include(d => d.Carros)
                .Include(d => d.Telefonos)
                .ToListAsync();
        }

        public async Task<bool> UpdateDuenno(Entidades.Duenno Duenno)
        {
            if (Duenno == null) return false;

            var existing = await _context.Duennos.Include(d => d.Telefonos).FirstOrDefaultAsync(d => d.Id == Duenno.Id);
            if (existing == null) return false;
            existing.Nombre = Duenno.Nombre ?? existing.Nombre;
            existing.Edad = Duenno.Edad;
            existing.Apellido1 = Duenno.Apellido1 ?? existing.Apellido1;
            existing.Apellido2 = Duenno.Apellido2 ?? existing.Apellido2;

            // Reemplaza la lista completa de telefonos con la enviada desde el formulario
            _context.Telefonos.RemoveRange(existing.Telefonos);
            existing.Telefonos = (Duenno.Telefonos ?? new List<Entidades.Telefono>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .Select(t => new Entidades.Telefono { Numero = t.Numero, Fkduenno = existing.Id })
                .ToList();

            _context.Duennos.Update(existing);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

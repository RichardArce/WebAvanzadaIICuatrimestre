using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.DAL.Entidades;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Duenno
{
    public class DuennoServicio : IDuennoServicio
    {
        private readonly IMapper _mapper;
        private readonly IRepositorioGenerico<DAL.Entidades.Duenno> _repositorioGenerico;

        public DuennoServicio(IMapper mapper, IRepositorioGenerico<DAL.Entidades.Duenno> repo)
        {
            _mapper = mapper;
            _repositorioGenerico = repo;
        }

        public async Task<Respuesta<DuennoDto>> CreateDuenno(DuennoDto duenno)
        {
            var respuesta = new Respuesta<DuennoDto>();

            if (duenno == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Duenno inválido";
                respuesta.codigo = 400;
                return respuesta;
            }

            if(duenno.Edad< 18)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "El Duenno no puede ser menor a 18 años";
                respuesta.codigo = 400;
                return respuesta;
            }

            duenno.Telefonos = (duenno.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            var entity = _mapper.Map<DAL.Entidades.Duenno>(duenno);
            _repositorioGenerico.AgregarAsync(entity);
            if (!await _repositorioGenerico.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo crear el dueño";
                respuesta.codigo = 500;
                return respuesta;
            }

            respuesta.Dato = duenno;
            return respuesta;
        }

        public async Task<Respuesta<DuennoDto>> DeleteDuenno(int id)
        {
            var respuesta = new Respuesta<DuennoDto>();
            _repositorioGenerico.EliminarAsync(id);
            if (!await _repositorioGenerico.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo eliminar el dueño";
                respuesta.codigo = 404;
            }
            return respuesta;
        }

        public async Task<Respuesta<DuennoDto?>> GetDuennoById(int id)
        {
            var respuesta = new Respuesta<DuennoDto?>();
            var entity = await _repositorioGenerico.ObtenerPorIdAsync(id, asNoTracking: true, d => d.Carros, d => d.Telefonos);
            if (entity == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Dueño no encontrado";
                respuesta.codigo = 404;
                respuesta.Dato = null;
                return respuesta;
            }

            respuesta.Dato = _mapper.Map<DuennoDto>(entity);
            return respuesta;
        }

        public async Task<Respuesta<List<DuennoDto>>> GetDuennos()
        {
            var respuesta = new Respuesta<List<DuennoDto>>();
            var list = await _repositorioGenerico.ObtenerTodosAsync(asNoTracking: true, d => d.Carros, d => d.Telefonos);
            respuesta.Dato = _mapper.Map<List<DuennoDto>>(list);
            return respuesta;
        }

        public async Task<Respuesta<DuennoDto>> UpdateDuenno(DuennoDto duenno)
        {
            var respuesta = new Respuesta<DuennoDto>();

            if (duenno == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Duenno inválido";
                respuesta.codigo = 400;
                return respuesta;
            }

            duenno.Telefonos = (duenno.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            var entity = _mapper.Map<DAL.Entidades.Duenno>(duenno);
            var existing = await _repositorioGenerico.BuscarAsync(d => d.Id == entity.Id, asNoTracking: false, d => d.Telefonos);
            if (existing == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el dueño";
                respuesta.codigo = 404;
                return respuesta;
            }

            existing.Nombre = entity.Nombre ?? existing.Nombre;
            existing.Edad = entity.Edad;
            existing.Apellido1 = entity.Apellido1 ?? existing.Apellido1;
            existing.Apellido2 = entity.Apellido2 ?? existing.Apellido2;
            existing.Telefonos = (entity.Telefonos ?? new List<DAL.Entidades.Telefono>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .Select(t => new DAL.Entidades.Telefono { Numero = t.Numero, Fkduenno = existing.Id })
                .ToList();

            _repositorioGenerico.ActualizarAsync(existing);
            if (!await _repositorioGenerico.SaveChangesAsync())
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el dueño";
                respuesta.codigo = 404;
                return respuesta;
            }

            respuesta.Dato = duenno;
            return respuesta;
        }
    }
}

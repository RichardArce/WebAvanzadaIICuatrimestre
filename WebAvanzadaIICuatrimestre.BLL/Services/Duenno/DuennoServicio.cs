using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Duenno;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Duenno
{
    public class DuennoServicio : IDuennoServicio
    {
        private readonly IDuennoRepositorio _duennoRepositorio;
        private readonly IMapper _mapper;

        public DuennoServicio(IDuennoRepositorio duennoRepositorio, IMapper mapper)
        {
            _duennoRepositorio = duennoRepositorio;
            _mapper = mapper;
        }

        public async Task<Respuesta<DuennoDto>> CreateDuenno(DuennoDto duenno)
        {
            var respuesta = new Respuesta<DuennoDto>();

            if (duenno == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Duenno inv�lido";
                respuesta.codigo = 400;
                return respuesta;
            }

            if(duenno.Edad< 18)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "El Due�o no p�ede ser menor a 18 a�os";
                respuesta.codigo = 400;
                return respuesta;
            }

            duenno.Telefonos = (duenno.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            var entity = _mapper.Map<DAL.Entidades.Duenno>(duenno);
            if (!await _duennoRepositorio.CreateDuenno(entity))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo crear el due�o";
                respuesta.codigo = 500;
                return respuesta;
            }

            respuesta.Dato = duenno;
            return respuesta;
        }

        public async Task<Respuesta<DuennoDto>> DeleteDuenno(int id)
        {
            var respuesta = new Respuesta<DuennoDto>();
            var deleted = await _duennoRepositorio.DeleteDuenno(id);
            if (!deleted)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo eliminar el due�o";
                respuesta.codigo = 404;
            }
            return respuesta;
        }

        public async Task<Respuesta<DuennoDto?>> GetDuennoById(int id)
        {
            var respuesta = new Respuesta<DuennoDto?>();
            var entity = await _duennoRepositorio.GetDuennoById(id);
            if (entity == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Due�o no encontrado";
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
            var list = await _duennoRepositorio.GetDuennos();
            respuesta.Dato = _mapper.Map<List<DuennoDto>>(list);
            return respuesta;
        }

        public async Task<Respuesta<DuennoDto>> UpdateDuenno(DuennoDto duenno)
        {
            var respuesta = new Respuesta<DuennoDto>();

            if (duenno == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Duenno inv�lido";
                respuesta.codigo = 400;
                return respuesta;
            }

            duenno.Telefonos = (duenno.Telefonos ?? new List<TelefonoDto>())
                .Where(t => !string.IsNullOrWhiteSpace(t.Numero))
                .ToList();

            var entity = _mapper.Map<DAL.Entidades.Duenno>(duenno);
            if (!await _duennoRepositorio.UpdateDuenno(entity))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el due�o";
                respuesta.codigo = 404;
                return respuesta;
            }

            respuesta.Dato = duenno;
            return respuesta;
        }
    }
}

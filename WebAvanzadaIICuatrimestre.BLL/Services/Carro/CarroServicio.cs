using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using WebAvanzadaIICuatrimestre.BLL.Dtos;
using WebAvanzadaIICuatrimestre.DAL.Entidades;
using WebAvanzadaIICuatrimestre.DAL.Repositorios.Carro;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Carro
{
    public class CarroServicio : ICarroServicio
    {
        //Inyeccion de dependencias del repositorio de carro

        private readonly ICarroRepositorio _carroRepositorio;
        private readonly IMapper _mapper;

        public CarroServicio(ICarroRepositorio carroRepositorio, IMapper mapper) // Inyeccion de dependencias del repositorio de carro
        {
            _carroRepositorio = carroRepositorio;   
            _mapper = mapper;
        }


        //Architectura por capas, cada capa tiene una responsabilidad, la capa de servicios se encarga de la logica de negocio, la capa de repositorios se encarga de la persistencia de datos, la capa de controladores se encarga de recibir las peticiones y enviar las respuestas, la capa de modelos se encarga de definir las entidades del sistema, la capa de dto se encarga de definir los objetos que se van a enviar y recibir entre capas.
        public async Task<Respuesta<CarroDto>> CreateCarro(CarroDto carro)
        {
            var respuesta = new Respuesta<CarroDto>();

            /*Bloques de validaciones*/

            if (carro.Marca != "Ferrari")
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Solo atendemos carros Ferrari";
                respuesta.codigo = 001;
                return respuesta;

            }

            if (carro.Chocado == 1)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No aceptamos carros chocados";
                respuesta.codigo = 002;
                return respuesta;

            }

            if (carro.ValorFiscal < 100000)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No aceptamos carros con un valor fiscal menor de 100000";
                respuesta.codigo = 003;
                return respuesta;
            }

            var dtoACarro = _mapper.Map<DAL.Entidades.Carro>(carro); // Mapeo de Dto a Modelo

            //Proceso
            if (!await _carroRepositorio.CreateCarro(dtoACarro))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo crear el carro";
                respuesta.codigo = 004;
                return respuesta;

            }

            return respuesta;

        }

        public async Task<Respuesta<CarroDto>> DeleteCarro(int id)
        {
            var respuesta = new Respuesta<CarroDto>();

            if (!await _carroRepositorio.DeleteCarro(id))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo eliminar el carro";
                respuesta.codigo = 404;
            }

            return respuesta;
        }

        public async Task<Respuesta<CarroDto?>> GetCarroById(int id)
        {
            var respuesta = new Respuesta<CarroDto?>();

            var entity = await _carroRepositorio.GetCarroById(id);
            if (entity == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Carro no encontrado";
                respuesta.codigo = 404;
                respuesta.Dato = null;
                return respuesta;
            }
            respuesta.Dato = _mapper.Map<CarroDto>(entity);
            return respuesta;
        }

        public async Task<Respuesta<List<CarroDto>>> GetCarros()
        {
            var respuesta = new Respuesta<List<CarroDto>>();

            var list = await _carroRepositorio.GetCarros();
            respuesta.Dato = _mapper.Map <List<CarroDto>>(list);

            return respuesta;
        }

        public async Task<Respuesta<CarroDto>> UpdateCarro(CarroDto carro)
        {
            var respuesta = new Respuesta<CarroDto>();

            if (carro == null)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Carro inválido";
                respuesta.codigo = 400;
                return respuesta;
            }

            // Validaciones de negocio (misma que Create)
            if (carro.Marca != "Ferrari")
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "Solo atendemos carros Ferrari";
                respuesta.codigo = 001;
                return respuesta;
            }

            if (carro.Chocado == 1)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No aceptamos carros chocados";
                respuesta.codigo = 002;
                return respuesta;
            }

            if (carro.ValorFiscal < 100000)
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No aceptamos carros con un valor fiscal menor de 100000";
                respuesta.codigo = 003;
                return respuesta;
            }

            var entity = _mapper.Map<DAL.Entidades.Carro>(carro);

            if (!await _carroRepositorio.UpdateCarro(entity))
            {
                respuesta.esCorrecto = false;
                respuesta.mensaje = "No se pudo actualizar el carro";
                respuesta.codigo = 404;
                return respuesta;
            }

            respuesta.Dato = carro;
            return respuesta;
        }
    }
}

/*LOGICA DE NEGOCIO 
 
    
    "Solo atendemos carros Ferraris"
    "Carros que no hayan sido chocados, (Check si ha sido chocado)"
    "Valor fiscal mayor a X monto"

    // Programar sin haber hecho reglas de negocio es como construir una casa sin planos, puede funcionar pero no es lo ideal, es importante definir las reglas de negocio antes de programar para tener una guía clara de lo que se quiere lograr y evitar problemas a futuro.
    
    

 */
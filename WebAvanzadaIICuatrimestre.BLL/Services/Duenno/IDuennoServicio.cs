using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAvanzadaIICuatrimestre.BLL.Dtos;

namespace WebAvanzadaIICuatrimestre.BLL.Services.Duenno
{
    public interface IDuennoServicio
    {
        Task<Respuesta<List<DuennoDto>>> GetDuennos();
        Task<Respuesta<DuennoDto?>> GetDuennoById(int id);
        Task<Respuesta<DuennoDto>> CreateDuenno(DuennoDto duenno);
        Task<Respuesta<DuennoDto>> UpdateDuenno(DuennoDto duenno);
        Task<Respuesta<DuennoDto>> DeleteDuenno(int id);
    }
}

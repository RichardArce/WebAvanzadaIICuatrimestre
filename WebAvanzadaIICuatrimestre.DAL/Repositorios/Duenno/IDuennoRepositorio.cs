using System;
using System.Collections.Generic;
using System.Text;

namespace WebAvanzadaIICuatrimestre.DAL.Repositorios.Duenno
{
    public interface IDuennoRepositorio
    {
        Task<List<Entidades.Duenno>> GetDuennos();
        Task<Entidades.Duenno?> GetDuennoById(int id);
        Task<bool> CreateDuenno(Entidades.Duenno Duenno);
        Task<bool> UpdateDuenno(Entidades.Duenno Duenno);
        Task<bool> DeleteDuenno(int id);
    }
}

//CRUD
//Create
//Read
//Update
//Delete
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace WebAvanzadaIICuatrimestre.DAL.Repositorios.Generico
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task<List<T>> ObtenerTodosAsync(bool asNoTracking = true, params Expression<Func<T, object>>[] includes);
        Task<T?> ObtenerPorIdAsync(int id, bool asNoTracking = true, params Expression<Func<T, object>>[] includes);
        Task<T?> BuscarAsync(Expression<Func<T, bool>> predicate, bool asNoTracking = false, params Expression<Func<T, object>>[] includes);
        void AgregarAsync(T entity);
        void ActualizarAsync(T entity);
        void EliminarAsync(int id);

        Task<bool> SaveChangesAsync();
    }
}

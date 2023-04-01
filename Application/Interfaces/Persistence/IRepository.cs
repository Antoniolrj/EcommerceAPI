using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Persistence
{
    public interface IRepository<T>
    {
        T Add(T entity);
        T Update(T entity);
        bool Delete(int id);
        List<T> GetAll();
        T GetById(int id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INC.EntityFramework
{
    public interface IUowOfWork
    {
        IRepository<T> Repositories<T>() where T : class;

        void SaveChanges();
    }
}

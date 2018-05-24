using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INC.EntityFramework
{
    public interface IRepositoryContainer: IDisposable
    {
        IRepository<T> Get<T>() where T : class;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INC.EntityFrameworkCore
{
    public interface IRepositoryContainer: IDisposable
    {
        IRepository<T> Get<T>() where T : class;
    }
}

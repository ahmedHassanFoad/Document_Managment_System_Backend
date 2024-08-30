using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Core.Entities;
using Directory = DMS.Core.Entities.Directory;

namespace DMS.Core.Repository
{
    public interface IDirectoryRepository : IGenericRepository<Directory>
    {

        Task<IEnumerable<Directory>> GetDirectoriesByUserIdAsync(string userId);
    }
}




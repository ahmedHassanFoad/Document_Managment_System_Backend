using DMS.Core.Entities;
using DMS.Core.Repository;
using DMS.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Repository.Repository
{
    public class DirectoryRepository : GenericRepository<Core.Entities.Directory>, IDirectoryRepository
    {
        public DirectoryRepository(DMSDbContext context) : base(context)
        {
        }
        public bool DeleteDirectory(Core.Entities.Directory directory)
        {
            if (directory == null) return false;

            directory.IsDeleted = true; 

            _dbContext.Update(directory); 

            return SaveAsync(); 
        }
        public async Task<IEnumerable<Core.Entities.Directory>> GetDirectoriesByUserIdAsync(string userId)
        {
            return await _dbContext.Directories
                .Include(d => d.WorkSpace)
                .Where(d => d.WorkSpace.user.Id == userId)
                .ToListAsync();
        }

    }
}

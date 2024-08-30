using DMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Core.Services
{
    public interface IDirectoryService
    {
        Task<bool> CreateDirectory(Entities.Directory directory);

        Task<IEnumerable<Entities.Directory>> GetAllDirectories();

        Task<Entities.Directory> GetDirectoryById(int DirectoryId);

        Task<bool> UpdateDirectory(Entities.Directory DirectoryDetails);

        Task<bool> DeleteDirectory(int DirectoryId);
        Task<bool> MakeDirectoryPublic(int directoryId);
        Task<bool> MakeDirectoryPrivate(int directoryId);
        Task<IEnumerable<Entities.Directory>> GetAllNonDeletedAsync();
        Task<IEnumerable<Entities.Directory>> GetDirectoriesByUserIdAsync(string userId);
    }
}

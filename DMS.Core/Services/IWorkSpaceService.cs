using DMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Core.Services
{
    public interface IWorkSpaceService
    {
        Task<bool> CreateworkSpace(WorkSpace workSpace);

        Task<IEnumerable<WorkSpace>> GetAllWorkSpaces();

        Task<WorkSpace> GetWorkSpaceById(int workSpaceId);

        Task<bool> UpdateworkSpace(WorkSpace workSpaceDetails);

        Task<bool> DeleteworkSpace(int woekSpaceId);

    }
}

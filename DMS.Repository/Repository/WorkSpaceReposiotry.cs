using DMS.Core.Entities;
using DMS.Core.Repository;
using DMS.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Repository.Repository
{
    public class WorkSpaceReposiotry : GenericRepository<WorkSpace>, IWorkSpaceRepository
    {
        public WorkSpaceReposiotry(DMSDbContext context) : base(context)
        {
        }
    }
}

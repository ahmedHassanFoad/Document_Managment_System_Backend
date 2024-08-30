using DMS.Core;
using DMS.Core.Repository;
using DMS.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DMSDbContext _dbContext;

        public IDirectoryRepository Directories { get; }

        public IDocumentRepository Documents { get; }

        public IWorkSpaceRepository Workspaces { get; }
        public UnitOfWork(DMSDbContext dbContext, IDirectoryRepository DirectoryRepository
            , IDocumentRepository DocumentRepository, IWorkSpaceRepository WorkspaceRepository)
        {
            _dbContext= dbContext;
            Directories = DirectoryRepository;
            Documents = DocumentRepository;
            Workspaces = WorkspaceRepository;
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}

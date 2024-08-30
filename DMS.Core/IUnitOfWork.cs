using DMS.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IDirectoryRepository Directories { get; }
        IDocumentRepository Documents { get; }
        IWorkSpaceRepository Workspaces { get; }

        int Save(); // Asynchronous Save method
    }
}

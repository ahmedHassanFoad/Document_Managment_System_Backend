using DMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Core.Repository
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(string userId);
        Task<IEnumerable<Document>> GetDocumentsByDirectoryIdAsync(int directoryId);
        Task<IEnumerable<Document>> GetDocumentsByWorkSpaceIdAsync(int workSpaceId);
        Task<IEnumerable<Document>> GetAllNonDeletedAsync();
        public bool DeleteDocument(Document document);
        Task<int> CountDocumentsByUserAsync(string userId);
        
    }
}

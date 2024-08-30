using DMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Core.Repository;
using DMS.Repository.Data;
using Microsoft.EntityFrameworkCore;


namespace DMS.Repository.Repository
{
    public class DocumentRepository : GenericRepository<Document>, IDocumentRepository
    {
        public DocumentRepository(DMSDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Document>> GetDocumentsByDirectoryIdAsync(int directoryId)
        {
            return await _dbContext.Documents
                 .Include(d => d.Directory)
                 .Where(d => d.DirectoryId == directoryId)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(string userId)
        {
            return await _dbContext.Documents
               .Include(d => d.Directory)
               .ThenInclude(d => d.WorkSpace)
               .Where(d => d.Directory.WorkSpace.user.Id == userId)
               .ToListAsync();
        }

        public async Task<IEnumerable<Document>> GetDocumentsByWorkSpaceIdAsync(int workSpaceId)
        {
            return await _dbContext.Documents
                .Include(d => d.Directory)
                .ThenInclude(d => d.WorkSpace)
                .Where(d => d.Directory.WorkSpaceId == workSpaceId)
                .ToListAsync();
        }
        public bool DeleteDocument(Document document)
        {
            if (document == null) return false;

            document.IsDeleted = true;

            _dbContext.Update(document);

            return SaveAsync();
        }

        public async Task<IEnumerable<Document>> GetAllNonDeletedAsync()
        {
            return await _dbContext.Documents.Where(d => !d.IsDeleted).ToListAsync();
        }
        public async Task<int> CountDocumentsByUserAsync(string userId)
        {
            return await _dbContext.Documents.CountAsync(d => d.Directory.WorkSpace.user.Id == userId && !d.IsDeleted);
        }
    }
}

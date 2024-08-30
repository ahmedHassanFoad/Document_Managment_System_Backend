using DMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DMS.Core.Services
{
    public interface IDocumentService
    {
        Task<bool> CreateDocument(Document document);
        Task<IEnumerable<Document>> GetAllDocuments();

        Task<Document> GetDocumenteById(int DocumentId);

        Task<bool> UpdateDocument(Document DocumentDetails);

        Task<bool> DeleteDocument(int DocumentId);
        Task<string> SaveFileAsync(IFormFile file);
        Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(string userId);
        Task<IEnumerable<Document>> GetDocumentsByDirectoryIdAsync(int directoryId);
        Task<IEnumerable<Document>> GetDocumentsByWorkSpaceIdAsync(int workSpaceId);
        Task<IEnumerable<Document>> GetAllNonDeletedDocumentsAsync();
        Task<int> CountDocumentsByUserAsync(string userId);
        



    }
}

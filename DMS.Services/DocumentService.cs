using DMS.Core;
using DMS.Core.Entities;
using DMS.Core.Repository;
using DMS.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services
{
    public class DocumentService : IDocumentService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IDocumentRepository _documentRepository;
        private readonly string _uploadPath;
        private readonly IWebHostEnvironment _environment;
        public DocumentService(IConfiguration configuration, IUnitOfWork unitOfWork,
            IDocumentRepository documentRepository,
            IWebHostEnvironment environment)
        {
            _documentRepository = documentRepository;
            _uploadPath = configuration.GetValue<string>("UploadPath");
            _unitOfWork = unitOfWork;
            _environment = environment;
        }




        public async Task<bool> CreateDocument(Document document)
        {
            if (document != null)
            {
                await _unitOfWork.Documents.Add(document);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteDocument(int DocumentId)
        {
            if (DocumentId > 0)
            {
                var DocumentDetails = await _unitOfWork.Documents.GetById(DocumentId);
                if (DocumentDetails != null)
                {
                    DocumentDetails.IsDeleted= true;
                    _unitOfWork.Documents.Update(DocumentDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<Document>> GetAllDocuments()
        {
            var DocumentList = await _unitOfWork.Documents.GetAll();
            return DocumentList;
        }

        public async Task<Document> GetDocumenteById(int DocumentId)
        {
            if (DocumentId > 0)
            {
                var Document = await _unitOfWork.Documents.GetById(DocumentId);
                if (Document != null)
                {
                    return Document;
                }
            }
            return null;
        }

        public async Task<bool> UpdateDocument(Document DocumentDetails)
        {
            if (DocumentDetails != null)
            {
                var DocumentDet
                    = await _unitOfWork.Documents.GetById(DocumentDetails.Id);
                if (DocumentDet != null)
                {
                    DocumentDet.Name = DocumentDetails.Name;
                    DocumentDet.IsDeleted= DocumentDetails.IsDeleted;
                    DocumentDet.Version= DocumentDetails.Version;
                    DocumentDet.Date=DateTime.Now;

                    _unitOfWork.Documents.Update(DocumentDet);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            List<string> validExtensions = new List<string> { ".pdf", ".xml", ".doc", ".docx", ".ppt", ".jpg", ".png" };

            // Validate file extension
            string extension = Path.GetExtension(file.FileName).ToLower();
            if (!validExtensions.Contains(extension))
            {
                return "Extension not valid";
            }

            // Validate file size
            long size = file.Length;
            if (size > 10 * 1024 * 1024) // 10 MB
            {
                return "The maximum size is 10 MB";
            }

            
            string fileName = Path.GetFileNameWithoutExtension(file.FileName) + extension;
            string uploadPath = Path.Combine(_environment.WebRootPath, "Uploads");
            string fullPath = Path.Combine(uploadPath, fileName);

            // Ensure the directory exists
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream); // Use async version
            }

            return fileName;
        }
        public async Task<IEnumerable<Document>> GetDocumentsByUserIdAsync(string userId)
        {
            return await _unitOfWork.Documents.GetDocumentsByUserIdAsync(userId);
        }

        public async Task<IEnumerable<Document>> GetDocumentsByDirectoryIdAsync(int directoryId)
        {
            return await _unitOfWork.Documents.GetDocumentsByDirectoryIdAsync(directoryId);
        }

        public async Task<IEnumerable<Document>> GetDocumentsByWorkSpaceIdAsync(int workSpaceId)
        {
            return await _unitOfWork.Documents.GetDocumentsByWorkSpaceIdAsync(workSpaceId);
        }
        public async Task<IEnumerable<Document>> GetAllNonDeletedDocumentsAsync()
        {
            return await _unitOfWork.Documents.GetAllNonDeletedAsync();
        }


        public async Task<int> CountDocumentsByUserAsync(string userId)
        {
            return await _unitOfWork.Documents.CountDocumentsByUserAsync(userId);
        }

    }
}

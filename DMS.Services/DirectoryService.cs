using DMS.Core;
using DMS.Core.Entities;
using DMS.Core.Repository;
using DMS.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Services
{
    public class DirectoryService : IDirectoryService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IDirectoryRepository _directoryRepository;

        public DirectoryService(IUnitOfWork unitOfWork , IDirectoryRepository directoryRepository)
        {
            _unitOfWork = unitOfWork;
            _directoryRepository = directoryRepository;
        }

        public async Task<bool> CreateDirectory(Core.Entities.Directory directory)
        {
            if (directory != null)
            {
                // Attach related entities if necessary
                if (directory.WorkSpace != null && directory.WorkSpace.Id > 0)
                {
                    var workspace = await _unitOfWork.Workspaces.GetById(directory.WorkSpace.Id);
                    if (workspace == null)
                    {
                        return false; 
                    }
                    directory.WorkSpace = workspace;
                }

                await _unitOfWork.Directories.Add(directory);

                // Save changes and return the result
                var result = _unitOfWork.Save();
                return result > 0;
            }
            return false;
        }

      

        public async Task<IEnumerable<Core.Entities.Directory>> GetAllDirectories()
        {
            var DirectoriesList = await _unitOfWork.Directories.GetAll();
            return DirectoriesList;
        }

        public async Task<Core.Entities.Directory> GetDirectoryById(int DirectoryId)
        {
            if (DirectoryId > 0)
            {
                var directory = await _unitOfWork.Directories.GetById(DirectoryId);
                if (directory != null)
                {
                    return directory;
                }
            }
            return null;
        }

        public async Task<bool> UpdateDirectory(Core.Entities.Directory DirectoryDetails)
        {
            if (DirectoryDetails != null)
            {
                var DirectoryDet
                    = await _unitOfWork.Directories.GetById(DirectoryDetails.Id);
                if (DirectoryDet != null)
                {
                    DirectoryDet.Name = DirectoryDetails.Name;
                    DirectoryDet.IsDeleted= DirectoryDetails.IsDeleted;
                    DirectoryDet.WorkSpaceId= DirectoryDetails.WorkSpaceId;
                    DirectoryDet.IsPublic= DirectoryDetails.IsPublic;

                    _unitOfWork.Directories.Update(DirectoryDet);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
        public async Task<bool> MakeDirectoryPublic(int directoryId)
        {
            var directory = await _unitOfWork.Directories.GetById(directoryId);
            if (directory == null || directory.IsDeleted)
                return false;

            directory.IsPublic = true;
            _unitOfWork.Directories.Update(directory);
            return _unitOfWork.Save() > 0;
        }

        public async Task<bool> MakeDirectoryPrivate(int directoryId)
        {
            var directory = await _unitOfWork.Directories.GetById(directoryId);
            if (directory == null || directory.IsDeleted)
                return false;

            directory.IsPublic = false;
            _unitOfWork.Directories.Update(directory);
            return _unitOfWork.Save() > 0;
        }
        public async Task<IEnumerable<Core.Entities.Directory>> GetAllNonDeletedAsync()
        {
            var directories = await _unitOfWork.Directories.GetAll();
            return directories.Where(d => !d.IsDeleted).ToList();
        }

        public async Task<IEnumerable<Core.Entities.Directory>> GetDirectoriesByUserIdAsync(string userId)
        {
            return await _directoryRepository.GetDirectoriesByUserIdAsync(userId);
        }

        async Task<bool> IDirectoryService.DeleteDirectory(int DirectoryId)
        {
            if (DirectoryId > 0)
            {
                var DirDetails = await _unitOfWork.Directories.GetById(DirectoryId);
                if (DirDetails != null)
                {
                    DirDetails.IsDeleted = true;
                    _unitOfWork.Directories.Update(DirDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}

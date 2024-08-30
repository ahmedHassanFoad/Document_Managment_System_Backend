using DMS.Core;
using DMS.Core.Entities;
using DMS.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMS.Repository.Repository;
namespace DMS.Services
{
    public class WorkSpaceService : IWorkSpaceService
    {

        public IUnitOfWork _unitOfWork;

        public WorkSpaceService(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        public async Task<bool> CreateworkSpace(WorkSpace workSpace)
        {
            if (workSpace != null)
            {
                await _unitOfWork.Workspaces.Add(workSpace);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteworkSpace(int woekSpaceId)
        {
            if (woekSpaceId > 0)
            {
                var WorksSpaceDetails = await _unitOfWork.Workspaces.GetById(woekSpaceId);
                if (WorksSpaceDetails != null)
                {
                    _unitOfWork.Workspaces.Delete(WorksSpaceDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<WorkSpace>> GetAllWorkSpaces()
        {
            var WorSpacesList = await _unitOfWork.Workspaces.GetAll();
            return WorSpacesList;
        }

        public async Task<WorkSpace> GetWorkSpaceById(int workSpaceId)
        {
            if (workSpaceId > 0)
            {
                var WorkSpace = await _unitOfWork.Workspaces.GetById(workSpaceId);
                if (WorkSpace != null)
                {
                    return WorkSpace;
                }
            }
            return null;
        }

        public async Task<bool> UpdateworkSpace(WorkSpace workSpaceDetails)
        {
            if (workSpaceDetails != null)
            {
                var workSpaceDet
                    = await _unitOfWork.Workspaces.GetById(workSpaceDetails.Id);
                if (workSpaceDet != null)
                {
                    workSpaceDet.Name = workSpaceDetails.Name;
                   

                    _unitOfWork.Workspaces.Update(workSpaceDet);

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

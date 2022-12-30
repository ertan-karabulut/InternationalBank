using BusinessLayer.Dto;
using BusinessLayer.Dto.Account;
using BusinessLayer.Dto.Branch;
using CoreLayer.DataAccess.Concrete;
using CoreLayer.DataAccess.Concrete.DataRequest;

using CoreLayer.Utilities.Result.Concreate;
using EntityLayer.Models.EntityFremework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IBranchWorkFlow
    {
        Task<Result<List<BranchDto>>> BranchListAsync(bool IsActive = true);
        Result<List<BranchDto>> BranchList(bool IsActive = true);
    }
}

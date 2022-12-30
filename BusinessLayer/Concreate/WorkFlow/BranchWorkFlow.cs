using AutoMapper;
using BusinessLayer.Abstract;
using BusinessLayer.Concreate.Component;
using BusinessLayer.Dto.Branch;
using CoreLayer.Utilities.Aspect;
using CoreLayer.Utilities.Cache.Abstract;
using CoreLayer.Utilities.Cache.Concreate;

using CoreLayer.Utilities.Result.Concreate;
using DataAccessLayer.Abstract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate.WorkFlow
{
    public class BranchWorkFlow : BranchComponent, IBranchWorkFlow
    {
        #region Variables
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public BranchWorkFlow(IUnitOfWork unow, IMapper mapper) : base(unow)
        {
            this._mapper = mapper;
        }
        #endregion

        [CacheAspect]
        public async Task<Result<List<BranchDto>>> BranchListAsync(bool IsActive = true)
        {
            var result = new Result<List<BranchDto>>();
            var branchList = await base.BranchListComponentAsync(IsActive);
            if (branchList.ResultStatus)
            {
                result.ResultObje = this._mapper.Map<List<BranchDto>>(branchList.ResultObje);
                result.SetTrue();
            }

            return result;
        }
        [CacheAspect]
        public Result<List<BranchDto>> BranchList(bool IsActive = true)
        {
            var result = new Result<List<BranchDto>>();
            var branchList = base.BranchListComponent(IsActive);
            if (branchList.ResultStatus)
            {
                result.ResultObje = this._mapper.Map<List<BranchDto>>(branchList.ResultObje);
                result.SetTrue();
            }

            return result;
        }
    }
}

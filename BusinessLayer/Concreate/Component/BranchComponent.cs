using CoreLayer.BusinessLayer.Concreate.Component;
using CoreLayer.Utilities.Result.Concreate;
using DataAccessLayer.Abstract;
using EntityLayer.Models.EntityFremework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate.Component
{
    public class BranchComponent : ComponentBase
    {
        #region Variables
        protected readonly IUnitOfWork _unow;
        #endregion

        #region Constructor
        public BranchComponent(IUnitOfWork unow)
        {
            this._unow = unow;
        }
        #endregion

        #region protected method
        #region Async metod
        protected async Task<Result<List<Branch>>> BranchListComponentAsync(bool IsActive = true)
        {
            var result = new Result<List<Branch>>();
            var query = this._unow.BranchRepository.Get(x => x.IsActive == IsActive);
            query = query.Include(x => x.City).Include(x => x.District);

            var branchList = await query.ToListAsync();
            if (branchList.Count > 0)
            {
                result.ResultObje = branchList;
                result.SetTrue();
            }

            return result;
        }
        #endregion
        protected Result<List<Branch>> BranchListComponent(bool IsActive = true)
        {
            var result = new Result<List<Branch>>();
            var query = this._unow.BranchRepository.Get(x => x.IsActive == IsActive);
            query = query.Include(x => x.City).Include(x => x.District);

            var branchList = query.ToList();
            if (branchList.Count > 0)
            {
                result.ResultObje = branchList;
                result.SetTrue();
            }

            return result;
        }
        #endregion
    }
}

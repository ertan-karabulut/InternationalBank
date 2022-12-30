using CoreLayer.Utilities.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.DataAccess.Abstract
{
    public interface ILogRepository
    {
        void AddLog(ILogDto entity);
    }
}

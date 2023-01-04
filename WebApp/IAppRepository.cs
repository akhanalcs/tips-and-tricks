using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp
{
    public interface IAppRepository
    {
        IQueryable<Log> Logs { get; }
        void ClearLogs();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employee { get; }
        ILeaveRequestRepository LeaveRequest { get; }
        IMailRepository Email { get; }

        void Save();
    }
}

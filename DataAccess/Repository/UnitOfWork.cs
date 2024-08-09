using DataAccess.Data;
using DataAccess.Repository.IRepository;
using MailKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IEmployeeRepository Employee { get; private set; }
        public ILeaveRequestRepository LeaveRequest { get; private set; }
        public IMailRepository Email { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Employee = new EmployeeRepository(_db);
            LeaveRequest = new LeaveRequestRepository(_db);
            Email = new MailRepository();
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace DataAccess.Repository
{
    public class LeaveRequestRepository : Repository<LeaveRequest>, ILeaveRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public LeaveRequestRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public override void Add(LeaveRequest request) 
        {
            request.Duration = DateHelper.GetWorkdaysBetween(request.Start, request.End);
            _db.LeaveRequests.Add(request);
        }

        public void Update(LeaveRequest request)
        {
            request.Duration = DateHelper.GetWorkdaysBetween(request.Start, request.End);
            _db.LeaveRequests.Update(request);
        }

    }
}

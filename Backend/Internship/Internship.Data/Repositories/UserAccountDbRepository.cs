using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Api.Models;

namespace Internship.Data.Repositories
{
    public class UserAccountDbRepository : IUserAccountRepository
    {
        private ApplicationDbContext _context;

        public UserAccountDbRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserAccount Get(string id)
        {
            return _context.Users.FirstOrDefault(s => s.Id == id);
        }
       
    }
}

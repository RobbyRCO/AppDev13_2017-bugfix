using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internship.Api.Models;

namespace Internship.Data.Repositories
{
    public interface IUserAccountRepository
    {
        UserAccount Get(string id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementMVC.Entities.Models;

namespace TaskManagementMVC.Repositories.IRepositories
{
    public interface IUserRepo
    {
        List<AspNetUser> GetAspNetUserTable();
    }
}

using FullMamba_Core.Models;
using FullMamba_Core.RepositoryAbstracts;
using FullMamba_Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullMamba_Data.RepositoryConcretes
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}

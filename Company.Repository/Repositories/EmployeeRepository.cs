using Company.Data.Contexts;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>,IEmployeeRepository
    {

        public EmployeeRepository(CompanyDbContext context) : base(context)
        {
        }
    }
}

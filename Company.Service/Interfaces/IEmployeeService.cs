using Company.Data.Entities;
using Company.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDto GetById(int id);
        IEnumerable<EmployeeDto> GetAll();
        void Add(EmployeeDto entity);
        void Update(EmployeeDto entity);
        void Delete(int id);
        public IEnumerable<EmployeeDto> GetEmployeesByName(string name);
    }
}

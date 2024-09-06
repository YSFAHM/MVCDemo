using Company.Data.Entities;
using Company.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IDepartmentService
    {
        DepartmentDto GetById(int id);
        IEnumerable<DepartmentDto> GetAll();
        void Add(DepartmentDto entity);
        void Update(DepartmentDto entity);
        void Delete(int id);
    }
}

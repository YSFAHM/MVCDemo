using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Dtos
{
    public class DepartmentDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<EmployeeDto>? Employees { get; set; }

    }
}

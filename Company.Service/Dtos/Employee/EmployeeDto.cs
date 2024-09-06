﻿using Company.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HirirngDate { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile Image {  get; set; }
        public DepartmentDto? Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}

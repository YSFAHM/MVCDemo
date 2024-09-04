using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public void Add(Employee entity)
        {
            var employee = new Employee
            {
                Address = entity.Address,
                Age = entity.Age,CreatedAt = DateTime.Now,
                DepartmentId = entity.DepartmentId,
                HirirngDate = entity.HirirngDate,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Salary = entity.Salary,
                Email = entity.Email,
            };
            _unitOfWork.EmployeeRepository.Add(employee);
            _unitOfWork.Complete();
        }

        public void Delete(Employee entity)
        {
            _unitOfWork.EmployeeRepository.Delete(entity);
            _unitOfWork.Complete();
        }

        public IEnumerable<Employee> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            return employees;
        }

        public Employee GetById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            return employee;
        }

        public void Update(Employee entity)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(entity.Id);
            employee.Address = entity.Address;
            employee.Age = entity.Age;

            employee.DepartmentId = entity.DepartmentId;
            employee.HirirngDate = entity.HirirngDate;
            employee.Name = entity.Name;
            employee.PhoneNumber = entity.PhoneNumber;
            employee.Salary = entity.Salary;
            employee.Email = entity.Email;
            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}

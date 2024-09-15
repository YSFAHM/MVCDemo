using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Service.Dtos;
using Company.Service.Helper;
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
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork,IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void Add(EmployeeDto employeeDto)
        {
            employeeDto.ImageUrl = DocumentSettings.UploadFile(employeeDto.Image, "Images");
            var mappedEmployee = _mapper.Map<Employee>(employeeDto);
            mappedEmployee.CreatedAt = DateTime.Now;

            _unitOfWork.EmployeeRepository.Add(mappedEmployee);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            DocumentSettings.DeleteFile(employee.ImageUrl, "images");
            _unitOfWork.EmployeeRepository.Delete(employee);
            _unitOfWork.Complete();
        }

        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll();
            var employeesDtos = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(employees);
            return employeesDtos;
        }

        public EmployeeDto GetById(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(id);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public IEnumerable<EmployeeDto> GetEmployeesByName(string name)
        {
            var employees = _unitOfWork.EmployeeRepository.GetEmployeesByName(name);
            var employeesDtos = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            return employeesDtos;
        }
        public void Update(EmployeeDto employeeDto)
        {
            var employee = _unitOfWork.EmployeeRepository.GetById(employeeDto.Id);
            if (employeeDto.Image is not null) {
                employeeDto.ImageUrl = DocumentSettings.UploadFile(employeeDto.Image, "Images");
                DocumentSettings.DeleteFile(employee.ImageUrl, "images");
                employee.ImageUrl = employeeDto.ImageUrl;
                
            }
            employee.Address = employeeDto.Address;
            employee.Age = employeeDto.Age;

            employee.DepartmentId = employeeDto.DepartmentId;
            employee.HirirngDate = employeeDto.HirirngDate;
            employee.Name = employeeDto.Name;
            employee.PhoneNumber = employeeDto.PhoneNumber;
            employee.Salary = employeeDto.Salary;
            employee.Email = employeeDto.Email;
            _unitOfWork.EmployeeRepository.Update(employee);
            _unitOfWork.Complete();
        }
    }
}

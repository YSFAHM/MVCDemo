using AutoMapper;
using Company.Data.Entities;
using Company.Repository.Interfaces;
using Company.Service.Dtos;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class DService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public void Add(DepartmentDto departmentDto)
        {
            var mappedDepartment = _mapper.Map<Department>(departmentDto);
            mappedDepartment.CreatedAt = DateTime.Now;
            _unitOfWork.DepartmentRepository.Add(mappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            _unitOfWork.DepartmentRepository.Delete(department);
            _unitOfWork.Complete();
        }

        public IEnumerable<DepartmentDto> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            var departmentsDtos = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentDto>>(departments);
            departmentsDtos = new List<DepartmentDto>();
            return departmentsDtos;
        }

        public DepartmentDto GetById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        public void Update(DepartmentDto departmentDto)
        {

            var department = _unitOfWork.DepartmentRepository.GetById(departmentDto.Id);
            department.Name = departmentDto.Name;
            department.Code = departmentDto.Code;
            _unitOfWork.DepartmentRepository.Update(department);

            _unitOfWork.Complete();
        }
    }
}

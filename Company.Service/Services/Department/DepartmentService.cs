
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using Company.Data.Entities;
using AutoMapper;
using Company.Service.Dtos;
namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
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

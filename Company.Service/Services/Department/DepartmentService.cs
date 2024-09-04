
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using Company.Data.Entities;
namespace Company.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public void Add(Department entity)
        {
            var mappedDepartment = new Department
            {
                Code = entity.Code,
                Name = entity.Name,
                CreatedAt = DateTime.Now,
            };
            _unitOfWork.DepartmentRepository.Add(mappedDepartment);
            _unitOfWork.Complete();
        }

        public void Delete(Department entity)
        {
            _unitOfWork.DepartmentRepository.Delete(entity);
            _unitOfWork.Complete();
        }

        public IEnumerable<Department> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments;
        }

        public Department GetById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            return department;
        }

        public void Update(Department entity)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(entity.Id);
            department.Name = entity.Name;
            department.Code = entity.Code;
            _unitOfWork.DepartmentRepository.Update(department);
            _unitOfWork.Complete();
        }

    }
}

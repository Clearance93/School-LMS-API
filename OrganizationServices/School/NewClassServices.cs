using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;
using System.Diagnostics;

namespace OrganizationServices.School
{
    public class NewClassServices : INewClassServicesInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;

        public NewClassServices(IMapper mapper,
                                IUnitOfWork unit)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Unit = unit ?? throw new ArgumentNullException(nameof(_Unit));
        }

        public async Task<bool> CreateNewClassAsync(NewClassDto dto)
        {
            try
            {
                var teacher = await _Unit.Teacher.GetByIdAsync(dto.TeacherId);
                var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);

                if (organization == null &&
                    teacher == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The organization Id: {dto.OrganizationId} and teacher id: {dto.TeacherId} does not exist");
                }

                var newClass = _Mapper.Map<NewClass>(dto);

                newClass.NewClassId = Guid.NewGuid();
                newClass.CreatedAt = DateTime.Now;
                newClass.UpdatedAt = DateTime.Now;
                newClass.IsActiveClass = true;

                await _Unit.NewClass.AddAsync(newClass);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<NewClassDto>> GetAllNewCreatedClassesAsync(Guid organizationId, Guid teacherId)
        {
            try
            {
                var allClasses = await _Unit.NewClass.GetAllClassesCreatedByTeacher(organizationId, teacherId);

                return _Mapper.Map<IEnumerable<NewClassDto>>(allClasses);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateClassAsync(Guid id, NewClassDto dto)
        {
            try
            {
                var classes = await _Unit.NewClass.GetByIdAsync(id);

                if (classes == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The id {id} does not exist");
                }

                if (dto.Capacity > 0)
                {
                    classes.Capacity = dto.Capacity;
                }

                if (!string.IsNullOrWhiteSpace(dto.ClassRoom))
                {
                    classes.ClassRoom = dto.ClassRoom;
                }

                if (!string.IsNullOrWhiteSpace(dto.Subject))
                {
                    classes.Subject = dto.Subject;
                }

                if (dto.IsActiveClass == true)
                {
                    classes.IsActiveClass = true;
                }

                if (dto.IsActiveClass == false)
                {
                    classes.IsActiveClass = false;
                }

                classes.UpdatedAt = DateTime.Now;

                _Unit.NewClass.Update(classes);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}

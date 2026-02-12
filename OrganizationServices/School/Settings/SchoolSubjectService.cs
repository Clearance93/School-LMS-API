using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto.Settings;
using OrganizationIInterface.IReporitory.Schools.Settings;
using OrganizationIInterface.IService.School.Settings;
using OrganizationModels.Model.Settings;

namespace OrganizationServices.School.Settings
{
    public class SchoolSubjectService : ISchoolSubjectServiceInterface
    {
        private readonly IUnitOfWork _Work;
        private readonly ILogger<SchoolSubjectService> _Logger;
        private readonly IMapper _Mapper;
        private readonly ISchoolSubjectRepositoryInterface _Subject;

        public SchoolSubjectService(IMapper mapper,
                                    ILogger<SchoolSubjectService> logger,
                                    IUnitOfWork work,
                                    ISchoolSubjectRepositoryInterface subject)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _Work = work ?? throw new ArgumentNullException(nameof(work));
            _Subject = subject ?? throw new ArgumentNullException(nameof(_Subject));
        }

        public async Task<bool> AddSchoolSubjectAsync(SchoolSubjectDto dto)
        {
            var subject = await _Subject.GetSubjectBySubjectNameAsyc(dto.GradeId, dto.SubjectName!);

            if (subject != null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The Subject: {dto.SubjectName} already exist");
            }

            var schoolSubject = _Mapper.Map<SchoolSubjects>(dto);

            schoolSubject.SubjectId = Guid.NewGuid();

            await _Work.SchoolSubject.AddAsync(schoolSubject);

            await _Work.SaveChangeAsync();

            return true;
        }

        public async Task<bool> UpdateSubjectAsync(Guid id, SchoolSubjectDto dto)
        {
            if (id == Guid.Empty)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The subject Id cannot be empty");
            }

            var subject =  await _Work.SchoolSubject.GetByIdAsync(id);

            _Mapper.Map(dto, subject);

            _Work.SchoolSubject.Update(subject!);

            await _Work.SaveChangeAsync();

            return true;
        }
    }
}

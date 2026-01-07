using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IService.Assignments;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class AssignmentServices : IAssignmentServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;
        private readonly ITeacherRepository _Teacher;

        public AssignmentServices(IMapper mapper,
                                  IUnitOfWork unit,
                                  ITeacherRepository teacher)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Teacher = teacher ?? throw new ArgumentNullException(nameof(_Teacher));
        }

        public async Task<bool> AddAssignmentGradesAsync(AssignmentGradesDto dto)
        {
            try
            {
                var assignmentsGrades = _Mapper.Map<AssignmentGrades>(dto);

                assignmentsGrades.AssignmentGradesId = Guid.NewGuid();
                assignmentsGrades.GradedDate = DateTime.Now;

                await _Unit.AssignmentGrades.AddAsync(assignmentsGrades);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> AssignmentsSubmissionAsync(AssignmentSubmissionDto dto)
        {
            try
            {
                var student = await _Unit.Student.GetByIdAsync(dto.StudentId);

                if (student == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The student with Id: {dto.StudentId} does not exist");
                }

                var assignment = await _Unit.Assignments.GetByIdAsync(dto.AssignmentId);

                if (assignment == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The assignment with the Id: {dto.AssignmentId} does not exists");
                }

                var submission = _Mapper.Map<AssignmentSubmission>(dto);

                submission.AssignmentSubmissionId = Guid.NewGuid();
                submission.SubmissionDate = DateTime.Now;

                await _Unit.AssignmentSubmission.AddAsync(submission);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> CreateAnAssignmentAsync(AssignmentDto dto)
        {
            try
            {
                var teacher = await _Teacher.GetByIdAsync(dto.TeacherId);
                var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);
                var grades = await _Unit.GradeStream.GetByIdAsync(dto.GradeStreamId);

                if (teacher == null &&
                    organization == null &&
                    grades == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"One or more values does not exist");
                }

                var assignment = _Mapper.Map<Assignment>(dto);

                assignment.AssignmentId = Guid.NewGuid();
                assignment.CreatedDate = DateTime.Now;

                await _Unit.Assignments.AddAsync(assignment);

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

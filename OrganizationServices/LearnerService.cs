using AutoMapper;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class LearnerService : ILearnerServiceInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;

        public LearnerService(IUnitOfWork unit,
                             IMapper mapper)
        {
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(_Mapper));
        }

        public async Task<bool> AddNewLearnerAsync(CreateLearnerDto dto)
        {
            var link = await _Unit.RegistrationLink.GetByIdAsync(dto.RegistrationLinkId);

            if (link != null)
            {
                link.UserCount++;

                if (link.UserCount <= link.MaxUsers)
                {
                    _Unit.RegistrationLink.Update(link);
                }
                else
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The link is broken due to max limit of {link.MaxUsers}");
                }
            }

            var existingUser = await _Unit.Learner.GetLearnerByEmailAsync(dto.LeanerEmail!);

            if (existingUser != null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The email {dto.LeanerEmail} already exist and it belongs to: \n {dto.FirstName} {dto.LastName}");
            }

            var user = _Mapper.Map<Learners>(dto);

            user.IsActive = true;
            user.IsDeleted = false;
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            await _Unit.Learner.AddAsync(user);

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<bool> DeleteLearnerAsync(string email)
        {
            var exitingUser = await _Unit.Learner.GetLearnerByEmailAsync(email);

            if (exitingUser == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException("The user does not exist or they are already deleted");
            }

            exitingUser.IsDeleted = true;
            exitingUser.IsActive = false;
            exitingUser.UpdatedAt = DateTime.Now;

            _Unit.Learner.Update(exitingUser);

            await _Unit.SaveChangeAsync();

            return true;
        }

        public async Task<IEnumerable<LearnersDto>> GetAllLearnersAsync(Guid organizationId)
        {
            var learners = await _Unit.Learner.GetAllLearnersByOrganizationId(organizationId);

            return _Mapper.Map<IEnumerable<LearnersDto>>(learners);
        }

        public async Task<LearnersDto> GetLearnerByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The email provided is either null or contain white spaces");
            }

            var checkExistance = await _Unit.Learner.GetLearnerByEmailAsync(email);

            if (checkExistance == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException("The learner does not exist");
            }

            return _Mapper.Map<LearnersDto>(checkExistance);
        }

        public async Task<bool> UpdateLearnerAsync(Guid id, UpdateLearnerDto dto)
        {
            var user = await _Unit.Learner.GetByIdAsync(id);

            if (user == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The user with id: {id} is not found");
            }

            _Mapper.Map(dto, user);

            user.UpdatedAt = DateTime.Now;

            _Unit.Learner.Update(user);

            await _Unit.SaveChangeAsync();

            return true;
        }
    }
}

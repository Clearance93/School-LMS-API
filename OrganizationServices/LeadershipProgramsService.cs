using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class LeadershipProgramsService : ILeadershipPropgramServirceInterface
    {
        private readonly IUnitOfWork _Work;
        private readonly IMapper _Mapper;

        public LeadershipProgramsService(IMapper mapper,
                                         IUnitOfWork work)
        {
            _Mapper = mapper;
            _Work = work ?? throw new ArgumentNullException(nameof(work));
        }

        public async Task<bool> CreateLeadershipProgramAsync(LeadershipProgramDto dto)
        {
            try
            {
                var leader = _Mapper.Map<LeadershipProgram>(dto);

                leader.ProgrameId = Guid.NewGuid();
                leader.CreatedAt = DateTime.Now;
                leader.IsActive = true;

                await _Work.LeadershipPropgram.AddAsync(leader);

                await _Work.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<LeadershipProgramDto>> GetAllLeadershipProgramAsync(Guid organizationId, Guid userId)
        {
            var allPrograms = await _Work.LeadershipPropgram.GetAllByOrganizationAndUserId(organizationId, userId);

            return _Mapper.Map<IEnumerable<LeadershipProgramDto>>(allPrograms);
        }
    }
}

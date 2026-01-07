using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class ActivitiesServices : IActitiesServicesInterface
    {
        private readonly IUnitOfWork _Unit;
        private readonly ILogger<ActivitiesServices> _Logger;
        private readonly IMapper _Mapper;

        public ActivitiesServices(IMapper mapper,
                                  ILogger<ActivitiesServices> logger,
                                  IUnitOfWork unit)
        {
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _Logger = logger ?? throw new ArgumentNullException(nameof(_Logger));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
        }

        public async Task<bool> CreateNewActivities(ActivitiesDto activities)
        {
            try
            {
                var organization = await _Unit.Organization.GetByIdAsync(activities.OrganizationId);

                if (organization == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The organization with the Id: {activities.OrganizationId} is invalid");
                }

                var userDetails = await _Unit.Activities.GetUserByEmail(activities.Email);

                if (userDetails == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"The user with the email: {activities.Email} is invalid");
                }

                var activity = _Mapper.Map<Activities>(activities);

                activity.CreatedAt = DateTime.Now;
                activity.ActivityId = Guid.NewGuid();
                activity.UserId = Guid.Parse(userDetails.Id);
                activity.FullName = $"{userDetails.FirstName} {userDetails.LastName}";

                await _Unit.Activities.AddAsync(activity);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<IEnumerable<ActivitiesDto>> GetAllActivitiesByOrganization(Guid organizationId)
        {
            try
            {
                var allActivities = await _Unit.Activities.GetAllActivitiesByOrganization(organizationId);

                return _Mapper.Map<IEnumerable<ActivitiesDto>>(allActivities);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateNewActivities(Guid activitityId, UpdateActivitiesDto activities)
        {
            try
            {
                var activity = await _Unit.Activities.GetByIdAsync(activitityId);

                if (activity == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"Activity not found with id: {activitityId}");
                }

                if (!string.IsNullOrEmpty(activity.ActionDescription))
                {
                    activity.ActionDescription = activities.ActionDescription;
                }

                if (!string.IsNullOrEmpty(activity.ActivityType))
                {
                    activity.ActivityType = activities.ActivityType;
                }

                _Unit.Activities.Update(activity);

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

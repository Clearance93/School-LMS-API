using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.CreateDto;
using OrganizationDTO.Dto.UpdateDto;
using OrganizationIInterface.IService;
using OrganizationModels.Model;

namespace OrganizationServices
{
    public class GuestService : IGuestServiceInterface
    {
        private readonly IUnitOfWork _Work;
        private readonly ILogger<GuestService> _Logger;
        private readonly IMapper _Mapper;

        public GuestService(IUnitOfWork work,
                            ILogger<GuestService> logger,
                            IMapper mapper)
        {
            _Work = work ?? throw new ArgumentNullException(nameof(work));
            _Logger = logger;
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AddNewGuestAsync(CreateGuestDto dto)
        {
            try
            {
                var existingGuest = await _Work.Guests.GetGuestByEmailAsync(dto.GuestEmail!);

                if (existingGuest == null)
                {
                    throw new Exception($"User with this email: {dto.GuestEmail} had not being found");
                }

                existingGuest.IsDeleted = false;
                existingGuest.IsActive = true;
                existingGuest.CreatedAt = DateTime.Now;
                existingGuest.UpdatedAt = DateTime.Now;

                await _Work.Guests.AddAsync(existingGuest);

                await _Work.SaveChangeAsync();

                return true;


            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to add a new guest: {exception.Message}");

                throw;
            }
        }

        public async Task<bool> DeleteGuestAsync(string email)
        {
            try
            {
                var existingUser = await _Work.Guests.GetGuestByEmailAsync(email);

                if (existingUser == null)
                {
                    _Logger.LogInformation($"The email: {email} provided does not exist");
                }

                _Work.Guests.Delete(existingUser!);

                await _Work.SaveChangeAsync();

                return true;
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to remove/delete the user with the email: {exception.Message}");

                throw;
            }
        }

        public async Task<IEnumerable<GuestsDto>> GetAllGuestAsync()
        {
            var guests = await _Work.Guests.GetAllAsync();

            return _Mapper.Map<IEnumerable<GuestsDto>>(guests);
        }

        public async Task<GuestsDto> GetGuestAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    _Logger.LogWarning($"The Id: {id} cannot be empty or default");

                    throw new ArgumentException("Please provide the proper Id");
                }

                var guestUser = await _Work.Guests.GetByIdAsync(id);

                if (guestUser == null)
                {
                    _Logger.LogInformation($"Failed to get the guest{nameof(guestUser)}");
                }

                var guestDto = _Mapper.Map<GuestsDto>(guestUser);

                return guestDto;
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to fetch the guest with the id of {id}, exception: {exception.Message}");

                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> UpdateGuestAsync(Guid id, UpdateGuestDto dto)
        {
            var existingUser = await _Work.Guests.GetByIdAsync(id);

            if (existingUser == null)
            {
                _Logger.LogInformation($"The Id: {id} cannot be null");

                return false;
            }

            _Mapper.Map(dto, existingUser);

            existingUser.UpdatedAt = DateTime.Now;

            _Work.Guests.Update(existingUser);

            await _Work.SaveChangeAsync();

            return true;
        }
    }
}

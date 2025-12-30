using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.Paasword;
using OrganizationCore.UnitOfWork;
using OrganizationDTO;
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
        private readonly IUserServiceInterface _AddnewUser;
        private readonly IPasswordGenerationInterface _Password;

        public GuestService(IUnitOfWork work,
                            ILogger<GuestService> logger,
                            IMapper mapper,
                            IUserServiceInterface user,
                            IPasswordGenerationInterface password)
        {
            _Work = work ?? throw new ArgumentNullException(nameof(work));
            _Logger = logger;
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _AddnewUser = user ?? throw new ArgumentNullException(nameof(user));
            _Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public async Task<bool> AddNewGuestAsync(CreateGuestDto dto)
        {
            try
            {
                var link = await _Work.RegistrationLink.GetByIdAsync(dto.RegistrationLinkId);

                if (link != null)
                {
                    link.UserCount++;

                    if (link.UserCount <= link.MaxUsers)
                    {
                        _Work.RegistrationLink.Update(link);
                    }
                    else
                    {
                        throw new OrganizationCore.Exceptions.InvalidOperationException($"The link is broken due to max limit of {link.MaxUsers}");
                    }
                }

                var existingGuest = await _Work.Guests.GetGuestByEmailAsync(dto.GuestEmail!);

                if (existingGuest != null)
                {
                    throw new Exception($"User with this email: {dto.GuestEmail} had not being found");
                }

                if (dto.Password == null)
                {
                    var passwordGeneration = _Password.GeneratePasswordAsync(12);

                    var guestUser = new CreateUserDto
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Email = dto.GuestEmail,
                        ProfileImage = dto.GuestProfilePicture,
                        Password = passwordGeneration,
                        Role = "Guest"
                    };

                    await _AddnewUser.CreateUserAsync(guestUser);
                }

                var user = _Mapper.Map<Guests>(dto);

                user!.IsDeleted = false;
                user.IsActive = true;
                user.CreatedAt = DateTime.Now;
                user.UpdatedAt = DateTime.Now;

                await _Work.Guests.AddAsync(user);

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

        public async Task<IEnumerable<GuestsDto>> GetAllGuestAsync(Guid organizationId)
        {
            var guests = await _Work.Guests.GetAllOrganizationGuest(organizationId);

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

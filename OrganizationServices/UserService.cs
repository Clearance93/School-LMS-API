using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrganizationCore.Email_Sender;
using OrganizationCore.Exceptions;
using OrganizationCore.UnitOfWork;
using OrganizationDTO;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using OrganizationModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OrganizationServices
{
    public class UserService : IUserServiceInterface
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IMapper _Mapper;
        private readonly UserManager<ApplicationUser> _UserManager;
        private readonly RoleManager<IdentityRole> _RoleManager;
        private readonly IConfiguration _Configuration;
        private readonly string _ProductionBaseUrl;
        private readonly IUserEmailSenderInterface _UserEmailSender;

        public UserService(IUnitOfWork unitOfWork,
                           IMapper mapper,
                            UserManager<ApplicationUser> userManager,
                           RoleManager<IdentityRole> roleManager,
                           IConfiguration configuration,
                           IUserEmailSenderInterface userEmailSender)
        {
            _UnitOfWork = unitOfWork;
            _Mapper = mapper;
            _RoleManager = roleManager;
            _UserManager = userManager;
            _Configuration = configuration;
            _UserEmailSender = userEmailSender;
            _ProductionBaseUrl = _Configuration["AppSettings:ProductionBaseUrl"] ?? throw new ArgumentNullException(nameof(_ProductionBaseUrl)); 
        }

        public async Task<ResponseUserDto> CreateUserAsync(CreateUserDto dto)
        {
            var existingUser = await _UnitOfWork.Users.GetUserByEmailAsync(dto.Email!);

            if (existingUser != null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException("User with the given email already exists.");
            }

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            var user = _Mapper.Map<ApplicationUser>(dto);

            user.Password = passwordHasher.HashPassword(user, dto.Password!);

            user.Id = Guid.NewGuid().ToString();
            user.IsActive = true;
            user.IsDeleted = false;
            user.CreatedAt = DateTime.Now;
            user.UserName = dto.Email;
            user.UpdatedAt = DateTime.Now;
            user.UserName = dto.Email;

            var role = await _UnitOfWork.Users.GetUserRoles(dto.Role ?? "User");

            if (role == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException("Specified role does not exist.");
            }

            if (!await _RoleManager.RoleExistsAsync(role))
            {
                var newRole = new IdentityRole(role);

                await _RoleManager.CreateAsync(newRole);
            }

            var result = await _UserManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));

                throw new OrganizationCore.Exceptions.InvalidOperationException($"User creation failed: {errors}");
            }

            var token = await _UserManager.GenerateEmailConfirmationTokenAsync(user);

            var encodedToken = Uri.EscapeDataString(token);
            var encodedUserId = Uri.EscapeDataString(user.Id);

            var callbackUrl = $"{_ProductionBaseUrl}confirm-email?userId={encodedUserId}&token={encodedToken}";

            var emailData = new EmailSenderDto
            {
                CallBackUrl = callbackUrl,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = role,
                Password = dto.Password
            };

            if (role == "Student")
            {
                await _UserEmailSender.StudentEmailSender(emailData);

                var userWithToken = await UserRole(user, role);

                return userWithToken;
            }
            else 
            if (role == "Guest")
            {
                await _UserEmailSender.GuestEmailSender(emailData);

                var userWithToken = await UserRole(user, role);

                return userWithToken;
            }
            else if (role == "Teacher")
            {
                await _UserEmailSender.TeacherEmailSender(emailData);

                var userWithToken = await UserRole(user, role);

                return userWithToken;
            }
            else if (role == "Learner")
            {
                await _UserEmailSender.LearnerEmailSender(emailData);

                var userWithToken = await UserRole(user, role);

                return userWithToken;
            }
            else if (role == "StuffMember")
            {
                await _UserEmailSender.StuffMemberEmailSender(emailData);

                var userWithToken = await UserRole(user, role);

                return userWithToken;
            }
            else if (role == "Admin")
            {
                await _UserEmailSender.AdminEmailSender(emailData);

                var userWithToken = await UserRole(user, role);

                return userWithToken;
            }
            else
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException("Unsupported role specified.");
            }
        }

        private async Task<ResponseUserDto> UserRole(ApplicationUser user, string role)
        {
            var isInRole = await _UserManager.IsInRoleAsync(user, role);

            if (!isInRole)
            {
                var roleResult = await _UserManager.AddToRoleAsync(user, role);

                if (!roleResult.Succeeded)
                {
                    await _UserManager.DeleteAsync(user);

                    var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));

                    throw new OrganizationCore.Exceptions.InvalidOperationException($"Assigning role failed: {errors}");
                }
            }

            var userRoles = await _UserManager.GetRolesAsync(user);
            var userDto = _Mapper.Map<UserDto>(user);
            userDto.Roles = userRoles.ToList();

            var userWithToken = await GenerateTokenAsync(userDto);

            return userWithToken;
        }

        private async Task<ResponseUserDto> GenerateTokenAsync(UserDto userDto)
        {
            var jwtKey = _Configuration["Jwt:Key"];
            var jwtIssuer = _Configuration["Jwt:Issuer"];
            var jwtAudience = _Configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(jwtKey) ||
                string.IsNullOrWhiteSpace(jwtIssuer) ||
                string.IsNullOrWhiteSpace(jwtAudience))
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException("JWT configuration is missing.");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userDto.Email),
                new Claim(JwtRegisteredClaimNames.Email, userDto.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                new Claim("extension_userId", userDto.Id),
                new Claim("extension_firstName", userDto.FirstName ?? string.Empty),
                new Claim("extension_lastName", userDto.LastName ?? string.Empty)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds
            );

            return new ResponseUserDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                Email = userDto.Email
            };
        }

        public async Task<bool> DeactivateUserAsync(string userId)
        {
            var user = await _UnitOfWork.Users.GetUserByIdAsync(userId);

            if (user == null || user.IsDeleted)
            {
                return false;
            }

            user.IsActive = false;
            user.IsDeleted = true;
            user.UpdatedAt = DateTime.Now;

            _UnitOfWork.Users.Update(user);
            await  _UnitOfWork.SaveChangeAsync();

            return true;
        }

        public async Task<IEnumerable<UserDto>> GetAllActiveUsersAsync()
        {
            var users = await _UnitOfWork.Users.GetAllActiveUsersAsync();

            return _Mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _UnitOfWork.Users.GetUserByEmailAsync(email);

            return _Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(string userId, UpdateUserDto dto, string token = null!)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.");
            };

            var existingUser = await _UnitOfWork.Users.GetUserByIdAsync(userId);

            if (existingUser == null || existingUser.IsDeleted)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException("User not found.");
            }

            _Mapper.Map(dto, existingUser);

            existingUser.UpdatedAt = DateTime.Now;

            _UnitOfWork.Users.Update(existingUser);

            await _UnitOfWork.SaveChangeAsync();

            return _Mapper.Map<UserDto>(existingUser);
        }

        public async Task<ResponseUserDto> AuthenticateUserAsync(UserLoginDto dto)
        {
            var exisitingUser = await _UnitOfWork.Users.GetUserByEmailAsync(dto.Email);

            if (exisitingUser == null)
            {
                throw new AuthenticationException("Invalid email or password.");
            }

            var user = exisitingUser;

            if (!user.EmailConfirmed)
            {
                throw new EmailNotConfirmedException("Please confirm your email before logging in.");
            }

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            var hashedPassword = user.Password;

            var results = passwordHasher.VerifyHashedPassword(user, hashedPassword, dto.Password);

            if (results == PasswordVerificationResult.Failed)
            {
                user.AccessFailedCount++;

                if (user.AccessFailedCount >= 5)
                {
                    user.LockoutEnd = DateTimeOffset.Now.AddMinutes(15);

                    user.IsActive = false;

                    user.LockoutEnabled = true;

                    throw new AccountLockedException("Your account is blocked due to multiple failed login attempts. Please try again after 15 minutes. 😔");
                }

                _UnitOfWork.Users.Update(user); 

                await _UnitOfWork.SaveChangeAsync();
            }
            else
            {
                user.AccessFailedCount = 0;

                _UnitOfWork.Users.Update(user);

                await _UnitOfWork.SaveChangeAsync();

                return await GenerateTokenAsync(_Mapper.Map<UserDto>(user));
            }

            throw new AuthenticationException("Invalid email or password.");
        }

        public async Task<string> GeneratePasswordResetTOkenRepositoryAsnc(string email)
        {
            var user = await _UnitOfWork.Users.GetUserByEmailAsync(email);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            var passwordResertToken = Guid.NewGuid().ToString();

            var callbackUrl = $"{_ProductionBaseUrl}reset-password?token={passwordResertToken}&email={email}";

            var emailSender = new EmailSenderDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = email,
                CallBackUrl = callbackUrl,
            };

            user.PasswordResetToken = passwordResertToken;
            user.TokenExpiration = DateTime.Now.AddMinutes(30);

            _UnitOfWork.Users.Update(user);

            await _UnitOfWork.SaveChangeAsync();

            await _UserEmailSender.PasswordResetAsync(emailSender);

            return passwordResertToken;
        }

        public async Task<bool> EmailConfirmationAsync(string userId, string token)
        {
            var userExistance = await _UnitOfWork.Users.CheckEmailConfirmationAsync(userId);

            if (userExistance == null)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"The user this the Id: {userId} does not exist");
            }

            userExistance.EmailConfirmed= true;

            _UnitOfWork.Users.Update(userExistance);

            await _UnitOfWork.SaveChangeAsync();

            return true;
        }
    }
}

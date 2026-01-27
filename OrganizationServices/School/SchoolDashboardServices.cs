
using AutoMapper;
using Microsoft.Extensions.Logging;
using OrganizationCore.UnitOfWork;
using OrganizationDTO.Dto;
using OrganizationDTO.Dto.Communication;
using OrganizationIInterface.IReporitory;
using OrganizationIInterface.IReporitory.Schools;
using OrganizationIInterface.IService.School;
using OrganizationModels.Model;
using OrganizationModels.Model.Communication;

namespace OrganizationServices.School
{
    public class SchoolDashboardServices : ISchoolDashboardServiceInterface
    {
        private readonly ICommunicationMessageInterfaceRepository _Communication;
        private readonly IUnitOfWork _Unit;
        private readonly IMapper _Mapper;

        public SchoolDashboardServices(ICommunicationMessageInterfaceRepository communication,
                                       IUnitOfWork unit,
                                       IMapper mapper)
        {
            _Communication = communication ?? throw new ArgumentNullException(nameof(_Communication));
            _Unit = unit ?? throw new ArgumentNullException(nameof(unit));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> AddNewBroadCastMessageAsync(BroadcastMessageDto dto)
        {
            try
            {
                if (dto.OrganizationId == Guid.Empty)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException("The organization id is needed.");
                }
                else
                {
                    var organization = await _Unit.Organization.GetByIdAsync(dto.OrganizationId);

                    if (organization == null)
                    {
                        throw new OrganizationCore.Exceptions.InvalidOperationException($"The is no organization match with the provided Id: {dto.OrganizationId}");
                    }
                }

                var senderInfo = await GetUserInfoByEmailAsync(dto.SenderEmail);

                var broadcastMessage = _Mapper.Map<Message>(dto);

                broadcastMessage.MessageId = Guid.NewGuid();
                broadcastMessage.IsRead = false;
                broadcastMessage.IsDeleted = false;
                broadcastMessage.TimeStamp = DateTime.Now;
                broadcastMessage.SenderName = senderInfo.FullName;
                broadcastMessage.SenderId = senderInfo.UserId;

                await _Unit.Communication.AddAsync(broadcastMessage);

                await _Unit.SaveChangeAsync();

                return true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public async Task<bool> AddNewMessageCommunicationAsync(CreateMessageDto dto)
        {
            try
            {
                if (dto.OrganizationId == Guid.Empty)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException("The organization id is needed.");
                }

                var organization = await _Unit.Organization.GetOrganizationAsync(dto.OrganizationId);
                if (organization == null)
                {
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"There is no organization matching the provided Id: {dto.OrganizationId}");
                }

                var recipientInfo = await GetUserInfoByRoleAsync(dto.RecipientRole!, dto.RecipientId);
                var senderInfo = await GetUserInfoByEmailAsync(dto.SenderEmail);

                var message = _Mapper.Map<Message>(dto);
                message.MessageId = Guid.NewGuid();
                message.IsRead = false;
                message.IsDeleted = false;
                message.TimeStamp = DateTime.Now;
                message.RecipientName = recipientInfo.FullName;
                message.SenderName = senderInfo.FullName;
                message.SenderId = senderInfo.UserId;

                await _Unit.Communication.AddAsync(message);
                await _Unit.SaveChangeAsync();

                return true;
            }
            catch
            {
                throw;
            }
        }

        private class UserInfo
        {
            public Guid UserId { get; set; }
            public string FullName { get; set; } = string.Empty;
        }

        private async Task<UserInfo> GetUserInfoByRoleAsync(string role, Guid userId)
        {
            switch (role?.ToLower())
            {
                case "student":
                    var student = await _Unit.Student.GetByIdAsync(userId);
                    if (student == null)
                        throw new OrganizationCore.Exceptions.InvalidOperationException($"Student not found with id: {userId}");
                    return new UserInfo
                    {
                        UserId = student.StudentId,
                        FullName = $"{student.FirstName} {student.LastName}"
                    };

                case "admin":
                    var admin = await _Unit.Admin.GetByIdAsync(userId);
                    if (admin == null)
                        throw new OrganizationCore.Exceptions.InvalidOperationException($"Admin not found with id: {userId}");
                    return new UserInfo
                    {
                        UserId = admin.AdminId,
                        FullName = $"{admin.FirstName} {admin.LastName}"
                    };

                case "teacher":
                    var teacher = await _Unit.Teacher.GetByIdAsync(userId);
                    if (teacher == null)
                        throw new OrganizationCore.Exceptions.InvalidOperationException($"Teacher not found with id: {userId}");
                    return new UserInfo
                    {
                        UserId = teacher.TeacherId,
                        FullName = $"{teacher.FirstName} {teacher.LastName}"
                    };

                case "stuff":
                case "staff":
                    var staffMember = await _Unit.StuffMember.GetByIdAsync(userId);
                    if (staffMember == null)
                        throw new OrganizationCore.Exceptions.InvalidOperationException($"Staff member not found with id: {userId}");
                    return new UserInfo
                    {
                        UserId = staffMember.StuffMemberId,
                        FullName = $"{staffMember.FirstName} {staffMember.LastName}"
                    };

                case "guest":
                    var guest = await _Unit.Guests.GetByIdAsync(userId);
                    if (guest == null)
                        throw new OrganizationCore.Exceptions.InvalidOperationException($"Guest not found with id: {userId}");
                    return new UserInfo
                    {
                        UserId = guest.GuestId,
                        FullName = $"{guest.FirstName} {guest.LastName}"
                    };

                default:
                    throw new OrganizationCore.Exceptions.InvalidOperationException($"Invalid recipient role: {role}");
            }
        }

        private async Task<UserInfo> GetUserInfoByEmailAsync(string? email)
        {
            if (string.IsNullOrEmpty(email))
                return new UserInfo { UserId = Guid.Empty, FullName = string.Empty };

            var student = await _Unit.Student.GetStudentByEmailAsync(email);
            if (student != null)
            {
                return new UserInfo
                {
                    UserId = student.StudentId,
                    FullName = $"{student.FirstName} {student.LastName}"
                };
            }

            var teacher = await _Unit.Teacher.GetTeacherByEmailAsync(email);
            if (teacher != null)
            {
                return new UserInfo
                {
                    UserId = teacher.TeacherId,
                    FullName = $"{teacher.FirstName} {teacher.LastName}"
                };
            }

            var admin = await _Unit.Admin.GetAdminByEmail(email);
            if (admin != null)
            {
                return new UserInfo
                {
                    UserId = admin.AdminId,
                    FullName = $"{admin.FirstName} {admin.LastName}"
                };
            }

            var staffMember = await _Unit.StuffMember.GetStuffMemberByEmailAsync(email);
            if (staffMember != null)
            {
                return new UserInfo
                {
                    UserId = staffMember.StuffMemberId,
                    FullName = $"{staffMember.FirstName} {staffMember.LastName}"
                };
            }

            var guest = await _Unit.Guests.GetGuestByEmailAsync(email);
            if (guest != null)
            {
                return new UserInfo
                {
                    UserId = guest.GuestId,
                    FullName = $"{guest.FirstName} {guest.LastName}"
                };
            }

            return new UserInfo { UserId = Guid.Empty, FullName = string.Empty };
        }

        public async Task<SchoolDashboardStatsDto?> GetSchoolDashboardServiceAsync(Guid id)
        {
            return await _Unit.SchoolDashboard.GetSchoolDashboardRepositoryAsync(id);
        }

        public async Task<IEnumerable<MessagesDto>> GetAllMessagesByIds(Guid organizationId, Guid senderId)
        {
            try
            {
                if (organizationId != Guid.Empty &&
                    senderId != Guid.Empty)
                {
                    var organization = await _Unit.Organization.GetByIdAsync(organizationId);

                    if (organization == null)
                    {
                        throw new OrganizationCore.Exceptions.InvalidOperationException($"The id provided {organizationId} does not exist");
                    }

                    var messages = await _Unit.Communication.GetAllMessagesByIdsAsync(organizationId, senderId);

                    return _Mapper.Map<IEnumerable<MessagesDto>>(messages);
                }

                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<TeacherDashboardViewDto?> GetTeacherDashboardViewAsync(Guid organizationId, Guid teacherId)
        {
            return await _Unit.TeacherDashboard.GetTeacherDashboardAsync(organizationId, teacherId);
        }
    }
}

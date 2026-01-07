namespace OrganizationIInterface.IReporitory
{
    public interface IAttendanceOverViewRepositoryInterface
    {
        Task RebuildDailyAttendanceOverViewAsync(Guid organizationId, Guid teacherId, DateTime date);
    }
}

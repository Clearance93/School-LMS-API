using Dapper;
using System.Data;

namespace OrganizationUtility.Sealed
{
    public class TimeOnlyTypeHandler : SqlMapper.TypeHandler<TimeOnly>
    {
        public override TimeOnly Parse(object value)
        {
            if (value is TimeSpan timeSpan)
            {
                return TimeOnly.FromTimeSpan(timeSpan);
            }

            if (value is DateTime dateTime)
            {
                return TimeOnly.FromDateTime(dateTime);
            }

            if (value is string str &&
                TimeOnly.TryParse(str, out var result))
            {
                return result;
            }

            return TimeOnly.MinValue;
        }

        public override void SetValue(IDbDataParameter parameter, TimeOnly value)
        {
            throw new NotImplementedException();
        }
    }
}

using Dapper;
using System.Data;

namespace OrganizationUtility.Sealed
{
    public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override DateOnly Parse(object value)
        {
            if (value is DateTime dateTime)
            {
                return DateOnly.FromDateTime(dateTime);
            }

            if (value is string str &&
                DateOnly.TryParse(str, out var date))
            {
                return date;
            }

            return DateOnly.MinValue;
        }

        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.Value = value.ToDateTime(TimeOnly.MinValue);

            parameter.DbType = DbType.Date;
        }
    }
}

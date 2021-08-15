using System.Collections.Generic;
using System.Linq;
using Dapper;
using Npgsql;

namespace Hangfire.WaitingAckState
{
    public class PostgreSqlRepository
    {
        public List<WaitingAckJobDto> GetWaitingAckJobs(string connectionString, string schemaName = "hangfire")
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            var result = connection.Query<WaitingAckJobDto>(
                    @$"SELECT j.""id"" as ""JobId"", j.""createdat"" as ""CreatedAt"", j.""expireat"" as ""ExpireAt"" , jp.""value"" as ""JobName"" FROM ""{schemaName}"".job as j inner join ""{schemaName}"".jobparameter as jp on j.id = jp.jobid and jp.name = 'RecurringJobId'  where ""statename"" = '{WaitingAckState.StateName}' order by j.""createdat"" desc;")
                .ToList();

            return result;
        }
        
        public static int GetWaitingAckJobCount(string connectionString, string schemaName = "hangfire")
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            var result = connection.QuerySingle<int>(
                @$"SELECT sum(""value"") FROM ""{schemaName}"".counter where ""key"" = '{WaitingAckState.Handler.STATE_STAT_KEY}';");

            return result;
        }
    }
}
using System.Threading.Tasks;
using Hangfire.Server;

namespace Hangfire.WaitingAckState
{
    public abstract class WaitingAckJobBase
    {
        public Task Execute(PerformContext context, object[] args)
        {
            context.SetJobParameter(WaitingAckStateFilter.PARAMETER_JOB, true);

            PerformJob(context, args);
            
            return Task.CompletedTask;
        }
        
        protected abstract Task PerformJob(PerformContext context, object[] args);
    }
}
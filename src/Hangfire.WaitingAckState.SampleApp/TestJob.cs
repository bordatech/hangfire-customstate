using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.Server;

namespace Hangfire.WaitingAckState.SampleApp
{
    public class TestJob : WaitingAckJobBase
    {
        protected override Task PerformJob(PerformContext context, object[] args)
        {
            Console.WriteLine("Job is executing...");
            Thread.Sleep(2000);

            return Task.CompletedTask;
        }
    }
}
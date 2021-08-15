using System;
using Hangfire.States;

namespace Hangfire.WaitingAckState
{
public static class WaitingAckJobClient
{
    public static void MarkAsSucceeded(string jobId, object result, long latency, long performanceDuration)
    {
        new BackgroundJobClient().ChangeState(jobId, new SucceededState(result, latency, performanceDuration));
    }

    public static void MarkAsFailed(string jobId, Exception exception)
    {
        new BackgroundJobClient().ChangeState(jobId, new FailedState(exception));
    }

    public static void MarkAsDeleted(string jobId)
    {
        new BackgroundJobClient().Delete(jobId);
    }
}
}
using Hangfire.States;

namespace Hangfire.WaitingAckState
{
    public class WaitingAckStateFilter : IElectStateFilter
    {
        public const string PARAMETER_JOB = "waitingAck";
        
        public void OnStateElection(ElectStateContext context)
        {
            if (context.CurrentState == ProcessingState.StateName 
                && context.CandidateState is SucceededState
                && context.GetJobParameter<bool>(PARAMETER_JOB))
            {
                context.CandidateState = new WaitingAckState();
            }
        }
    }
}
using System.Collections.Generic;
using Hangfire.States;
using Hangfire.Storage;

namespace Hangfire.WaitingAckState
{
    public class WaitingAckState : IState
    {
        public string Name => StateName;
        public string Reason => "Waiting for acknowledgement from an external service.";
        public bool IsFinal => false;
        public bool IgnoreJobLoadException => true;

        public static readonly string StateName = "WaitingAck";

        public Dictionary<string, string> SerializeData() => new Dictionary<string, string>();

        public class Handler : IStateHandler
        {
            public const string STATE_STAT_KEY = "stats:waitingack";
            
            public void Apply(ApplyStateContext context, IWriteOnlyTransaction transaction)
            {
                transaction.IncrementCounter(STATE_STAT_KEY);
            }

            public void Unapply(ApplyStateContext context, IWriteOnlyTransaction transaction)
            {
                transaction.DecrementCounter(STATE_STAT_KEY);
            }

            public string StateName => WaitingAckState.StateName;
        }
    }
}
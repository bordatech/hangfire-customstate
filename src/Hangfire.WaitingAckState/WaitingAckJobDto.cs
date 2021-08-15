using System;

namespace Hangfire.WaitingAckState
{
    public class WaitingAckJobDto
    {
        private string _jobName;
        
        public string JobId { get; set; }

        public string JobName
        {
            get => _jobName.Replace("\"",string.Empty); 
            set => _jobName = value;
        }

        public DateTime CreatedAt { get; set; }
    }
}
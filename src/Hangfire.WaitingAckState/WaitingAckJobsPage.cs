using System.Collections.Generic;
using Hangfire.Dashboard;
using Hangfire.Dashboard.Pages;

namespace Hangfire.WaitingAckState
{
    public class WaitingAckJobsPage : RazorPage
    {
        private readonly List<WaitingAckJobDto> _jobs;
        
        public WaitingAckJobsPage(string connectionString, string schemaName)
        {
            _jobs = new PostgreSqlRepository().GetWaitingAckJobs(connectionString, schemaName);
        }
        
        public override void Execute()
        {
            WriteLiteral("\r\n");
            Layout = new LayoutPage("WaitingAck");
            
            WriteLiteral("<div class=\"row\">\r\n");
            WriteLiteral("<div class=\"col-md-3\">\r\n</div>\r\n");
 
            
            WriteLiteral("<div class=\"col-md-9\">\r\n");
            WriteLiteral("<h1 class=\"page-header\">\r\nWaitingAckJobs</h1>\r\n");

            WriteLiteral("<div class=\"table-responsive\">\r\n");
            WriteLiteral("<table class=\"table\">\r\n");
            WriteLiteral("<thead>\r\n");
            WriteLiteral("<tr>\r\n");
            WriteLiteral("<th class=\"min-width\">Id</th>\r\n");
            WriteLiteral("<th>Job</th>\r\n");
            WriteLiteral("<th class=\"align-right\">CreatedAt (UTC)</th>\r\n");
            WriteLiteral("<th class=\"align-right\">Actions</th>\r\n");
            WriteLiteral("\r\n</tr>\r\n");
            WriteLiteral("\r\n</thead>\r\n");
            
            WriteLiteral("<tbody>\r\n");
            foreach (var job in _jobs)
            {
                WriteLiteral("<tr>\r\n");
                WriteLiteral($"<td class=\"min-width\"><a href='jobs/details/{job.JobId}'>#{job.JobId}</a></td>\r\n");
                WriteLiteral($"<td><a href='jobs/details/{job.JobId}'>{job.JobName}</a></td>\r\n");
                WriteLiteral($"<td class=\"align-right\">{job.CreatedAt}</td>\r\n");
                WriteLiteral($"<td class=\"align-right\"><a style='cursor:pointer' data-ajax='{@Url.To($"/waitingack/{job.JobId}/delete")}' data-confirm='Are you sure?'>Delete</a></td>\r\n");
                WriteLiteral("\r\n</tr>\r\n");
            }
            WriteLiteral("\r\n</table>\r\n");
            WriteLiteral("\r\n</div>\r\n");
 
            WriteLiteral("<div class=\"btn-toolbar\">\r\n");
            WriteLiteral("<div class=\"btn-toolbar-label\">\r\n");
            WriteLiteral($"Total items: {_jobs.Count}");
            WriteLiteral("\r\n</div>\r\n");
            WriteLiteral("\r\n</div>\r\n");
        }
    }
}
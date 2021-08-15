using Hangfire.Dashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.WaitingAckState
{
    public static class StartupExtensions
    {
        /// <summary>
        /// Adds WaitingAckState handler and filter to the Hangfire global configuration.
        /// </summary>
        public static void AddHangfireWaitingAckState(this IServiceCollection services)
        {
            GlobalConfiguration.Configuration.UseFilter(new WaitingAckStateFilter());
            GlobalStateHandlers.Handlers.Add(new WaitingAckState.Handler());
        }
        
        /// <summary>
        /// Adds WaitingAck Jobs page to the Hangfire Dashboard.
        /// </summary>
        /// <param name="connectionString">Connection string of Hangfire database.</param>
        /// <param name="schema">Schema name of Hangfire in given database.</param>
        public static void UseHangfireWaitingAckPage(
            this IApplicationBuilder app,
            string connectionString,
            string schema = "hangfire")
        {
            DashboardRoutes.Routes.AddRazorPage("/waitingack",
                page => new WaitingAckJobsPage(connectionString, schema));
            
            NavigationMenu.Items.Add(
                menu => new MenuItem("WaitingAck Jobs", menu.Url.To("/waitingack")));
            
            DashboardRoutes.Routes.AddCommand("/waitingack/(?<JobId>.+)/delete",
                context =>
                {
                    WaitingAckJobClient.MarkAsDeleted(context.UriMatch.Groups["JobId"].Value);
                    return true;
                });
        }
    }
}
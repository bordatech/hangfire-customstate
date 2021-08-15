using Microsoft.EntityFrameworkCore;

namespace Hangfire.WaitingAckState.SampleApp
{
    public class TestContext : DbContext
    {
        public TestContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
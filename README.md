# Hangfire.WaitingAckState
Hangfire is an open-source framework that helps you to create, process and manage your background jobs, i.e. operations you don't want to put in your request processing pipeline (https://www.hangfire.io).

Hangfire includes a state machine for background jobs. Each background job has a state and there are transitions between states. Some states are used as final states. The background job is completed when the state is final.In addition to predefined states, custom states may be needed in some cases. For example, three different services must be triggered by a scheduler. The real process will be done in these application services. After jobs are completed, services will send an acknowledgment to hangfire to maintain the state. Therefore, hangfire needs an intermediate state between processing and a succeeded (or failed) state. We called WaitingAck. The diagram below shows the basic flow.

![Hangfire state diagram with WaitingAck state](assets/diagram.png)

## Usage

To register state handler and filter:

```cs
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHangfireWaitingAckState();
    }
}
```

To add WaitingAck Jobs page to the Hangfire dashboard:

```cs
public class Startup
{
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseHangfireWaitingAckPage(Configuration.GetConnectionString("DefaultConnection"));
    }
}
```

Example recurring job with WaitingAck state:

```cs
public class TestJob : WaitingAckJobBase
{
    protected override Task PerformJob(PerformContext context, object[] args)
    {
        Console.WriteLine("Job is executing...");
        Thread.Sleep(2000);

        return Task.CompletedTask;
    }
}
```

To register recurring job in Startup.cs:
```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    RecurringJob.AddOrUpdate<TestJob>("testjob", job => job.Execute(null, null), Cron.Never);
}
```

## How to run sample application ?

Sample application needs postgresql database. [docker-compose.yml](docker-compose.yml) file has postgresql and pgadmin containers. To run docker-compose file:

```cs
docker-compose up -d
```

Then, postgresql will be ready at **localhost:5432**. Default username is **admin** and password is **admin**.

Pgadmin will be available at http://localhost:5050. Default username is **admin@admin.com** and password is **admin**.

After the infrastructure running, sample application can be run with the command:


```
cd src/Hangfire.WaitingAckState.SampleApp
dotnet run
```

Application runs at http://localhost:5000. Hangfire dashboard will be available at http://localhost:5000/hangfire

## Trigger Recurring Job

Open http://localhost:5000/hangfire/recurring page and select the job. Then, click *Trigger now* button.

After a few seconds, the job will be listed in the page http://localhost:5000/hangfire/waitingack.

There are two endpoints to complete job as *succeeded* or *failed*.

To complete as succeeded: http://localhost:5000/api/job/{jobId}/success

To complete as failed: http://localhost:5000/api/job/{jobId}/fail

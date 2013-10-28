using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using WebBackgrounder;

namespace AdvancedWebForms.Backgrounder
{
    public static class BackgrounderConfig
    {
        private static readonly Lazy<JobManager> _jobManager = new Lazy<JobManager>(CreateJobManager);

        public static void Start()
        {
            _jobManager.Value.Start();
        }

        public static void Stop()
        {
            _jobManager.Value.Dispose();
        }

        private static JobManager CreateJobManager()
        {
            var jobs = new []
            {
                new SampleJob(interval: TimeSpan.FromSeconds(5), timeout: TimeSpan.FromSeconds(20)),
                //new WorkItemCleanupJob(interval: TimeSpan.FromMinutes(1), timeout: TimeSpan.FromMinutes(5), dbContext: new WorkItemsContext())
            };

            var manager = new JobManager(jobs, new SingleServerJobCoordinator());

            manager.Fail(ex => Debug.WriteLine(String.Format("Error: {0}", ex), "WebBackgrounder"));

            return manager;
        }
    }
}
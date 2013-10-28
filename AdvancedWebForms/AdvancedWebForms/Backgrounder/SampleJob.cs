using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using WebBackgrounder;

namespace AdvancedWebForms.Backgrounder
{
    public class SampleJob : Job
    {
        public SampleJob(TimeSpan interval, TimeSpan timeout)
            : base("Sample Job", interval, timeout)
        {

        }

        public override Task Execute()
        {
            return new Task(() =>
            {
                Debug.WriteLine(String.Format("SampeJob started at {0}", DateTimeOffset.UtcNow), "WebBackgrounder");
                Thread.Sleep(3000);
                Debug.WriteLine(String.Format("SampeJob finished {0}", DateTimeOffset.UtcNow), "WebBackgrounder");
            });
        }
    }
}
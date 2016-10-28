using System;
using System.Linq;
using System.Management;
using System.Reactive.Linq;

namespace observable_win_process
{
    class ProcessObservationSource
    {
        public IObservable<ProcessObservation> AsObservable
        {
            get
            {
                var starts = "SELECT * FROM Win32_ProcessStartTrace".AsObservableWqlEventQuery().ToObservation("started");
                var stops = "SELECT * FROM Win32_ProcessStopTrace".AsObservableWqlEventQuery().ToObservation("stopped");
                return starts.Merge(stops);
            }
        }
    }

    static class ProcessObservationExtensions
    {
        public static IObservable<ProcessObservation> ToObservation(this IObservable<ManagementBaseObject> observable, string observation_name)
        {
            return observable.Select(o => new ProcessObservation
            {
                EventName = observation_name,
                ProcessName = o.Properties["ProcessName"].Value.ToString(),
                ParentProcessID = uint.Parse(o.Properties["ParentProcessID"].Value.ToString()),
                ProcessID = uint.Parse(o.Properties["ProcessID"].Value.ToString()),
                TimeCreated = DateTime.FromFileTime(long.Parse(o.Properties["TIME_CREATED"].Value.ToString())).ToUniversalTime()
            });
        }

        public static IObservable<ManagementBaseObject> AsObservableWqlEventQuery(this string query)
        {
            return Observable.Create<ManagementBaseObject>(observer =>
            {
                var watcher = new ManagementEventWatcher(new WqlEventQuery(query));
                watcher.EventArrived += (_, e) => observer.OnNext(e.NewEvent);
                watcher.Start();
                return () =>
                {
                    watcher.Stop();
                    watcher.Dispose();
                };
            });
        }
    }
}

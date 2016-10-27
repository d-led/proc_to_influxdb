using System;
using System.Reactive.Linq;

namespace observable_win_process
{
    static class Program
    {
        public static object Settings { get; private set; }

        static void Main()
        {
            try
            {
                RunForever();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                Environment.Exit(1);
            }
        }

        static void RunForever()
        {
            var settings = app.Default;

            var authenticate = !string.IsNullOrWhiteSpace(settings.InfluxUser);

            var influx = new InfluxDbProcessObserver(
                settings.InfluxUri,
                settings.InfluxDb,
                settings.InfluxMeasurement,
                authenticate ? settings.InfluxUser : null,
                authenticate ? settings.InfluxPassword : null);

            var observations = new ProcessObservationSource().AsObservable;

            observations.Subscribe(influx.Write);

            observations.Subscribe(ToConsole);

            observations.WaitForever();
        }

        static void ToConsole(ProcessObservation o)
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}: {o.EventName} {o.ProcessName} [parent:{o.ParentProcessID}->id:{o.ProcessID}]");
        }
    }
}

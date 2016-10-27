using InfluxDB.Collector;
using System;
using System.Collections.Generic;

namespace observable_win_process
{
    class InfluxDbProcessObserver
    {
        readonly string url;
        readonly string db;
        readonly string table;

        public InfluxDbProcessObserver(string url,string db, string table)
        {
            this.url = url;
            this.db = db;
            this.table = table;

            InitializeMeasurement();
        }

        public void Write(ProcessObservation o)
        {
            Metrics.Write(table,
                new Dictionary<string, object> {
                    { "process_id", o.ProcessID },
                    { "parent_process_id", o.ParentProcessID },
                    { "time_created", o.TimeCreated }
                },
                new Dictionary<string, string> {
                    { "process_name", o.ProcessName },
                    { "event_name", o.EventName }
                }
            );
        }

        void InitializeMeasurement()
        {
            //singleton for now
            Metrics.Collector = new CollectorConfiguration()
               .Tag.With("host", Environment.GetEnvironmentVariable("COMPUTERNAME"))
               .Batch.AtInterval(TimeSpan.FromSeconds(1))
               .WriteTo.InfluxDB(url, db)
               .CreateCollector();
        }
    }
}

using InfluxDB.LineProtocol.Client;
using InfluxDB.LineProtocol.Payload;
using System;
using System.Collections.Generic;

namespace observable_win_process
{
    class InfluxDbProcessObserver
    {
        readonly string table;
        readonly LineProtocolClient client;
        readonly string host = Environment.GetEnvironmentVariable("COMPUTERNAME");

        public InfluxDbProcessObserver(string url, string db, string table, string username, string password)
        {
            this.table = table;

            client = new LineProtocolClient(new Uri(url), db, username, password);
        }

        public void Write(ProcessObservation o)
        {
            var point = new LineProtocolPoint(table,
                new Dictionary<string, object> {
                    { "process_id", o.ProcessID },
                    { "parent_process_id", o.ParentProcessID }
                },
                new Dictionary<string, string> {
                    { "process_name", o.ProcessName },
                    { "event_name", o.EventName },
                    { "host", host }
                },
                o.TimeCreated
            );

            var payload = new LineProtocolPayload();
            payload.Add(point);

            var result = client.WriteAsync(payload).Result;
            if (!result.Success)
                Console.Error.WriteLine(result.ErrorMessage);
        }
    }
}

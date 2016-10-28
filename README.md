# proc_to_influxdb

> ever wondered what processes are being started and stopped on your machine?

observe windows process starts and stops via [InfluxDB](https://www.influxdata.com/time-series-platform/influxdb/), [influxdb-csharp](https://github.com/influxdata/influxdb-csharp), [WqlEventQuery](), with the code cleaning help of [Reactive Extensions](https://github.com/Reactive-Extensions/Rx.NET).

## query in InfluxDB

after

```
create database processes
```

and running the application (requires administration rights)

query, in the InfluxDB UI ([@localhost](http://localhost:8083/)):

```
> select * from processes..lifecycle order by time desc limit 10
name: lifecycle
---------------
time                event_name host  parent_process_id process_id process_name
1477664284913589760 stopped    PING2 0                 13888      dllhost.exe
1477664283913088768 stopped    PING2 0                 5344       dllhost.exe
1477664279910585088 stopped    PING2 0                 7660       nvtray.exe
1477664278912537600 stopped    PING2 0                 13624      nvtray.exe
1477664278912537344 started    PING2 12844             7660       nvtray.exe
1477664278911542016 started    PING2 9000              3736       conhost.exe
1477664278911542016 started    PING2 948               13888      dllhost.exe
1477664278911542016 started    PING2 12664             9000       observable_win_process.exe
1477664278911541760 stopped    PING2 0                 6028       consent.exe
1477664278910555648 started    PING2 948               5344       dllhost.exe
```

Example limiting the query to a time frame and a certain process:

```
select * from processes..lifecycle
   WHERE time > '2016-10-27T20:21:00Z' AND time < '2016-10-27T20:21:00Z' + 1m
   AND process_name = 'git.exe'
```

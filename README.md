# winproc_to_influxdb

observe windows process starts and stops via [InfluxDB](https://www.influxdata.com/time-series-platform/influxdb/), [influxdb-csharp](https://github.com/influxdata/influxdb-csharp), [WqlEventQuery](), with the code cleaning help of [Reactive Extensions](https://github.com/Reactive-Extensions/Rx.NET).

## query in InfluxDB

after

```
create database processes
```

and running the application (requires administration rights)

query:

```
> select * from processes..lifecycle
name: lifecycle
---------------
time                 event_name host    parent_process_id process_id process_name           time_created
1477598421130892032  stopped    PING2   0                 7220       dllhost.exe            131220720211128846
1477598421153912064  stopped    PING2   0                 11044      dllhost.exe            131220720211138838
1477598422114627840  started    PING2   5992              10676      SearchFilterHost.exe   131220720221136314
1477598422114627840  started    PING2   5992              12640      SearchProtocolHost.exe 131220720221136313
1477598434121334016  stopped    PING2   0                 14300      cmd.exe                131220720341203132
1477598434121334016  stopped    PING2   0                 7172       conhost.exe            131220720341208197
```

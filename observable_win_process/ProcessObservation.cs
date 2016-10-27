namespace observable_win_process
{
    public sealed class ProcessObservation
    {
        public string EventName { get; internal set; }
        public uint ParentProcessID { get; set; }
        public uint ProcessID { get; set; }
        public string ProcessName { get; internal set; }
        public ulong TimeCreated { get; set; }
    }
}

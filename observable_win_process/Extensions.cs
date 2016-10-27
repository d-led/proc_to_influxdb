using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace observable_win_process
{
    public static class Extensions
    {
        public static void WaitForever<T>(this IObservable<T> obs)
        {
            obs.FirstAsync(x => false).ToTask().Wait();
        }
    }
}

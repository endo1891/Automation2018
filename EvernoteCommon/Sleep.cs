using System.Threading;

namespace EvernoteCommon
{

    public static class Sleep
    {
        public static void HalfSecond()
        {
            Thread.Sleep(500);
        }

        public static void Seconds(int seconds)
        {
            Thread.Sleep(seconds * 1000);
        }
    }

}
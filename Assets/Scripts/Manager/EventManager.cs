using System;

namespace ZagZig.Manager
{
    public static class EventManager
    {
        public static Action<int> OnScoreChanged;

        public static void StartOnScoreChanged(int score)
        {
            OnScoreChanged?.Invoke(score);
        }
    }
}
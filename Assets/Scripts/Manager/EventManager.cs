using System;

namespace ZagZig.Manager
{
    public static class EventManager
    {
        public static Action<int> OnScoreChanged;
        public static Action OnGameStarted;
        public static Action OnGameEnded;

        public static void StartOnScoreChanged(int score)
        {
            OnScoreChanged?.Invoke(score);
        }

        public static void StartOnGameStarted()
        {
            OnGameStarted?.Invoke();
        }

        public static void StartOnGameEnded()
        {
            OnGameEnded?.Invoke();
        }
    }
}
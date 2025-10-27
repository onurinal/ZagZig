using UnityEngine;

namespace ZagZig.Manager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance { get; private set; }

        [Header("Infinite Level Settings")]
        [SerializeField] private Transform gemPrefab;
        [SerializeField] [Range(0, 100)] private int gemSpawnRate;

        [SerializeField] private LevelState currentLevelState = LevelState.Infinite;

        public LevelState CurrentLevelState => currentLevelState;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void SetLevelState(LevelState newState)
        {
            currentLevelState = newState;
        }

        public void StartLevel(PathManager pathManager)
        {
            if (currentLevelState == LevelState.Infinite)
            {
                ObjectPoolManager.Instance.InitializePoolForInfiniteLevel();
                pathManager.Initialize(gemPrefab, gemSpawnRate);
            }
            else
            {
                ObjectPoolManager.Instance.InitializePoolForNormalLevel();
            }
        }
    }
}
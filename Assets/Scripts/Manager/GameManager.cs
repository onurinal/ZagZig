using UnityEngine;
using ZagZig.Player;

namespace ZagZig.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private PathManager pathManager;

        public bool HasGameStarted { get; private set; } = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            InitializeManagers();
            LevelManager.Instance.StartLevel(pathManager);
        }

        private void InitializeManagers()
        {
            if (cameraManager == null)
            {
                Debug.LogError("CameraManager is null");
            }
            else
            {
                cameraManager.Initialize();
            }

            if (playerManager == null)
            {
                Debug.LogError("PlayerManager is null");
            }
            else
            {
                playerManager.Initialize();
            }
        }

        public void StartGame()
        {
            HasGameStarted = true;
            EventManager.StartOnGameStarted();
        }

        public void GameOver()
        {
            EventManager.StartOnGameEnded();
        }
    }
}
using UnityEngine;
using ZagZig.Player;

namespace ZagZig.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private PathManager pathManager;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private FollowPlayer followPlayer;

        public bool HasGameStarted { get; private set; } = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            pathManager.Initialize();
            followPlayer.Initialize();
            playerController.Initialize();
        }

        public void StartGame()
        {
            HasGameStarted = true;
            UIManager.Instance.AnimateStartPanelCoroutine();
        }

        public void GameOver()
        {
            UIManager.Instance.ShowGameOverPanel();
        }
    }
}
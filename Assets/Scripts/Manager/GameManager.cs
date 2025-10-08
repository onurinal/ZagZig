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
    }
}
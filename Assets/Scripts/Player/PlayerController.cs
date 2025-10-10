using UnityEngine;
using ZagZig.Manager;

namespace ZagZig.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private Transform ballHead;

        [Tooltip("If ball y < this value, the game ends")]
        [SerializeField] private float minHeightThreshold;

        public void Initialize()
        {
            playerMovement.Initialize(this, playerInput, playerProperties, ballHead);
        }

        private void Update()
        {
            if (ballHead.position.y < minHeightThreshold)
            {
                GameManager.Instance.GameOver();
                Destroy(gameObject);
            }
        }
    }
}
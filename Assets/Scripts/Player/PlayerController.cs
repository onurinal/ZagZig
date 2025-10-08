using UnityEngine;

namespace ZagZig.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Rigidbody playerRigidbody;
        [SerializeField] private Transform ballHead;

        public void Initialize()
        {
            playerMovement.Initialize(this, playerInput, playerProperties, ballHead);
        }
    }
}
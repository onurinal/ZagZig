using UnityEngine;

namespace ZagZig.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput playerInput;
        private PlayerProperties playerProperties;
        private PlayerController playerController;
        private Transform ballHead;

        private Vector3 moveDirection = Vector3.right;

        public void Initialize(PlayerController playerController, PlayerInput playerInput, PlayerProperties playerProperties, Transform ballHead)
        {
            this.playerController = playerController;
            this.playerInput = playerInput;
            this.playerProperties = playerProperties;
            this.ballHead = ballHead;

            playerInput.OnTap += HandleMovement;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            ballHead.position += moveDirection * (playerProperties.MoveSpeed * Time.deltaTime);
        }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
            playerInput.OnTap -= HandleMovement;
        }

        //When player click, tap or press space, the ball will change direction
        private void HandleMovement()
        {
            moveDirection = moveDirection == Vector3.right ? Vector3.forward : Vector3.right;
        }
    }
}
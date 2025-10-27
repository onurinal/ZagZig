using UnityEngine;
using System.Collections.Generic;
using ZagZig.Ball;
using ZagZig.Manager;

namespace ZagZig.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerInput playerInput;
        private PlayerProperties playerProperties;
        private PlayerManager playerManager;

        private BallHead ballHead;
        private List<BallBody> ballBodyParts;
        private List<Vector3> ballHeadPositions;

        private Vector3 moveDirection = Vector3.right;

        public void Initialize(PlayerManager playerManager, PlayerInput playerInput, PlayerProperties playerProperties, BallHead ballHead,
        List<BallBody> ballBodyParts, List<Vector3> ballHeadPositions)
        {
            this.playerManager = playerManager;
            this.playerInput = playerInput;
            this.playerProperties = playerProperties;
            this.ballHead = ballHead;
            this.ballBodyParts = ballBodyParts;
            this.ballHeadPositions = ballHeadPositions;

            playerInput.OnTap += HandleMovement;
        }

        private void Update()
        {
            if (!GameManager.Instance.HasGameStarted || !playerManager.CanMove) return;

            Move();

            if (LevelManager.Instance != null && LevelManager.Instance.CurrentLevelState == LevelState.Normal)
            {
                EnqueueBallHeadPosition();
                UpdateBallBodyPositions();
            }
        }

        private void Move()
        {
            if (ballHead == null) return;

            ballHead.transform.position += moveDirection * (playerProperties.MoveSpeed * Time.deltaTime);
        }

        private void EnqueueBallHeadPosition()
        {
            if (ballHead == null) return;

            ballHeadPositions.Add(ballHead.transform.position);

            var maxSize = (ballBodyParts.Count + 2) * playerProperties.Spacing;

            if (ballHeadPositions.Count > maxSize)
            {
                ballHeadPositions.Remove(ballHeadPositions[0]);
            }
        }

        private void UpdateBallBodyPositions()
        {
            if (ballBodyParts.Count <= 0) return;
            if (ballHead == null) return;

            for (int i = 0; i < ballBodyParts.Count; i++)
            {
                var index = (i) * playerProperties.Spacing;
                if (index < ballHeadPositions.Count)
                {
                    var startPosition = ballBodyParts[i].transform.position;
                    var targetPosition = ballHeadPositions[ballHeadPositions.Count - index - 1];
                    ballBodyParts[i].transform.position = Vector3.Lerp(startPosition, targetPosition, playerProperties.FollowSpeed * Time.deltaTime);
                }
            }
        }

        private void OnDisable()
        {
            playerInput.OnTap -= HandleMovement;
        }

        // When player click, tap or press space, ball will change direction
        private void HandleMovement()
        {
            if (!GameManager.Instance.HasGameStarted)
            {
                GameManager.Instance.StartGame();
                moveDirection = Vector3.right;
                return;
            }

            moveDirection = moveDirection == Vector3.right ? Vector3.forward : Vector3.right;
        }
    }
}
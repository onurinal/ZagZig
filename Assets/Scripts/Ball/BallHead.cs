using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZagZig.Manager;
using ZagZig.Player;

namespace ZagZig.Ball
{
    public class BallHead : MonoBehaviour
    {
        private PlayerManager playerManager;

        public void Initialize(PlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        private void OnCollisionEnter(Collision other)
        {
            var obstacle = other.gameObject.GetComponentInParent<Obstacle>();

            if (obstacle != null)
            {
                StartCoroutine(ProcessObstacleCollision(obstacle, obstacle.GetObstacleValue(), obstacle.GetReduceDelay()));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var collectable = other.GetComponentInParent<ICollectable>();

            if (collectable != null)
            {
                collectable.Collect();
            }
        }

        private IEnumerator ProcessObstacleCollision(Obstacle obstacle, int obstacleValue, float reduceDelay)
        {
            if (playerManager == null)
            {
                Debug.LogError("PlayerManager is null");
                yield break;
            }

            playerManager.StopMovement();

            yield return StartCoroutine(RemoveBallPartsRoutine(obstacle, obstacleValue, reduceDelay));

            playerManager.StartMovement();
        }

        private IEnumerator RemoveBallPartsRoutine(Obstacle obstacle, int obstacleValue, float reduceDelay)
        {
            for (int i = 0; i < obstacleValue; i++)
            {
                playerManager.RemoveBodyPart();
                obstacle.ReduceObstacleValue();
                yield return new WaitForSeconds(reduceDelay);
            }
        }
    }
}
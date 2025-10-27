using System;
using System.Collections;
using UnityEngine;
using ZagZig.Ball;
using ZagZig.Manager;

namespace ZagZig
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] private float fallDelay;
        [SerializeField] [Range(0, 5)] private float fallSpeed;
        [SerializeField] [Range(0, 10)] private float fallDistance;

        [SerializeField] private bool isStartGround = false;

        private IEnumerator fallingCoroutine;

        private void OnDisable()
        {
            StopFallTile();
        }

        private void OnCollisionExit(Collision other)
        {
            var ballHead = other.gameObject.GetComponentInParent<BallHead>();

            if (ballHead != null)
            {
                EventManager.OnScoreChanged?.Invoke(UIManager.Instance.TileScorePoint);
                StartCoroutine(FallTileRoutine());
            }
        }

        private IEnumerator FallTileRoutine()
        {
            yield return new WaitForSeconds(fallDelay);

            var startPosition = transform.position;
            var endPosition = startPosition + Vector3.down * fallDistance;

            var t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * fallSpeed;
                transform.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }

            HandleTileEnd();

            fallingCoroutine = null;
        }

        private void HandleTileEnd()
        {
            if (!isStartGround)
            {
                if (LevelManager.Instance.CurrentLevelState == LevelState.Infinite)
                {
                    ObjectPoolManager.Instance.RemoveTile(this);
                    PathManager.Instance.CreateNewTile();
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private void StopFallTile()
        {
            if (fallingCoroutine != null)
            {
                StopCoroutine(fallingCoroutine);
                fallingCoroutine = null;
            }
        }
    }
}
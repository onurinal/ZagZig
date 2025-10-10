using System.Collections;
using UnityEngine;
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

        private void OnDestroy()
        {
            StopFallTileCoroutine();
        }

        private IEnumerator FallTileCoroutine()
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

            if (!isStartGround)
            {
                ObjectPoolManager.Instance.RemoveTile(transform);
            }

            fallingCoroutine = null;
        }

        public IEnumerator StartFallTileCoroutine()
        {
            if (fallingCoroutine == null)
            {
                fallingCoroutine = FallTileCoroutine();
                yield return fallingCoroutine;
            }
        }

        private void StopFallTileCoroutine()
        {
            if (fallingCoroutine != null)
            {
                StopCoroutine(fallingCoroutine);
                fallingCoroutine = null;
            }
        }
    }
}
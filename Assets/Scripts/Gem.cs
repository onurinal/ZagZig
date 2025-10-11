using UnityEngine;
using ZagZig.Manager;

namespace ZagZig
{
    public class Gem : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            ObjectPoolManager.Instance.RemoveGem(transform);
            EventManager.OnScoreChanged?.Invoke(UIManager.Instance.GemScorePoint);
            UIManager.Instance.AnimateFloatingScoreText(UIManager.Instance.GemScorePoint, transform.position);
        }
    }
}
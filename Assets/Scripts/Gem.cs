using UnityEngine;
using ZagZig.Manager;

namespace ZagZig
{
    public class Gem : MonoBehaviour, ICollectable
    {
        public void Collect()
        {
            ObjectPoolManager.Instance.RemoveGem(this);
            EventManager.OnScoreChanged?.Invoke(UIManager.Instance.GemScorePoint);
            UIManager.Instance.AnimateFloatingScoreText(transform.position);
        }
    }
}
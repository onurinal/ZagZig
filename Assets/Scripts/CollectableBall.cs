using UnityEngine;
using ZagZig.Manager;

namespace ZagZig
{
    public class CollectableBall : MonoBehaviour, ICollectable
    {
        public void Collect()
        {
            var newBallBodyPart = ObjectPoolManager.Instance.GetBallBody();
            PlayerManager.Instance.AddBodyPart(newBallBodyPart);
            Destroy(gameObject);
        }
    }
}
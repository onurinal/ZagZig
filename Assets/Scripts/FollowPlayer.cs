using UnityEngine;

namespace ZagZig
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] private Transform ballHead;
        [SerializeField] [Range(0, 5)] private float followSpeed;

        private Vector3 offset;

        public void Initialize()
        {
            offset = transform.position - ballHead.position;
        }

        private void LateUpdate()
        {
            if (ballHead != null)
            {
                FollowBall();
            }
        }

        private void FollowBall()
        {
            transform.position = Vector3.Lerp(transform.position, ballHead.position + offset, followSpeed * Time.deltaTime);
        }
    }
}
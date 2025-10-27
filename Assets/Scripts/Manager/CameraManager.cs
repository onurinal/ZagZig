using UnityEngine;

namespace ZagZig.Manager
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        [SerializeField] [Range(0, 5)] private float followSpeed;

        private Vector3 offset;

        public void Initialize()
        {
            offset = transform.position - followTarget.position;
        }

        private void LateUpdate()
        {
            if (followTarget != null)
            {
                FollowBall();
            }
        }

        private void FollowBall()
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position + offset, followSpeed * Time.deltaTime);
        }
    }
}
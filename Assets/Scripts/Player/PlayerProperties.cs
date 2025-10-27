using UnityEngine;

namespace ZagZig.Player
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "ZagZig/Player/Player Properties")]
    public class PlayerProperties : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float followSpeed;
        [SerializeField] private int spacing = 10;

        public float MoveSpeed => moveSpeed;
        public float FollowSpeed => followSpeed;
        public int Spacing => spacing;
    }
}
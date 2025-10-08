using UnityEngine;

namespace ZagZig.Player
{
    [CreateAssetMenu(fileName = "PlayerProperties", menuName = "ZagZig/Player/Player Properties")]
    public class PlayerProperties : ScriptableObject
    {
        [SerializeField] private float moveSpeed;

        public float MoveSpeed => moveSpeed;
    }
}
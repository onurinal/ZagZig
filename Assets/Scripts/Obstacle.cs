using TMPro;
using UnityEngine;

namespace ZagZig
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private TextMeshPro obstacleText;
        [SerializeField] private int obstacleValue;
        [SerializeField] private float reduceDelay;

        private void Start()
        {
            obstacleText.text = obstacleValue.ToString();
        }

        public int GetObstacleValue()
        {
            return obstacleValue;
        }

        public float GetReduceDelay()
        {
            return reduceDelay;
        }

        public void ReduceObstacleValue()
        {
            obstacleValue--;
            obstacleText.text = obstacleValue.ToString();

            if (obstacleValue == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
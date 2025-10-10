using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZagZig.Manager
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance;

        private int currentSceneIndex;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void LoadSameScene()
        {
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
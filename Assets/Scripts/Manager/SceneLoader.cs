using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZagZig.Manager
{
    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        private int currentSceneIndex;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }

        public void LoadSameScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadLevelInfinite()
        {
            LevelManager.Instance?.SetLevelState(LevelState.Infinite);
            SceneManager.LoadScene("Level Infinite");
        }

        public void LoadLevel1()
        {
            LevelManager.Instance?.SetLevelState(LevelState.Normal);
            SceneManager.LoadScene("Level 1");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
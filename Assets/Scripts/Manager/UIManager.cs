using System.Collections;
using TMPro;
using UnityEngine;

namespace ZagZig.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("Game Over Settings")]
        [SerializeField] private Transform gameOverPanel;

        [Header("Start Panel Settings")]
        [SerializeField] private Transform startPanel;
        [SerializeField] private Transform tapToStartText;
        [SerializeField] private Transform gameTitleText;
        [Tooltip("adjusting speed title text to out of screen")]
        [SerializeField] private float speedTitleText;
        [Tooltip("adjusting distance title text to out of screen")]
        [SerializeField] private float upDistanceTitleText;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private int gemScorePoint;
        [SerializeField] private int tileScorePoint; // after ball pass the tile, increase score text
        private int currentScore = 0;

        private IEnumerator hideStartPanelCoroutine;
        public int GemScorePoint => gemScorePoint;
        public int TileScorePoint => tileScorePoint;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void OnEnable()
        {
            EventManager.OnScoreChanged += UpdateScoreText;
            EventManager.OnGameStarted += AnimateStartPanel;
            EventManager.OnGameEnded += ShowGameOverPanel;
        }

        private void OnDisable()
        {
            EventManager.OnScoreChanged -= UpdateScoreText;
            EventManager.OnGameStarted -= AnimateStartPanel;
            EventManager.OnGameEnded -= ShowGameOverPanel;
        }

        private void ShowGameOverPanel()
        {
            gameOverPanel.gameObject.SetActive(true);
        }

        private IEnumerator HideStartPanelRoutine()
        {
            tapToStartText.gameObject.SetActive(false);

            var startPosition = gameTitleText.position;
            var endPosition = startPosition + upDistanceTitleText * Vector3.up;
            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * speedTitleText;
                gameTitleText.position = Vector3.Lerp(startPosition, endPosition, t);
                yield return null;
            }

            startPanel.gameObject.SetActive(false);
            hideStartPanelCoroutine = null;
        }

        private void AnimateStartPanel()
        {
            if (hideStartPanelCoroutine == null)
            {
                hideStartPanelCoroutine = HideStartPanelRoutine();
                StartCoroutine(hideStartPanelCoroutine);
            }
        }

        public void StopAnimateStartPanel()
        {
            if (hideStartPanelCoroutine != null)
            {
                StopCoroutine(hideStartPanelCoroutine);
                hideStartPanelCoroutine = null;
            }
        }

        private void UpdateScoreText(int score)
        {
            currentScore += score;
            scoreText.text = currentScore.ToString();
        }

        public void AnimateFloatingScoreText(Vector3 position)
        {
            var floatingText = ObjectPoolManager.Instance.GetFloatingText();
            floatingText.transform.position = position;
            floatingText.text = $"+{gemScorePoint}";
            StartCoroutine(FloatingTextRoutine(floatingText));
        }

        private IEnumerator FloatingTextRoutine(TextMeshPro floatingText)
        {
            var currentPosition = floatingText.transform.position;
            var targetPosition = floatingText.transform.position + Vector3.up;
            var t = 0f;

            while (t < 1)
            {
                t += Time.deltaTime;
                floatingText.transform.position = Vector3.Lerp(currentPosition, targetPosition, t);
                yield return null;
            }

            if (ObjectPoolManager.Instance != null)
            {
                ObjectPoolManager.Instance.RemoveFloatingText(floatingText);
            }
        }
    }
}
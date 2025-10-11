using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ZagZig.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [Header("Game Over Settings")]
        [SerializeField] private Transform gameOverPanel;

        [Header("Start Panel Settings")]
        [SerializeField] private Transform startPanel;
        [SerializeField] private Transform tapToStartText;
        [SerializeField] private Transform gameTitleText;
        [Tooltip("adjusting speed title text to out of screen")]
        [SerializeField] private float speedTitleText;
        [SerializeField] private float upDistanceTitleText;

        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Transform floatingScoreTextPrefab;
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
            }
            else
            {
                Instance = this;
            }
        }

        private void OnEnable()
        {
            EventManager.OnScoreChanged += UpdateScoreText;
        }

        private void OnDisable()
        {
            EventManager.OnScoreChanged -= UpdateScoreText;
        }

        public void ShowGameOverPanel()
        {
            gameOverPanel.gameObject.SetActive(true);
        }

        private IEnumerator HideStartPanelCoroutine()
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

        public void AnimateStartPanelCoroutine()
        {
            if (hideStartPanelCoroutine == null)
            {
                hideStartPanelCoroutine = HideStartPanelCoroutine();
                StartCoroutine(hideStartPanelCoroutine);
            }
        }

        public void StopAnimateStartPanelCoroutine()
        {
            if (hideStartPanelCoroutine != null)
            {
                StopCoroutine(hideStartPanelCoroutine);
                hideStartPanelCoroutine = null;
            }
        }

        public void UpdateScoreText(int score)
        {
            currentScore += score;
            scoreText.text = currentScore.ToString();
        }

        public void AnimateFloatingScoreText(int score, Vector3 position)
        {
            var floatingText = ObjectPoolManager.Instance.GetFloatingText();
            floatingText.transform.position = position;
            floatingText.GetComponentInChildren<TextMeshPro>().text = $"+{score}";
            StartCoroutine(RemoveFloatingTextAfterSeconds(floatingText));
        }

        private IEnumerator RemoveFloatingTextAfterSeconds(Transform floatingText)
        {
            yield return new WaitForSeconds(2);
            ObjectPoolManager.Instance.RemoveFloatingText(floatingText);
        }
    }
}
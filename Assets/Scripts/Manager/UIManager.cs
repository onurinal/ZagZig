using System.Collections;
using UnityEngine;

namespace ZagZig.Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private Transform gameOverPanel;

        [Header("Start Panel Settings")]
        [SerializeField] private Transform startPanel;
        [SerializeField] private Transform tapToStartText;
        [SerializeField] private Transform gameTitleText;
        [Tooltip("adjusting speed title text to out of screen")]
        [SerializeField] private float speedTitleText;
        [SerializeField] private float upDistanceTitleText;
        private IEnumerator hideStartPanelCoroutine;

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
    }
}
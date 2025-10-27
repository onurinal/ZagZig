using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZagZig.Ball;
using ZagZig.Player;

namespace ZagZig.Manager
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance { get; private set; }

        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerProperties playerProperties;
        [SerializeField] private PlayerMovement playerMovement;

        [SerializeField] private BallHead ballHead;
        [SerializeField] private List<BallBody> ballBodyParts = new List<BallBody>();

        [Tooltip("If ball y < this value, the game ends")]
        [SerializeField] private float minHeightThreshold;

        private readonly List<Vector3> ballHeadPositions = new List<Vector3>();
        public bool CanMove { get; private set; } = true;
        private IEnumerator fallCheckCoroutine;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Initialize()
        {
            playerMovement.Initialize(this, playerInput, playerProperties, ballHead, ballBodyParts, ballHeadPositions);
            ballHead.Initialize(this);
        }

        private void OnEnable()
        {
            fallCheckCoroutine = CheckFallRoutine();
            StartCoroutine(fallCheckCoroutine);
        }

        private void OnDisable()
        {
            if (fallCheckCoroutine != null)
            {
                StopCoroutine(fallCheckCoroutine);
            }
        }

        private void HandlePlayerFall()
        {
            CanMove = false;

            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
        }

        public void AddBodyPart(BallBody newBodyPart)
        {
            ballBodyParts.Add(newBodyPart);
        }

        public void RemoveBodyPart()
        {
            if (ballBodyParts.Count > 0)
            {
                ObjectPoolManager.Instance.RemoveBallBody(ballBodyParts[ballBodyParts.Count - 1]);
                ballBodyParts.RemoveAt(ballBodyParts.Count - 1);
            }
            else
            {
                GameManager.Instance.GameOver();
                gameObject.SetActive(false);
            }
        }

        private IEnumerator CheckFallRoutine()
        {
            if (ballHead == null) yield break;

            while (true)
            {
                if (ballHead.transform.position.y < minHeightThreshold)
                {
                    HandlePlayerFall();
                    yield break;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        public void StopMovement()
        {
            CanMove = false;
        }

        public void StartMovement()
        {
            CanMove = true;
        }
    }
}
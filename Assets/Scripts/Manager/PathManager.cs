using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZagZig.Manager
{
    public class PathManager : MonoBehaviour
    {
        public static PathManager Instance { get; private set; }

        [Header("Tile Settings")]
        [SerializeField] private Transform firstTileParent;
        [SerializeField] private Transform tilePrefab;
        [SerializeField] private int tileCountAtStart;

        [Header("Gem Settings")]
        private Transform gemPrefab;
        private int gemSpawnRate;

        private Vector3 lastTilePosition;
        private Vector3 tileScale;

        private float tileHalfHeight;
        private float gemHalfHeight;

        private enum Direction
        {
            Right,
            Forward
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Initialize(Transform gemPrefab, int gemSpawnRate)
        {
            this.gemPrefab = gemPrefab;
            this.gemSpawnRate = gemSpawnRate;

            InitializeLastTilePosition();
            CachePrefabData();
            CreateInitialTiles();
        }

        private void CachePrefabData()
        {
            if (tilePrefab == null)
            {
                Debug.LogError("No tile prefab assigned");
            }

            if (gemPrefab == null)
            {
                Debug.LogError("No gem prefab assigned");
            }

            tileScale = tilePrefab.GetChild(0).localScale;

            tileHalfHeight = tilePrefab.GetComponentInChildren<Renderer>().bounds.size.y / 2;
            gemHalfHeight = gemPrefab.GetComponentInChildren<Renderer>().bounds.size.y / 2;
        }

        private void InitializeLastTilePosition()
        {
            if (firstTileParent.childCount > 0)
            {
                lastTilePosition = firstTileParent.GetChild(0).position;
            }
            else
            {
                lastTilePosition = Vector3.zero;
            }
        }

        private Direction GetNextPathDirection()
        {
            var directionSize = Enum.GetValues(typeof(Direction)).Length;

            var newDirection = Random.Range(0, directionSize);
            return (Direction)newDirection;
        }

        public void CreateNewTile()
        {
            var nextPathDirection = GetNextPathDirection();

            if (nextPathDirection == Direction.Right)
            {
                SpawnNewTile(Vector3.right * tileScale.x);
            }
            else
            {
                SpawnNewTile(Vector3.forward * tileScale.z);
            }

            TrySpawnGem();
        }

        private void SpawnNewTile(Vector3 offset)
        {
            var newTile = ObjectPoolManager.Instance.GetTile();
            newTile.transform.position = (offset) + lastTilePosition;
            lastTilePosition += offset;
        }

        private void TrySpawnGem()
        {
            var number = Random.Range(0, 100);
            if (number < gemSpawnRate)
            {
                var newGem = ObjectPoolManager.Instance.GetGem();
                var yOffset = gemHalfHeight + tileHalfHeight + 0.01f;
                newGem.transform.position = lastTilePosition + Vector3.up * yOffset;
            }
        }

        private void CreateInitialTiles()
        {
            for (int i = 0; i < tileCountAtStart; i++)
            {
                CreateNewTile();
            }
        }
    }
}
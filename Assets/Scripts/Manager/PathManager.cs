using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZagZig.Manager
{
    public class PathManager : MonoBehaviour
    {
        public static PathManager Instance;

        [SerializeField] private Transform tileParent;
        [SerializeField] private Transform tilePrefab;
        [SerializeField] private int tileCountAtStart;

        private Vector3 lastTilePosition;
        private Vector3 tileScale;

        private Transform gemPrefab;
        private int gemSpawnRate;

        private float currentTileBoundHalfSizeY;
        private float currentGemBoundHalfSizeY;

        private enum Direction
        {
            Right,
            Front
        }

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

        public void Initialize(Transform gemPrefab, int gemSpawnRate)
        {
            this.gemPrefab = gemPrefab;
            this.gemSpawnRate = gemSpawnRate;

            UpdateBounds();
            InitializeLastTilePosition();
            UpdateTileSize();
            CreateNewTilesAtStart();
        }

        private void InitializeLastTilePosition()
        {
            if (tileParent.childCount > 0)
            {
                lastTilePosition = tileParent.GetChild(0).position;
            }
            else
            {
                lastTilePosition = Vector3.zero;
            }
        }

        private void UpdateTileSize()
        {
            if (tilePrefab.childCount > 0)
            {
                tileScale = tilePrefab.GetChild(0).localScale;
            }
            else
            {
                Debug.LogError("No tile prefab found");
            }
        }

        private void UpdateBounds()
        {
            currentTileBoundHalfSizeY = tilePrefab.GetComponentInChildren<Renderer>().bounds.size.y / 2;
            currentGemBoundHalfSizeY = gemPrefab.GetComponentInChildren<Renderer>().bounds.size.y / 2;
        }

        private Direction GetNextPathDirection()
        {
            var size = Enum.GetValues(typeof(Direction)).Length;

            var newDirection = Random.Range(0, size);
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

            TryToSpawnGem();
        }

        private void SpawnNewTile(Vector3 moveVector)
        {
            var newPosition = (moveVector) + lastTilePosition;
            var newTile = ObjectPoolManager.Instance.GetTile();
            newTile.transform.position = newPosition;
            lastTilePosition = newPosition;
        }

        private void TryToSpawnGem()
        {
            var number = Random.Range(0, 100);
            if (number < gemSpawnRate)
            {
                var newGem = ObjectPoolManager.Instance.GetGem();
                var newGemPosition = lastTilePosition + new Vector3(0, currentGemBoundHalfSizeY + currentTileBoundHalfSizeY + 0.01f, 0);
                newGem.transform.position = newGemPosition;
            }
        }

        private void CreateNewTilesAtStart()
        {
            for (int i = 0; i < tileCountAtStart; i++)
            {
                CreateNewTile();
            }
        }
    }
}
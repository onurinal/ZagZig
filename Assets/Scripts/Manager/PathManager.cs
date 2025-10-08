using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZagZig.Manager
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] private Transform firstTile;
        [SerializeField] private Transform tilePrefab;

        private Vector3 lastPathPosition;
        private Vector3 tileSize;

        private enum Direction
        {
            Right,
            Front
        }

        public void Initialize()
        {
            lastPathPosition = firstTile.position;

            UpdateTileSize();
            CreateNewTiles();
        }

        private void UpdateTileSize()
        {
            if (tilePrefab.childCount > 0)
            {
                tileSize = tilePrefab.GetChild(0).localScale;
            }
            else
            {
                Debug.LogError("No tile prefab found");
            }
        }

        private Direction GetNextPathDirection()
        {
            var size = Enum.GetValues(typeof(Direction)).Length;

            var newDirection = Random.Range(0, size);
            return (Direction)newDirection;
        }

        private void CreateNewTile()
        {
            var nextPathDirection = GetNextPathDirection();

            if (nextPathDirection == Direction.Right)
            {
                SpawnNewPath(Vector3.right * tileSize.x);
            }
            else
            {
                SpawnNewPath(Vector3.forward * tileSize.z);
            }
        }

        private void SpawnNewPath(Vector3 moveVector)
        {
            var newPosition = (moveVector) + lastPathPosition;
            var newTile = ObjectPoolManager.Instance.GetTile();
            newTile.transform.position = newPosition;
            lastPathPosition = newPosition;
        }

        private void CreateNewTiles()
        {
            for (int i = 0; i < 15; i++)
            {
                CreateNewTile();
            }
        }
    }
}
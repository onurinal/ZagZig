using System.Collections.Generic;
using UnityEngine;

namespace ZagZig.Manager
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance;

        [SerializeField] private Transform pathParent;
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private int maxTileSize = 20;

        private readonly Queue<GameObject> pathQueue = new Queue<GameObject>();

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

            CreateNewTiles(maxTileSize);
        }

        private void CreateNewTiles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newTile = Instantiate(tilePrefab, pathParent);
                newTile.gameObject.SetActive(false);
                pathQueue.Enqueue(newTile);
            }
        }

        public GameObject GetTile()
        {
            if (pathQueue.Count <= 0)
            {
                CreateNewTiles(5);
            }

            var tile = pathQueue.Dequeue();
            tile.SetActive(true);
            return tile;
        }

        public void RemoveTile(GameObject tile)
        {
            tile.SetActive(false);
            pathQueue.Enqueue(tile);
        }
    }
}
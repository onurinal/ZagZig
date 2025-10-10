using System.Collections.Generic;
using UnityEngine;

namespace ZagZig.Manager
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance;

        [Header("Tile")]
        [SerializeField] private Transform tileParent;
        [SerializeField] private Transform tilePrefab;
        [SerializeField] private int maxTileSize = 30;

        [Header("Gem")]
        [SerializeField] private Transform gemParent;
        [SerializeField] private Transform gemPrefab;
        [SerializeField] private int maxGemSize = 12;

        private readonly Queue<Transform> tileQueue = new Queue<Transform>();
        private readonly Queue<Transform> gemQueue = new Queue<Transform>();

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
            CreateNewGems(maxGemSize);
        }

        private void CreateNewTiles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newTile = Instantiate(tilePrefab, tileParent);
                newTile.gameObject.SetActive(false);
                tileQueue.Enqueue(newTile);
            }
        }

        public Transform GetTile()
        {
            if (tileQueue.Count <= 0)
            {
                CreateNewTiles(5);
            }

            var tile = tileQueue.Dequeue();
            tile.gameObject.SetActive(true);
            return tile;
        }

        public void RemoveTile(Transform tile)
        {
            tile.gameObject.SetActive(false);
            tileQueue.Enqueue(tile);
        }

        private void CreateNewGems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newGem = Instantiate(gemPrefab, gemParent);
                newGem.gameObject.SetActive(false);
                gemQueue.Enqueue(newGem);
            }
        }

        public Transform GetGem()
        {
            if (gemQueue.Count <= 0)
            {
                CreateNewGems(5);
            }

            var gem = gemQueue.Dequeue();
            gem.gameObject.SetActive(true);
            return gem;
        }

        public void RemoveGem(Transform gem)
        {
            gem.gameObject.SetActive(false);
            gemQueue.Enqueue(gem);
        }
    }
}
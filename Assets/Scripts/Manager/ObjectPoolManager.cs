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
        [SerializeField] private int maxTileCount = 30;

        [Header("Gem")]
        [SerializeField] private Transform gemParent;
        [SerializeField] private Transform gemPrefab;
        [SerializeField] private int maxGemCount = 10;

        [Header("FloatingText")]
        [SerializeField] private Transform floatingScoreTextParent;
        [SerializeField] private Transform floatingScoreTextPrefab;
        [SerializeField] private int maxFloatingTextCount = 10;

        private readonly Queue<Transform> tileQueue = new Queue<Transform>();
        private readonly Queue<Transform> gemQueue = new Queue<Transform>();
        private readonly Queue<Transform> floatingTextQueue = new Queue<Transform>();

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

            CreateNewTiles(maxTileCount);
            CreateNewGems(maxGemCount);
            CreateNewFloatingTexts(maxFloatingTextCount);
        }

        #region Tile

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

        #endregion

        #region Gem

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

        #endregion

        #region FloatingText

        private void CreateNewFloatingTexts(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newFloatingText = Instantiate(floatingScoreTextPrefab, floatingScoreTextParent);
                newFloatingText.gameObject.SetActive(false);
                floatingTextQueue.Enqueue(newFloatingText);
            }
        }

        public Transform GetFloatingText()
        {
            if (floatingTextQueue.Count <= 0)
            {
                CreateNewFloatingTexts(5);
            }

            var floatingText = floatingTextQueue.Dequeue();
            floatingText.gameObject.SetActive(true);
            return floatingText;
        }

        public void RemoveFloatingText(Transform floatingText)
        {
            floatingText.gameObject.SetActive(false);
            floatingTextQueue.Enqueue(floatingText);
        }

        #endregion
    }
}
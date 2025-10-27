using TMPro;
using UnityEngine;
using ZagZig.Ball;

namespace ZagZig.Manager
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance { get; private set; }

        [Header("Tile")]
        [SerializeField] private Tile tilePrefab;
        [SerializeField] private Transform tileParent;
        [SerializeField] private int initialTileCount;
        [Header("Gem")]
        [SerializeField] private Gem gemPrefab;
        [SerializeField] private Transform gemParent;
        [SerializeField] private int initialGemCount;
        [Header("Ball Body")]
        [SerializeField] private BallBody ballBodyPrefab;
        [SerializeField] private Transform ballBodyParent;
        [SerializeField] private int initialBallBodyCount;
        [Header("FloatingText")]
        [SerializeField] private TextMeshPro floatingTextPrefab;
        [SerializeField] private Transform floatingTextParent;
        [SerializeField] private int initialFloatingTextCount;

        private PoolableObject<Tile> tilePool;
        private PoolableObject<Gem> gemPool;
        private PoolableObject<BallBody> ballBodyPool;
        private PoolableObject<TextMeshPro> floatingTextPool;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void InitializePoolForInfiniteLevel()
        {
            tilePool = new PoolableObject<Tile>(tilePrefab, tileParent, initialTileCount);
            gemPool = new PoolableObject<Gem>(gemPrefab, gemParent, initialGemCount);
            floatingTextPool = new PoolableObject<TextMeshPro>(floatingTextPrefab, floatingTextParent, initialFloatingTextCount);

            tilePool.Initialize();
            gemPool.Initialize();
            floatingTextPool.Initialize();
        }

        public void InitializePoolForNormalLevel()
        {
            ballBodyPool = new PoolableObject<BallBody>(ballBodyPrefab, ballBodyParent, initialBallBodyCount);
            ballBodyPool.Initialize();
        }

        public Tile GetTile() => tilePool.Get();
        public void RemoveTile(Tile tile) => this.tilePool.Return(tile);
        public Gem GetGem() => gemPool.Get();
        public void RemoveGem(Gem gem) => gemPool.Return(gem);
        public BallBody GetBallBody() => ballBodyPool.Get();
        public void RemoveBallBody(BallBody ballBody) => ballBodyPool.Return(ballBody);
        public TextMeshPro GetFloatingText() => floatingTextPool.Get();
        public void RemoveFloatingText(TextMeshPro floatingText) => floatingTextPool.Return(floatingText);
    }
}
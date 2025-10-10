using System;
using System.Collections;
using UnityEngine;
using ZagZig.Manager;

namespace ZagZig.Ball
{
    public class BallHead : MonoBehaviour
    {
        private void OnCollisionExit(Collision other)
        {
            var tile = other.gameObject.GetComponentInParent<Tile>();

            if (tile != null)
            {
                StartCoroutine(StartFallTileCoroutine(tile));
            }
        }

        private IEnumerator StartFallTileCoroutine(Tile tile)
        {
            yield return StartCoroutine(tile.StartFallTileCoroutine());
            PathManager.Instance.CreateNewTile();
        }
    }
}
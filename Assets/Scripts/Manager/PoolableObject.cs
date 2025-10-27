using System.Collections.Generic;
using UnityEngine;

namespace ZagZig.Manager
{
    public class PoolableObject<T> where T : Component
    {
        private readonly T prefab;
        private readonly Transform parent;
        private readonly int initialCount;

        private readonly Queue<T> pool = new Queue<T>();

        public PoolableObject(T prefab, Transform parent, int initialCount)
        {
            this.prefab = prefab;
            this.parent = parent;
            this.initialCount = initialCount;
        }

        public void Initialize()
        {
            for (int i = 0; i < initialCount; i++)
            {
                var newObject = Object.Instantiate(prefab, parent);
                pool.Enqueue(newObject);
                newObject.gameObject.SetActive(false);
            }
        }

        private void AddNewObject()
        {
            var newObject = Object.Instantiate(prefab, parent);
            pool.Enqueue(newObject);
            newObject.gameObject.SetActive(false);
        }

        public T Get()
        {
            if (pool.Count <= 0)
            {
                AddNewObject();
            }

            var newObject = pool.Dequeue();
            newObject.gameObject.SetActive(true);
            return newObject;
        }

        public void Return(T obj)
        {
            if (obj == null) return;

            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
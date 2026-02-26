using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ObjectPoolSystem
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField]
        private uint poolSize;
        [SerializeField]
        private PooledObject objectToPool;
        [SerializeField]
        private Stack<PooledObject> stackPool;

        private void Awake()
        {
            SetupPool();
        }

        private void SetupPool()
        {
            stackPool = new Stack<PooledObject>();
            PooledObject instance = null;

            for (int i = 0; i < poolSize; i++)
            {
                instance = Instantiate(objectToPool);
                instance.SetPool(this);
                instance.gameObject.SetActive(false);
                stackPool.Push(instance);
            }
        }

        public PooledObject GetPooledObject()
        {
            if (stackPool.Count == 0)
            {
                PooledObject instance = Instantiate(objectToPool);
                instance.SetPool(this);
                return instance;
            }

            PooledObject nextinstance = stackPool.Pop();
            nextinstance.gameObject.SetActive(true);
            return nextinstance;
        }

        public PooledObject GetPooledObject(Vector3 pos)
        {
            if (stackPool.Count == 0)
            {
                PooledObject instance = Instantiate(objectToPool, pos, Quaternion.identity);
                instance.SetPool(this);
                return instance;
            }

            PooledObject nextinstance = stackPool.Pop();
            nextinstance.transform.position = pos;
            nextinstance.gameObject.SetActive(true);
            return nextinstance;
        }

        public void ReturnToPool(PooledObject pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
            stackPool.Push(pooledObject);
        }
    }
}

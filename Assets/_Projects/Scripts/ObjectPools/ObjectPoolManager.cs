using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance { get; private set; }

    [SerializeField] private PrefabInfo[] prefabInfos;
    [SerializeField] private Transform parentHolder;

    private Dictionary<PooledObject, IObjectPool<PooledObject>> pools = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePool();
        }
        else
            Destroy(gameObject);
    }

    private void InitializePool()
    {
        foreach (var info in prefabInfos)
        {
            IObjectPool<PooledObject> objectPool = null;
            objectPool = new ObjectPool<PooledObject>(
                createFunc: () => CreatePool(info.poolPrefab, objectPool, parentHolder),
                actionOnGet: OnTakeFromPool,
                actionOnRelease: OnReturnToPool,
                actionOnDestroy: OnDestroyPool,
                collectionCheck: true,
                defaultCapacity: info.defaultPoolSize,
                maxSize: info.maxPoolSize
                );

            PreCreate(objectPool, info.defaultPoolSize);
            pools.Add(info.poolPrefab, objectPool);
        }
    }

    private void PreCreate(IObjectPool<PooledObject> objectPool, int defaultPoolSize)
    {
        PooledObject[] tempArray = new PooledObject[defaultPoolSize];
        for (int i = 0; i < tempArray.Length; i++)
            tempArray[i] = objectPool.Get();

        for (int i = 0; i < tempArray.Length; i++)
            objectPool.Release(tempArray[i]);
    }

    public PooledObject GetPool(PooledObject pooledObject, Vector3 position, Quaternion rotation)
    {
        if (pooledObject == null || !pools.ContainsKey(pooledObject))
        {
            Debug.LogError("Prefab hasn't created!");
            return null;
        }

        PooledObject activePooledObject = pools[pooledObject].Get();
        activePooledObject.transform.SetPositionAndRotation(position, rotation);
        return activePooledObject;
    }

    private PooledObject CreatePool(PooledObject pooledPrefab, IObjectPool<PooledObject> pool, Transform parentHolder)
    {
        PooledObject newPool = Instantiate(pooledPrefab, parentHolder);
        newPool.SetupObjectPool(pool);
        return newPool;
    }

    private void OnTakeFromPool(PooledObject pooledObject)
    {
        pooledObject.Reset();
        pooledObject.gameObject.SetActive(true);
    }

    private void OnReturnToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    private void OnDestroyPool(PooledObject pooledObject)
    {
        if (pooledObject != null && pooledObject.gameObject != null)
            Destroy(pooledObject);
    }
}

[System.Serializable]
public struct PrefabInfo
{
    public PooledObject poolPrefab;
    public int defaultPoolSize;
    public int maxPoolSize;
}

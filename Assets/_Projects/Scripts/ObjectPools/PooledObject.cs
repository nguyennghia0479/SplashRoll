using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    protected IObjectPool<PooledObject> objectPool;

    public void SetupObjectPool(IObjectPool<PooledObject> objectPool) => this.objectPool = objectPool;

    public virtual void ReleasePool()
    {

    }

    public virtual void Reset()
    {

    }
}

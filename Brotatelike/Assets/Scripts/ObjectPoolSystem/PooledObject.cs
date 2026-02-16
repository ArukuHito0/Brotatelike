using ObjectPoolSystem;
using UnityEngine;

// プール対象のオブジェクト
public abstract class PooledObject : MonoBehaviour
{
    private ObjectPool pool;

    public void SetPool(ObjectPool pool)
    {
        this.pool = pool;
    }

    // プールにオブジェクトを返却する
    protected void Release()
    {
        pool.ReturnToPool(this);
    }
}

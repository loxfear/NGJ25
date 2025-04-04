using UnityEngine;

public class PoolItem : MonoBehaviour
{
    internal PoolManager.ObjectPool Pool;

    private void OnDisable()
    {
        this.Pool?.Free(this.gameObject);
    }
}
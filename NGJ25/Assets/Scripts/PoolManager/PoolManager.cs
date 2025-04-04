using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class PoolManager
{
    private static readonly Dictionary<string, ObjectPool> Pools = new Dictionary<string, ObjectPool>();

    public static void Reset()
    {
        Pools.Clear();
    }

    public static void Validate()
    {
        foreach (var pool in Pools.Values)
        {
            pool.ValidatePool();
        }
    }

#if UNITY_EDITOR
    static PoolManager()
    {
        EditorApplication.playModeStateChanged += EditorApplicationOnplayModeStateChanged;
    }

    private static void EditorApplicationOnplayModeStateChanged(PlayModeStateChange playModeStateChange)
    {
        if (playModeStateChange == PlayModeStateChange.ExitingEditMode || playModeStateChange == PlayModeStateChange.EnteredEditMode)
        {
            Reset();
        }
    }
#endif

    /// <summary>
    /// Create an instance using the position and orientation of the specified transform
    /// </summary>
    /// <param name="prefab">Prefab to create instance of</param>
    /// <param name="sourceTransform">Transform to use position and orientation from</param>
    /// <param name="activate">Optional activation flag</param>
    /// <returns>New instance</returns>
    public static GameObject CreateAtTransform(GameObject prefab, Transform sourceTransform, bool activate = true)
    {
        var instance = InternalCreate(prefab, activate: activate);
        var t = instance.transform;
        t.position = sourceTransform.position;
        t.rotation = sourceTransform.rotation;
        return instance;
    }

    public static GameObject CreateAsChild(GameObject prefab, Transform sourceTransform, bool activate = true)
    {
        var instance = InternalCreate(prefab, activate: activate);
        var t = instance.transform;
        t.position = sourceTransform.position;
        t.rotation = sourceTransform.rotation;
        t.SetParent(sourceTransform);
        return instance;
    }

    public static GameObject CreateAtPosition(GameObject prefab, Vector3 worldPos, Quaternion rotation,
        bool activate = true, Transform parent = null)
    {
        var instance = InternalCreate(prefab, activate: activate, parent: parent);
        instance.transform.position = worldPos;
        instance.transform.rotation = rotation;
        return instance;
    }

    public static GameObject Create(GameObject prefab, bool activate, Transform parent = null)
    {
        return InternalCreate(prefab, activate, parent: parent);
    }

    public static T Create<T>(T prefab, bool activate, Transform parent = null) where T : MonoBehaviour
    {
        return InternalCreate(prefab.gameObject, activate, parent: parent).GetComponent<T>();
    }

    private static GameObject InternalCreate(GameObject prefab, bool activate, Transform parent = null)
    {
        ObjectPool pool;
        var prefabKey = prefab.name;
        if (Pools.TryGetValue(prefabKey, out pool) == false)
        {
            pool = new ObjectPool(prefab);
            Pools[prefabKey] = pool;
        }

        var instance = pool.GetOrCreate();
        if (parent != null)
        {
            instance.transform.SetParent(parent, false);
        }

        if (activate)
        {
            instance.SetActive(true);
        }

        return instance;
    }

    internal class ObjectPool
    {
        private readonly Stack<GameObject> freeObjects = new Stack<GameObject>();
        private readonly HashSet<int> usedObjects = new HashSet<int>();
        private readonly GameObject prefab;

        public ObjectPool(GameObject poolPrefab)
        {
            this.prefab = poolPrefab;
        }

        internal GameObject Prefab => this.prefab;

        internal int TotalObjects => this.usedObjects.Count + this.freeObjects.Count;

        public void ValidatePool()
        {
            List<GameObject> liveObjects = new List<GameObject>();
            foreach (var obj in this.freeObjects)
            {
                if (obj)
                {
                    liveObjects.Add(obj);
                }
            }

            Debug.Log("Validate pool of " + this.prefab.name + " before: " + this.freeObjects.Count + " after: " +
                      liveObjects.Count);
            this.freeObjects.Clear();
            foreach (var obj in liveObjects)
            {
                this.freeObjects.Push(obj);
            }
        }

        public void Reserve(int count, Transform parent)
        {
            int amountToReserve = count - this.freeObjects.Count;
            for (int i = 0; i < amountToReserve; i++)
            {
                var instance = (GameObject)GameObject.Instantiate(this.prefab);
                instance.SetActive(false);
                instance.transform.SetParent(parent, worldPositionStays: true);
                var poolItem = instance.AddComponent<PoolItem>();
                poolItem.Pool = this;
                this.freeObjects.Push(instance);
            }
        }

        public GameObject GetOrCreate()
        {
            GameObject instance;
            if (this.freeObjects.Count == 0)
            {
                instance = (GameObject)GameObject.Instantiate(this.prefab);
                var poolItem = instance.AddComponent<PoolItem>();
                poolItem.Pool = this;
            }
            else
            {
                instance = this.freeObjects.Pop();
            }

            if (instance == null)
            {
                Debug.LogWarning("Poolobject: " + this.prefab.name +
                                 " had a nullreference in the freeObjects stack.");
                return GetOrCreate();
            }

            this.usedObjects.Add(instance.GetInstanceID());
            return instance;
        }

        public void Free(GameObject instance)
        {
            if (this.usedObjects.Remove(instance.GetInstanceID()))
            {
                this.freeObjects.Push(instance);
            }
            else
            {
                Debug.LogWarning("Tried to free object that is not marked as in use. Object name: " +
                                 instance.name);
            }
        }
    }
}
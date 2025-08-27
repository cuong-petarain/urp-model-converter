using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A generic, scalable object pool for Unity Components.
/// It creates an initial pool of objects and can grow dynamically if the demand exceeds the initial size.
/// </summary>
/// <typeparam name="T">The type of the Component to pool (e.g., Rigidbody, ParticleSystem, or a custom script).</typeparam>
public class ObjectPool<T> where T : Component
{
    private readonly T prefab;
    private readonly Queue<T> objectQueue;
    private readonly Transform parent;

    public ObjectPool(T prefab, int initialSize, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        this.objectQueue = new Queue<T>(initialSize);

        for (int i = 0; i < initialSize; i++)
        {
            T obj = CreateNewObject();
            obj.gameObject.SetActive(false);
            objectQueue.Enqueue(obj);
        }
    }
    public T Get()
    {
        if (objectQueue.Count > 0)
        {
            T obj = objectQueue.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        T newObj = CreateNewObject();
        newObj.gameObject.SetActive(true);
        return newObj;
    }

    public void Return(T obj)
    {
        if (obj != null)
        {
            obj.gameObject.SetActive(false);
            objectQueue.Enqueue(obj);
        }
    }
    private T CreateNewObject()
    {
        T obj = Object.Instantiate(prefab, parent);
        return obj;
    }
}

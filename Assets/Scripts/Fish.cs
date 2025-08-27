using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private LayerMask _penguinMask;
    private ObjectPool<Fish> _fishPool;

    public void Initialize(ObjectPool<Fish> pool)
    {
        _fishPool = pool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _penguinMask)
        {
            _fishPool.Return(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallableObject : MonoBehaviour
{
    public enum ObjectType
    {
        Fish, Stone
    }

    [SerializeField] private LayerMask _penguinMask;
    [SerializeField] private ObjectType _type;
    [SerializeField] private float _yPositionToDisable;

    private ObjectPool<FallableObject> _pool;

    public void Initialize(ObjectPool<FallableObject> pool)
    {
        _pool = pool;
    }

    private void Update()
    {
        if (transform.position.y <= _yPositionToDisable)
        {
            if (_pool != null)
            {
                _pool.Return(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_type == ObjectType.Fish)
        {
            HandleFishBehavior(collision);
        }
        else if (_type == ObjectType.Stone)
        {
            HandleStoneBehavior(collision);
        }
    }

    private void HandleFishBehavior(Collision collision)
    {
        if (collision.gameObject.layer == _penguinMask)
        {
        
        }
    }

    private void HandleStoneBehavior(Collision collision)
    {
        if (collision.gameObject.layer == _penguinMask)
        {

        }
    }
}

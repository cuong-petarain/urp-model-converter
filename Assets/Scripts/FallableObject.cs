using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallableObject : MonoBehaviour
{
    public enum Type
    {
        Fish, Stone
    }

    [Tooltip("Info")]
    [SerializeField] private LayerMask _penguinMask;
    [SerializeField] private Type _type;
    [SerializeField] private float _yPositionToDisable;

    [Tooltip("Events")]
    [SerializeField] private VoidEventHandlerSO _onFishHit;
    [SerializeField] private VoidEventHandlerSO _onStoneHit;

    private ObjectPool<FallableObject> _pool;

    public void Initialize(ObjectPool<FallableObject> pool)
    {
        _pool = pool;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (transform.position.y <= _yPositionToDisable)
        {
            HandleDisable();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_type == Type.Fish)
        {
            HandleFishBehavior(collision);
        }
        else if (_type == Type.Stone)
        {
            HandleStoneBehavior(collision);
        }
    }

    private void HandleFishBehavior(Collision collision)
    {
        if (collision.gameObject.layer == _penguinMask)
        {
            _onFishHit.RaiseEvent();
            HandleDisable();
        }
    }

    private void HandleStoneBehavior(Collision collision)
    {
        if (collision.gameObject.layer == _penguinMask)
        {
            _onStoneHit.RaiseEvent();
            HandleDisable();
        }
    }

    private void HandleDisable()
    {
        if (_pool != null)
        {
            gameObject.SetActive(false);
            _pool.Return(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

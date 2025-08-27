using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    private ObjectPool<Stone> _stonePool;

    public void Initialize(ObjectPool<Stone> pool)
    {
        _stonePool = pool;
    }
}

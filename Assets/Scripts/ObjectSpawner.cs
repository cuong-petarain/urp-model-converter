using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Tooltip("Data")]
    [SerializeField] private FallableObject[] _fishPrefabs = new FallableObject[4];
    [SerializeField] private FallableObject[] _stonePrefab = new FallableObject[4];
    [SerializeField] private float _spawnWidthRange = 4.5f;
    [SerializeField] private float _spawnHeight = 13.6f;

    [Tooltip("Drop Tables")]
    [SerializeField] private FishDropTable _fishDropTable;

    [Tooltip("Events")]
    [SerializeField] private VoidEventHandlerSO _onGameStarted;

    private List<ObjectPool<FallableObject>> _fishPools;
    private List<ObjectPool<FallableObject>> _stonePools;
    private int _initialPoolSize = 15;
    private float _gameTimer;
    private bool _isGameRunning = false;

    private void Awake()
    {
        _fishPools = new List<ObjectPool<FallableObject>>();
        foreach (var fish in _fishPrefabs)
        {
            ObjectPool<FallableObject> pool = new ObjectPool<FallableObject>(fish, _initialPoolSize, transform);
            _fishPools.Add(pool);
        }

        _stonePools = new List<ObjectPool<FallableObject>>();
        foreach (var stone in _stonePrefab)
        {
            ObjectPool<FallableObject> pool = new ObjectPool<FallableObject>(stone, _initialPoolSize, transform);
            _stonePools.Add(pool);
        }
    }

    private void OnEnable()
    {
        _onGameStarted.OnEventRaised += StartSpawning;
    }

    private void OnDisable()
    {
        _onGameStarted.OnEventRaised -= StartSpawning;
    }

    private void Update()
    {
        if (_isGameRunning)
        {
            _gameTimer += Time.deltaTime;
        }
    }

    private void ResetAllPools()
    {
    
    }

    private void StartSpawning()
    {
        _isGameRunning = true;

        FishWeightEntry activeWeight = _fishDropTable.GetWeightsForTime(_gameTimer);
        if (activeWeight != null)
        {
            // get random x position

            // spawn here
        }
    }

    private void StopSpawning()
    {
        _isGameRunning = false;
    }

}

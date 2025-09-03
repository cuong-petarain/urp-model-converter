using System;
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

    [Tooltip("Spawn Interval Data")]
    [SerializeField] private SpawnInterval _fishSpawnInterval;
    [SerializeField] private SpawnInterval _stoneSpawnInterval;

    [Tooltip("Events")]
    [SerializeField] private VoidEventHandlerSO _onGameStarted;

    private WeightedRandomSelector<ObjectType> _fishSelector;
    private Dictionary<ObjectType, ObjectPool<FallableObject>> _fishPools;
    private Dictionary<ObjectType, ObjectPool<FallableObject>> _stonePools;
    private int _initialPoolSize = 15;
    private float _gameTimer;
    private float _fishSpawnTimer;
    private float _stoneSpawnTimer;
    private bool _isGameRunning = false;

    private const float DEFAULT_FISH_SPAWN_TIMER = 2f;
    private const float DEFAULT_STONE_SPAWN_TIMER = 2.5f;

    private void Awake()
    {
        _fishPools = new Dictionary<ObjectType, ObjectPool<FallableObject>>();
        for (int i = 0; i < _fishPrefabs.Length; i++)
        {
            ObjectPool<FallableObject> pool = new ObjectPool<FallableObject>(_fishPrefabs[i], _initialPoolSize, transform);
            _fishPools.Add(GetPrefabType(i), pool);
        }

        _stonePools = new Dictionary<ObjectType, ObjectPool<FallableObject>>();
        for (int i = 0; i < _fishPrefabs.Length; i++)
        {
            ObjectPool<FallableObject> pool = new ObjectPool<FallableObject>(_stonePrefab[i], _initialPoolSize, transform);
            _stonePools.Add(GetPrefabType(i), pool);
        }

        _fishDropTable.SetupSelector();

    }

    private ObjectType GetPrefabType(int index)
    {
        return (ObjectType)Enum.ToObject(typeof(ObjectType), index);
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
        if (!_isGameRunning)
            return;

        _gameTimer += Time.deltaTime;
        _fishSpawnTimer -= Time.deltaTime;
        _stoneSpawnTimer -= Time.deltaTime;

        if (_fishSpawnTimer <= 0)
        {
            FishWeightEntry activeWeight = _fishDropTable.GetWeightsForTime(_gameTimer);
            if (activeWeight != null)
            {
                float xPos = UnityEngine.Random.Range(-_spawnWidthRange, _spawnWidthRange);

                // spawn here
                ObjectType fishType = _fishDropTable.GetRandomItem(_gameTimer);
                FallableObject fish = _fishPools[fishType].Get();
                fish.transform.position = new Vector3(xPos, _spawnHeight, 0);
                fish.transform.rotation = Quaternion.identity;
                fish.Initialize(_fishPools[fishType]);
            }

            _fishSpawnTimer = _fishSpawnInterval.GetIntervalForTime(_gameTimer);
        }

        if (_stoneSpawnTimer <= 0)
        {
            // spawn

            _stoneSpawnTimer = _stoneSpawnInterval.GetIntervalForTime(_gameTimer);
        }
    }

    private void ResetAllPools()
    {
    
    }

    private void StartSpawning()
    {
        _isGameRunning = true;
        _fishSpawnTimer = DEFAULT_FISH_SPAWN_TIMER;
        _stoneSpawnTimer = DEFAULT_STONE_SPAWN_TIMER;
    }

    private void StopSpawning()
    {
        _isGameRunning = false;
    }

}

public enum ObjectType
{
    SmallFish = 0, Fish = 1, BigFish = 2, HugeFish = 3, SmallStone = 4, Stone = 5, BigStone = 6, HugeStone = 7
}
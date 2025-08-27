using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnInterval", menuName = "Data/New Spawn Interval")]
public class SpawnInterval : ScriptableObject
{
    public List<TimeInterval> spawnIntervals = new List<TimeInterval>();

    public float GetIntervalForTime(float currentTime)
    {
        var sortedIntervals = spawnIntervals.OrderBy(entry => entry.Time).ToList();

        for (int i = sortedIntervals.Count - 1; i >= 0; i--)
        {
            if (currentTime >= sortedIntervals[i].Time)
            {
                return sortedIntervals[i].Interval;
            }
        }

        Debug.LogWarning($"No valid spawn interval found for time {currentTime} in {this.name}. Returning default of 1.");
        return 1f;
    }
}

[System.Serializable]
public class TimeInterval
{
    [Tooltip("Active at this time (in seconds).")]
    public float Time;

    [Tooltip("Interval (in seconds).")]
    public float Interval;
}
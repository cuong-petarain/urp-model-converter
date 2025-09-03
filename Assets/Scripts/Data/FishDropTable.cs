using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFishDropTable", menuName = "Loot/Fish Drop Table")]
public class FishDropTable : ScriptableObject
{
    [Tooltip("The list of weight entries, sorted by time. The editor script will handle sorting.")]
    public List<FishWeightEntry> weightEntries = new List<FishWeightEntry>();

    private Dictionary<float, WeightedRandomSelector<ObjectType>> _fishSelector;

    public void SetupSelector()
    {
        foreach (var entry in weightEntries)
        {
            WeightedRandomSelector<ObjectType> weightedRandomSelector = new();
            weightedRandomSelector.AddItem(ObjectType.SmallFish, entry.SmallFish);
            weightedRandomSelector.AddItem(ObjectType.Fish, entry.Fish);
            weightedRandomSelector.AddItem(ObjectType.BigFish, entry.BigFish);
            weightedRandomSelector.AddItem(ObjectType.HugeFish, entry.HugeFish);
            _fishSelector.Add(entry.Time, weightedRandomSelector);
        }
    }

    public FishWeightEntry GetWeightsForTime(float currentTime)
    {
        for (int i = weightEntries.Count - 1; i >= 0; i--)
        {
            if (currentTime >= weightEntries[i].Time)
            {
                return weightEntries[i];
            }
        }

        Debug.LogWarning($"No valid fish weight entry found for time {currentTime} in {this.name}.");
        return null;
    }

    public ObjectType GetRandomItem(float currentTime)
    {
        FishWeightEntry fishEntry = GetWeightsForTime(currentTime);
        WeightedRandomSelector<ObjectType> currentSelector = _fishSelector[fishEntry.Time];
        return currentSelector.GetRandomItem();
    }
}

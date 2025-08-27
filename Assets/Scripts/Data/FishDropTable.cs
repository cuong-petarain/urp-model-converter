using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFishDropTable", menuName = "Loot/Fish Drop Table")]
public class FishDropTable : ScriptableObject
{
    [Tooltip("The list of weight entries, sorted by time. The editor script will handle sorting.")]
    public List<FishWeightEntry> weightEntries = new List<FishWeightEntry>();

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
}

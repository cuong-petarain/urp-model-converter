using System.Collections.Generic;
using UnityEngine;

public class WeightedRandomSelector<T>
{
    private class WeightedItem
    {
        public T Item
        {
            get;
        }
        public int Weight
        {
            get;
        }

        public WeightedItem(T item, int weight)
        {
            Item = item;
            Weight = weight;
        }
    }

    private readonly List<WeightedItem> weightedItems = new List<WeightedItem>();
    private int totalWeight = 0;

    public void AddItem(T item, int weight)
    {
        if (weight <= 0)
            return; // Ignore items with non-positive weight.

        weightedItems.Add(new WeightedItem(item, weight));
        totalWeight += weight;
    }

    public T GetRandomItem()
    {
        if (weightedItems.Count == 0)
        {
            Debug.LogError("WeightedRandomSelector is empty. Cannot get a random item.");
            return default(T);
        }

        int randomNumber = Random.Range(1, totalWeight + 1);

        foreach (var weightedItem in weightedItems)
        {
            if (randomNumber <= weightedItem.Weight)
            {
                return weightedItem.Item;
            }

            randomNumber -= weightedItem.Weight;
        }

        return default(T);
    }
}
using UnityEngine;

[System.Serializable]
public class FishWeightEntry
{
    [Tooltip("This set of weights becomes active at this time (in seconds).")]
    public float Time;

    [Tooltip("The weight for spawning a Small Fish.")]
    public int SmallFish;

    [Tooltip("The weight for spawning a Normal Fish.")]
    public int Fish;

    [Tooltip("The weight for spawning a Big Fish.")]
    public int BigFish;

    [Tooltip("The weight for spawning a Huge Fish.")]
    public int HugeFish;
}

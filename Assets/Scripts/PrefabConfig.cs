[System.Serializable]
public class PrefabConfigItem
{
    public string key;
    public float[] position;
}

[System.Serializable]
public class PrefabConfig
{
    public PrefabConfigItem[] prefabs;
}
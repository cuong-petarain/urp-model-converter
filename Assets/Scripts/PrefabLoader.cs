using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PrefabLoader : MonoBehaviour
{
    public string prefabKey; // Set in Inspector or via code

    void Start()
    {
        LoadPrefab(prefabKey);
    }

    void LoadPrefab(string key)
    {
        Addressables.LoadAssetAsync<GameObject>(key).Completed += OnPrefabLoaded;
    }

    void OnPrefabLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(obj.Result, Vector3.zero, Quaternion.identity);
        }
    }
}

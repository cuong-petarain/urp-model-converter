using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;

public class MultiplePrefabLoader : MonoBehaviour
{
    public string label = "3DObjects";

    void Start()
    {
        Addressables.LoadAssetsAsync<GameObject>(label, OnPrefabLoaded);
        // what
    }

    void OnPrefabLoaded(GameObject prefab)
    {
        // Example: Instantiate in a grid
        Vector3 pos = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        Instantiate(prefab, pos, Quaternion.identity);
    }
}

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Networking;
using System.Collections;

public class DynamicPrefabLoader : MonoBehaviour
{
    public string configUrl = "https://fantastic-sherbet-a77e7e.netlify.app/prefab_config.json";

    void Start()
    {
        StartCoroutine(LoadConfig());
    }

    IEnumerator LoadConfig()
    {
        UnityWebRequest request = UnityWebRequest.Get(configUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string json = request.downloadHandler.text;
            PrefabConfig config = JsonUtility.FromJson<PrefabConfig>(json);
            LoadPrefabs(config);
        }
        else
        {
            Debug.LogError("Failed to download config: " + request.error);
        }
    }

    void LoadPrefabs(PrefabConfig config)
    {
        foreach (var item in config.prefabs)
        {
            Addressables.LoadAssetAsync<GameObject>(item.key).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Vector3 pos = Vector3.zero;
                    if (item.position != null && item.position.Length == 3)
                        pos = new Vector3(item.position[0], item.position[1], item.position[2]);

                    Instantiate(handle.Result, pos, Quaternion.identity);
                }
            };
        }
    }
}

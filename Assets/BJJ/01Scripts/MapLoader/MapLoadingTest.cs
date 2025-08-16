using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Timeline;

public class MapLoadingTest : MonoBehaviour
{
    public string LoadMap;
    public MapData mapData;

    async void Start()
    {
        await StartAsync();
    }

    private async Task StartAsync()
    {
        await MapDataLoad();

        if (mapData == null)
        {
            Debug.Log("MapLoadingTest.cs - Start() - mapData Loading Error");
            return;
        }
        else
        {
            await MapLoader.Instance.LoadMapAsync(mapData);
        }
    }

    private async Task MapDataLoad()
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>("Map_" + LoadMap + ".json");
        await handle.Task;

        var json = handle.Result.text;
        mapData = JsonUtility.FromJson<MapData>(json);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MapLoader : DestroySingleton<MapLoader>
{
    private Dictionary<string, GameObject> prefabCache = new Dictionary<string, GameObject>();

    /// <summary>
    /// 맵 데이터를 받아서 맵을 생성하는 매서드
    /// </summary>
    /// <param name="mapData">로비씬에서 고른 맵 데이터를 받아서 생성을 시작함</param>
    /// <returns></returns>
    public async Task LoadMapAsync(MapData mapData)
    {
        if(mapData == null)
        {
            Debug.Log("MapLoader.cs - LoadMapAsync() - MapData is Null");
            return;
        }

        var root = await CreatMapObjectFromDataAsync(mapData.Root, null);
        prefabCache.Clear();
    }

    // 재귀 함수를 이용하여 맵 오브젝트를 생성할 예정
    private async Task<GameObject> CreatMapObjectFromDataAsync(MapObjectData data, Transform parent)
    {
        GameObject obj = null;

        if(!string.IsNullOrEmpty(data.Resources))
        {
            if(!prefabCache.TryGetValue(data.Resources, out var prefab))
            {
                var handle = Addressables.LoadAssetAsync<GameObject>(data.Resources);
                prefab = await handle.Task;

                if(prefab != null)
                {
                    prefabCache[data.Resources] = prefab;
                }
                else
                {
                    Debug.LogWarning($"Prefab not found for resource: {data.Resources}");
                }
            }

            if(prefab != null)
            {
                obj = Instantiate(prefab, parent);
            }
        }

        if(obj == null)
        {
            obj = new GameObject(data.name);
            obj.transform.parent = parent;
        }

        Debug.Log($"{data.name}_Creating");

        obj.name = data.name;
        obj.transform.localPosition = data.Position;
        obj.transform.localEulerAngles = data.Rotation;
        obj.transform.localScale = data.Scale;
        if(data.Tag != "Untagged")
            obj.tag = data.Tag;
        obj.layer = data.layer;

        if(data.hasColl && data.Coll != null)
        {
            ApplyCollider(obj, data.Coll);
        }

        foreach(var child in data.childrens)
        {
            await CreatMapObjectFromDataAsync(child, obj.transform);
        }

        return obj;
    }

    private void ApplyCollider(GameObject obj, ColliderData data)
    {
        if (data == null) return;

        switch (data.type)
        {
            case "BoxCollider":
                var box = obj.AddComponent<BoxCollider>();
                box.isTrigger = data.isTrigger;
                box.size = data.size;
                box.center = data.center;
                break;
            case "SphereCollider":
                var sphere = obj.AddComponent<SphereCollider>();
                sphere.isTrigger = data.isTrigger;
                sphere.center = data.center;
                sphere.radius = data.radius;
                break;
            case "CapsuleCollider":
                var capsule = obj.AddComponent<CapsuleCollider>();
                capsule.isTrigger = data.isTrigger;
                capsule.center = data.center;
                capsule.height = data.size.y;
                capsule.radius = data.radius;
                break;
            case "MeshCollider":
                var mesh = obj.AddComponent<MeshCollider>();
                mesh.isTrigger = data.isTrigger;
                if (obj.TryGetComponent<MeshFilter>(out var mf))
                    mesh.sharedMesh = mf.sharedMesh;
                break;
            default:

                break;
        }
    }
}

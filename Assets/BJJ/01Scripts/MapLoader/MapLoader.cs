using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;

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
        BakeNavMesh(root);
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
        {
            obj.tag = data.Tag;
            foreach(Transform child in obj.transform)
            {
                child.gameObject.tag = obj.tag;
            }
        }
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

        bool hasColl = false;

        if (obj.TryGetComponent<Collider>(out Collider collider))
        {
            hasColl = true;
        }

        switch (data.type)
        {
            case "BoxCollider":
                BoxCollider box;
                if (!hasColl)
                    box = obj.AddComponent<BoxCollider>();
                else
                    box = collider as BoxCollider;

                box.isTrigger = data.isTrigger;
                box.size = data.size;
                box.center = data.center;
                break;
            case "SphereCollider":
                SphereCollider sphere;

                if (!hasColl)
                    sphere = obj.AddComponent<SphereCollider>();
                else
                    sphere = collider as SphereCollider;

                sphere.isTrigger = data.isTrigger;
                sphere.center = data.center;
                sphere.radius = data.radius;
                break;
            case "CapsuleCollider":
                CapsuleCollider capsule;
                if (!hasColl)
                    capsule = obj.AddComponent<CapsuleCollider>();
                else
                    capsule = collider as CapsuleCollider;

                capsule.isTrigger = data.isTrigger;
                capsule.center = data.center;
                capsule.height = data.size.y;
                capsule.radius = data.radius;
                break;
            case "MeshCollider":
                MeshCollider mesh;
                if (!hasColl) mesh = obj.AddComponent<MeshCollider>();
                else mesh = collider as MeshCollider;

                mesh.isTrigger = data.isTrigger;
                if (obj.TryGetComponent<MeshFilter>(out var mf))
                    mesh.sharedMesh = mf.sharedMesh;
                break;
            default:

                break;
        }
    }

    private void BakeNavMesh(GameObject root)
    {
        NavMeshSurface surface = root.AddComponent<NavMeshSurface>();

        int AREA_WALKABLE = NavMesh.GetAreaFromName("Walkable");
        int AREA_OBSTACLE = NavMesh.GetAreaFromName("Not Walkable");
        var sources = new List<NavMeshBuildSource>();
        var markups = new List<NavMeshBuildMarkup>();

        bool hasBounds = false;
        Bounds bound = default;

        void Encapsuleate(Bounds b)
        {
            if(!hasBounds)
            {
                bound = b;
                hasBounds = true;
            }else bound.Encapsulate(b);
        }

        // 오브젝트 수집
        foreach(var coll in root.GetComponentsInChildren<Collider>())
        {
            var go = coll.gameObject;
            if(go.CompareTag("Walkable") || go.CompareTag("Obstacle"))
            {
                var src = new NavMeshBuildSource();
                src.area = go.CompareTag("Walkable") ? AREA_WALKABLE : AREA_OBSTACLE;
                Matrix4x4 m = go.transform.localToWorldMatrix;

                switch(coll)
                {
                    case BoxCollider box:
                        src.shape = NavMeshBuildSourceShape.Box;
                        src.size = box.size;
                        src.transform = m * Matrix4x4.TRS(box.center, Quaternion.identity, Vector3.one);
                        Encapsuleate(box.bounds);
                        break;
                    case CapsuleCollider capsule:
                        src.shape = NavMeshBuildSourceShape.Capsule;
                        src.size = new Vector3(capsule.radius * 2f, capsule.height, capsule.radius * 2f);
                        Quaternion rot = Quaternion.identity;
                        switch(capsule.direction)
                        {
                            case 0:
                                rot = Quaternion.Euler(0,0,90); break;
                            case 1:
                                rot = Quaternion.Euler(90, 0, 0); break;
                        }
                        src.transform = m * Matrix4x4.TRS(capsule.center, rot, Vector3.one);
                        Encapsuleate(capsule.bounds);
                        break;
                    case SphereCollider sphere:
                        src.shape = NavMeshBuildSourceShape.Sphere;
                        src.size = Vector3.one * sphere.radius * 2f;
                        src.transform = m * Matrix4x4.TRS(sphere.center, Quaternion.identity, Vector3.one);
                        Encapsuleate(sphere.bounds);
                        break;
                    case MeshCollider mesh:
                        if(mesh.sharedMesh != null)
                        {
                            src.shape = NavMeshBuildSourceShape.Mesh;
                            src.sourceObject = mesh.sharedMesh;
                            src.transform = m;
                        }
                        break;
                }
                sources.Add(src);
            }
        }

        if (!hasBounds)
        {
            bound = new Bounds(root.transform.position, new Vector3(50, 50, 50));
        }
        else
            bound.Expand(1.0f);

        var setting = NavMesh.GetSettingsByID(0);
        var data = NavMeshBuilder.BuildNavMeshData(setting, sources, bound, root.transform.position, root.transform.rotation);

        if(data != null)
        {
            NavMesh.AddNavMeshData(data);
        }
    }
}

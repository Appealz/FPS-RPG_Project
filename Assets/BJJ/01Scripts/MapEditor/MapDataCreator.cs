#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;

public class MapDataCreator : MonoBehaviour
{
    public string MapName;
    public string MapDescript;
    public string saveFolder = "Assets/MapData";

    [ContextMenu("Generate Map Data")]
    public void MapCreate()
    {
        var data = new MapData();
        data.MapName = MapName;
        data.MapDescript = MapDescript;

        data.Root = CollectObjectData(transform);

        string path = Path.Combine(saveFolder, MapName);
        if(!Directory.Exists(path))
            Directory.CreateDirectory(path);

        // 직렬화
        string json = JsonUtility.ToJson(data, true);

        // 저장
        string jsonPath = Path.Combine(path, $"{MapName}.json");
        File.WriteAllText(jsonPath, json);

        AssetDatabase.Refresh();
        Debug.Log($"맵 데이터 저장 : {jsonPath}");
    }

    private MapObjectData CollectObjectData(Transform obj)
    {
        MapObjectData data = new MapObjectData();
        data.name = obj.name;
        data.Position = obj.localPosition;
        data.Rotation = obj.eulerAngles;
        data.Scale = obj.localScale;
        data.Tag = obj.tag;
        data.layer = obj.gameObject.layer;
        if(obj.TryGetComponent<Collider>(out var coll))
        {
            data.hasColl = true;
            data.Coll = CreateColliderData(coll);
        }

        var prefab = PrefabUtility.GetCorrespondingObjectFromSource(obj.gameObject);
        if (prefab != null)
            data.Resources = GetResourcePath(obj.gameObject);
        else
            data.Resources = string.Empty;

        data.childrens = new List<MapObjectData>();

        foreach(Transform child in obj)
        {
            if(!IsPrefabChileren(child))
                data.childrens.Add(CollectObjectData(child));
        }
        return data;
    }

    private ColliderData CreateColliderData(Collider coll)
    {
        ColliderData colData = new ColliderData
        {
            isTrigger = coll.isTrigger
        };

        switch (coll)
        {
            case BoxCollider box:
                colData.type = "BoxCollider";
                colData.size = box.size;
                colData.center = box.center;
                break;

            case SphereCollider sphere:
                colData.type = "SphereCollider";
                colData.radius = sphere.radius;
                colData.center = sphere.center;
                break;

            case CapsuleCollider capsule:
                colData.type = "CapsuleCollider";
                colData.radius = capsule.radius;
                colData.size = new Vector3(0, capsule.height, 0); // height를 size.y에 저장
                colData.center = capsule.center;
                break;

            case MeshCollider mesh:
                colData.type = "MeshCollider";
                colData.center = Vector3.zero; // MeshCollider는 center 없음
                break;

            default:
                colData.type = coll.GetType().Name;
                break;
        }

        return colData;
    }

    private bool IsPrefabChileren(Transform child)
    {
        var prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(child.gameObject);
        if (prefabRoot == null)
            return false;

        if (prefabRoot == child.gameObject)
            return false;

        Debug.Log($"{child.gameObject.name} is Prefab Child");
        return true;
    }


    private string GetResourcePath(GameObject obj)
    {
        // 어드레서블 세팅이 되어있는지 확인
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        if(settings == null)
        {
            Debug.Log("Addressable Asset Settings not found. Please enable Addressables in your project.");
            return null;
        }

        // 오브젝트가 원본 프리펩이 아닐 경우.
        var prefabRoot = PrefabUtility.GetOutermostPrefabInstanceRoot(obj);
        if (prefabRoot != obj)
            return string.Empty;

        // 원본 프리펩이 존재하는 오브젝트인지 확인
        string path = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(obj));
        if (string.IsNullOrEmpty(path)) // 원본이 없을 경우 패스
            return string.Empty;

        var entry = settings.FindAssetEntry(path);
        if(entry == null) // 원본 프리펩이 어드레서블에 등록이 안되어 있다면.
        {
            // 그룹 찾기 및 없을 시 생성
            var group = settings.FindGroup("MapResources");
            if (group == null)
                group = settings.CreateGroup("MapResources", false, false, false,
                        null, typeof(UnityEditor.AddressableAssets.Settings.GroupSchemas.BundledAssetGroupSchema));

            // 오브젝트 등록
            entry = settings.CreateOrMoveEntry(AssetDatabase.AssetPathToGUID(path), group, false, false);
            entry.address = Path.GetFileName(path);
        }

        return entry.address;
    }

}
#endif
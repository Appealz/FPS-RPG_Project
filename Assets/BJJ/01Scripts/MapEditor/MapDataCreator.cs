#if UNITY_EDITOR
using System.IO;
using UnityEditor;
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
        data.hasColl = obj.TryGetComponent<Collider>(out var coll);

        data.Resources = GetResourcePath(obj.gameObject);

        foreach(Transform child in obj)
        {
            data.childrens.Add(CollectObjectData(child));
        }
        return data;
    }


    private string GetResourcePath(GameObject obj)
    {
        string path = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromSource(obj));
        if (string.IsNullOrEmpty(path))
            return string.Empty;

        int resIndex = path.IndexOf("Resources/");
        if (resIndex >= 0)
        {
            path = path.Substring(resIndex + "Resources/".Length);
            path = Path.ChangeExtension(path, null); // 확장자 제거
        }

        return path;
    }

}
#endif
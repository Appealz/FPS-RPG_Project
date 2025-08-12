using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public string MapName;
    public string MapDescript;
    public MapObjectData Root;
}

[Serializable]
public class MapObjectData
{
    public string name;
    public string Tag;
    public int layer;
    public string Resources;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public bool hasColl;
    public ColliderData Coll;
    public List<MapObjectData> childrens;
}

[Serializable]
public class ColliderData
{
    public string type;
    public bool isTrigger;
    public Vector3 size;
    public Vector3 center;
    public float radius;
}
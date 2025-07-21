using System;
using UnityEngine;
public class UpdateSpawnObject
{
    public UpdateType type;
    public IUpdateObject obj;

    public UpdateSpawnObject(UpdateType newType, IUpdateObject newObj)
    {
        type = newType; obj = newObj;
    }
}

public static class EventBus_SpawnObject
{
    public static void Subscribe(Action<UpdateSpawnObject> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }

    public static void UnSubscribe(Action<UpdateSpawnObject> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }

    public static void Publish(UpdateSpawnObject evt)
    {
        EventBus.Publish(evt);
    }
}

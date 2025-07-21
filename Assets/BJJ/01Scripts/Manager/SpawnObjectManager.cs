using System.Collections.Generic;
using UnityEngine;

public class SpawnObjectManager
{
    private List<IUpdateObject> objs;

    public void InitObjectManager()
    {
        objs = new List<IUpdateObject>();

        EventBus_SpawnObject.Subscribe(UpdateObjectList);
    }

    public void DestoyObjectManger()
    {
        EventBus_SpawnObject.UnSubscribe(UpdateObjectList);
    }

    public void SpawnObejctUpdate()
    {
        for (int i = objs.Count - 1; i >= 0; i--)
        {
            objs[i].ObjectUpdate();
        }
    }

    private void UpdateObjectList(UpdateSpawnObject evt)
    {
        switch (evt.type)
        {
            case UpdateType.Regist:
                objs.Add(evt.obj);
                break;
            case UpdateType.Unregist:
                objs.Remove(evt.obj);
                break;
        }
    }
}

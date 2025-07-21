using UnityEngine;

public interface ISkillObject
{
    void InitSpawnObj(GameObject ownerObj, ClassSkillData data);
}

public interface IUpdateObject
{
    public void ObjectUpdate();
}


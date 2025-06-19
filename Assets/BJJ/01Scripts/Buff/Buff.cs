using UnityEngine;

public abstract class Buff
{
    public int BuffID { get; private set; }
    public string BuffName { get; private set; }
    public float Duration { get; private set; }
    public float ElapsedTime { get; private set; }

    public Sprite BuffIcon { get; private set; }

    public GameObject Target { get; private set; }
    public bool IsStackable { get; private set; }

    public bool isExpired => ElapsedTime >= Duration;

    public Buff (int buffID, string buffName, float duration, Sprite buffIcon, GameObject target, bool isStackable = false)
    {
        BuffID = buffID;
        BuffName = buffName;
        Duration = duration;
        ElapsedTime = 0f;
        BuffIcon = buffIcon;
        Target = target;
        IsStackable = isStackable;
    }

    public abstract void OnApply();
    public virtual void OnRemove() { }

    public virtual void BuffUpdate(float deltaTime)
    {
        ElapsedTime += deltaTime;
    }
}

using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Animator animator;
    AnimatorOverrideController overrideController;
    private static AnimationClip emptyClip;
    private int use = Animator.StringToHash("IsUse");
    private int reload = Animator.StringToHash("IsReload");
    private int drop = Animator.StringToHash("IsDrop");


    private void Awake()
    {
        if(!TryGetComponent<Animator>(out animator))
        {
            Debug.Log("animator is not ref");
        }
        if (emptyClip == null)
        {
            emptyClip = new AnimationClip();
            emptyClip.name = "Empty";
            emptyClip.legacy = false;
        }
    }

    private void OnEnable()
    {
        EventBus_ItemClip.Subscribe(ApplyClipHandler);
    }

    private void OnDisable()
    {
        EventBus_ItemClip.UnSubscribe(ApplyClipHandler);
    }

    public void UseAnim()
    {
        animator.SetTrigger(use);
    }

    public void ReloadAnim()
    {
        animator.SetTrigger(reload);
    }

    public void DropAnim()
    {
        animator.SetTrigger(drop);
    }

    private void ApplyClips(AnimationClip useClip, AnimationClip reloadClip = null, AnimationClip dropClip = null)
    {
        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        if(useClip != null)
        {
            overrideController["Use"] = useClip;
        }
        else
        {
            overrideController["Use"] = emptyClip;
        }
        if(reloadClip != null)
        {
            overrideController["Reload"] = reloadClip;
        }
        else
        {
            overrideController["Reload"] = emptyClip;
        }
        if(dropClip != null)
        {
            overrideController["Drop"] = dropClip;
        }
        else
        {
            overrideController["Drop"] = emptyClip;
        }

        animator.runtimeAnimatorController = overrideController;
    }

    public void ApplyClipHandler(ItemClipChangedEvent newItemClip)
    {
        ApplyClips(newItemClip.useClip, newItemClip.reloadClip, newItemClip.dropClip);
    }

    public void PlayUseAnim(ItemAnimEvent animEvent)
    {
        if (animEvent.sender != gameObject)
            return;
        if(animEvent.animType == ItemAnimType.Use)
        {
            UseAnim();
        }
    }
}

#region _AnimEvent_
public static class Event_ItemAnim
{
    public static void Subscribe(Action<ItemAnimEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }
    public static void UnSubscribe(Action<ItemAnimEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }
    public static void Publish(ItemAnimEvent type)
    {
        EventBus.Publish(type);
    }
}

public enum ItemAnimType
{
    Use,
    Reload,
    Drop,
}
public class ItemAnimEvent
{
    public ItemAnimType animType;
    public GameObject sender;

    public ItemAnimEvent(GameObject Sender, ItemAnimType newAnimType)
    {
        sender = Sender;
        animType = newAnimType;
    }
}
#endregion

#region _AnimClipSet_
public static class EventBus_ItemClip
{
    public static void Subscribe(Action<ItemClipChangedEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }
    public static void UnSubscribe(Action<ItemClipChangedEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }
    public static void Publish(ItemClipChangedEvent type)
    {
        EventBus.Publish(type);
    }
}


public class ItemClipChangedEvent
{
    public AnimationClip useClip;
    public AnimationClip dropClip;
    public AnimationClip reloadClip;
    

    public ItemClipChangedEvent(IItem changeItem)
    {
        useClip = changeItem.useClip;
        dropClip = changeItem.dropClip;
        reloadClip = changeItem.reloadClip;
    }
}
#endregion
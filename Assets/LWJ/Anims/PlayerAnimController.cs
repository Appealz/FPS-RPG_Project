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

    public void ApplyClips(AnimationClip useClip, AnimationClip reloadClip = null, AnimationClip dropClip = null)
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

    public void ApplyClipHandler(ItemCilpChangedEvent newItemClip)
    {
        
    }
}



public static class EventBus_ItemClip
{
    public static void Subscribe(Action<ItemCilpChangedEvent> newMethod)
    {
        EventBus.Subscribe(newMethod);
    }
    public static void UnSubscribe(Action<ItemCilpChangedEvent> newMethod)
    {
        EventBus.UnSubscribe(newMethod);
    }
    public static void Publish(ItemCilpChangedEvent type)
    {
        EventBus.Publish(type);
    }
}


public class ItemCilpChangedEvent
{
    public AnimationClip useClip;
    public AnimationClip dropClip;
    public AnimationClip reloadClip;
    

    public ItemCilpChangedEvent(IItem changeItem)
    {        
        
    }
}
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using Unity.Multiplayer.Center.Common;
using UnityEngine;

public interface IPlayerAnimHandle
{    
    event Action OnUseEvent;
    event Action OnReloadEvent;
    event Action OnReloadCancel;
    event Action OnSkillEffectEvent;
 
}

public class PlayerAnimController : MonoBehaviour, IPlayerAnimHandle
{
    Animator animator;
    AnimatorOverrideController overrideController;
    private static AnimationClip emptyClip;
    private int use = Animator.StringToHash("IsUse");
    private int reload = Animator.StringToHash("IsReload");
    private int drop = Animator.StringToHash("IsDrop");

    public event Action OnUseEvent;
    public event Action OnReloadEvent;    
    public event Action OnSkillEffectEvent;        
    public event Action OnReloadCancel;

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
        EventBus_ItemAnim.Subscribe(PlayEventAnim);
    }

    private void OnDisable()
    {
        EventBus_ItemClip.UnSubscribe(ApplyClipHandler);
        EventBus_ItemAnim.UnSubscribe(PlayEventAnim);
    }
    
    public void UseAnim()
    {
        animator.SetTrigger(use);
        PlayAnimation();
        
    }

    public void ReloadAnim()
    {
        animator.SetTrigger(reload);
        PlayAnimation();
    }


    private void ApplyClips(AnimationClip useClip, AnimationClip reloadClip = null)
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

        animator.runtimeAnimatorController = overrideController;
    }

    public void ApplyClipHandler(ItemClipChangedEvent newItemClip)
    {
        ApplyClips(newItemClip.useClip, newItemClip.reloadClip);
        currentEquipItem = newItemClip.currentEquipItem;
    }

    // 총기는 애니메이션 X
    public void PlayEventAnim(ItemAnimEvent animEvent)
    {
        if (animEvent.sender != gameObject)
            return;
        animEventData = animEvent.animData;
        switch(animEvent.animType)
        {
            case ItemAnimType.Use:
                UseAnim();
                break;
            case ItemAnimType.Reload:
                ReloadAnim();
                break;
        }
    }

    // 스킬스타트 => 1. 애니메이션 실행, 없으면 로직실행
    
    AnimEventData animEventData;
    List<TimeEvent> currentEvent;
    float animElapsedTime;
    int currentEventIndex;
    bool animRunning;
    IItem currentEquipItem;

    public void PlayAnimation()
    {
        currentEvent = animEventData.EventList;
        Debug.Log($"Count : {currentEvent.Count} ");
        animElapsedTime = 0f;
        currentEventIndex = 0;
        animRunning = true;        
    }

    public void AnimationUpdate()
    {
        if (!animRunning || currentEvent == null)
            return;
        animElapsedTime += Time.deltaTime;
        // currentEventInex : 현재 실행중인 이벤트 인덱스
        // currentEvent.Count : 현재 이벤트의 개수
        // 현재 이벤트 인덱스의 이벤트 시간이 누적시간보다 작거나 같다면
        if(currentEventIndex < currentEvent.Count && currentEvent[currentEventIndex].Time <= animElapsedTime)
        {
            ExecuteTimedEvent(currentEvent[currentEventIndex]);
            currentEventIndex++;
        }
    }

    private void ExecuteTimedEvent(TimeEvent evt)
    {
        Debug.Log($"[타이밍 이벤트] {evt.EventName} at {evt.Time}s | param: {evt.Param}");

        switch (evt.EventName)
        {
            case "Use":
                OnUseEvent?.Invoke();
                break;
            case "Reload":
                OnReloadEvent?.Invoke();
                break;
            case "Cancel":
                OnReloadCancel?.Invoke();
                break;
            case "End":
                //currentSkill.Finish();
                animRunning = false;
                break;
            case "UseSkill":
                OnSkillEffectEvent?.Invoke();
                break;

        }
    }

}



#region _AnimEvent_
public static class EventBus_ItemAnim
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
}

public class ItemAnimEvent
{
    public ItemAnimType animType;
    public GameObject sender;
    public AnimEventData animData;

    public ItemAnimEvent(GameObject Sender, ItemAnimType newAnimType, AnimEventData newAnimData)
    {
        sender = Sender;
        animType = newAnimType;
        animData = newAnimData;
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
    public AnimationClip reloadClip;
    public IItem currentEquipItem;

    public ItemClipChangedEvent(IItem changeItem)
    {
        currentEquipItem = changeItem;
        useClip = changeItem.useClip;
        reloadClip = changeItem.reloadClip;
    }
}
#endregion
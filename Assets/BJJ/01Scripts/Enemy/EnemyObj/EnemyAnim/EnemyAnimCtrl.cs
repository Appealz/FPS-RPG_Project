using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimCtrl : MonoBehaviour, IAnimCtrl, IAnimHandle
{
    protected Animator anim;
    private IEnemyContextReadable context;

    public List<TimeEvent> eventList;

    public event Action OnAttackEvent;
    public event Action OnAnimFinishEvent;

    [SerializeField]protected bool isAnimEvent;
    private float curTime;
    private int curEventIndex;

    public void AnimUpdate()
    {
        if (!isAnimEvent) return;
        if (eventList == null || curEventIndex >= eventList.Count) return;

        curTime += Time.deltaTime;

        if (eventList[curEventIndex].Time <= curTime)
        {
            ExecuteAnimEvent(eventList[curEventIndex++]);
        }
    }

    public void ExecuteAnimEvent(TimeEvent evt)
    {
        switch(evt.EventName)
        {
            case "Attack":
                OnAttackEvent?.Invoke();
                break;
            case "AnimFinish":
                OnAnimFinishEvent?.Invoke();
                isAnimEvent = false;
                break;
        }
    }

    public virtual void Init()
    {
        if (!TryGetComponent<Animator>(out anim))
            Debug.Log($"{gameObject.name} EnemyAnimCtrl.cs - Init() - Animator Can't Referenece");
        if (!TryGetComponent<IEnemyContextReadable>(out context))
            Debug.Log($"{gameObject.name} EnemyAnimCtrl.cs - Init() - IEnemyContextReadable Can't Referenece");

        curTime = 0;
        curEventIndex = 0;
        isAnimEvent = false;
    }


    public virtual void SetAnim(string animName)
    {
        anim.SetTrigger(AnimHash.GetAnimHash(animName));
        if (EnemyAnimEventDataManager.GetAnimEventData($"{context.enemyName}_{animName}", out var data))
        {
            eventList = data.EventList;
            isAnimEvent = true;
            curTime = 0;
            curEventIndex = 0;
        }
        else isAnimEvent = false;
    }

    public virtual void SetMoveState(bool isOn)
    {
        anim.SetBool(AnimHash.GetAnimHash("IsMove"), isOn);
    }
}

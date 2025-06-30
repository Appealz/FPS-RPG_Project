using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimCtrl : MonoBehaviour, IAnimCtrl, IAnimHandle
{
    private Animator anim;

    public List<TimeEvent> eventList;

    public event Action OnAttackEvent;
    public event Action OnAnimFinishEvent;

    private bool isAnimEvent;
    private float curTime;
    private int curEventIndex;

    public void AnimUpdate()
    {
        if (!isAnimEvent) return;

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
                OnAttackEvent.Invoke();
                break;
            case "AnimFinish":
                OnAnimFinishEvent.Invoke();
                break;
        }
    }

    public void Init()
    {
        if (!TryGetComponent<Animator>(out anim))
            Debug.Log($"{gameObject.name} EnemyAnimCtrl.cs - Init() - Animator Can't Referenece");

        curTime = 0;
        curEventIndex = 0;
        isAnimEvent = false;
    }

    public void SetAnim(string animName)
    {
        anim.SetTrigger(AnimHash.GetAnimHash(animName));
        // todo AnimEvent 가져오기
        curTime = 0;
        curEventIndex = 0;
    }

    public void SetMoveState(bool isOn)
    {
        anim.SetBool(AnimHash.GetAnimHash("IsMove"), isOn);
    }
}

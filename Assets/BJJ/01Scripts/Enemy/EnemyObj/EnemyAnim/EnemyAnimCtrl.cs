using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimCtrl : MonoBehaviour, IAnimCtrl, IAnimHandle
{
    private Animator anim;
    private IEnemyContextReadable context;

    public List<TimeEvent> eventList;

    public event Action OnAttackEvent;
    public event Action OnAnimFinishEvent;

    private bool isAnimEvent;
    private float curTime;
    private int curEventIndex;

    private bool isIKSet = false;
    private bool isRHSet;
    private bool isLHSet;
    private Transform rh_GripPoint;
    private Transform lh_GripPoint;

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
                OnAttackEvent?.Invoke();
                break;
            case "AnimFinish":
                OnAnimFinishEvent?.Invoke();
                isAnimEvent = false;
                break;
        }
    }

    public void Init()
    {
        if (!TryGetComponent<Animator>(out anim))
            Debug.Log($"{gameObject.name} EnemyAnimCtrl.cs - Init() - Animator Can't Referenece");
        if (!TryGetComponent<IEnemyContextReadable>(out context))
            Debug.Log($"{gameObject.name} EnemyAnimCtrl.cs - Init() - IEnemyContextReadable Can't Referenece");

        isIKSet = false;
        SetIK();

        curTime = 0;
        curEventIndex = 0;
        isAnimEvent = false;
    }

    private void SetIK()
    {
        isRHSet = false;
        isLHSet = false;
        rh_GripPoint = MyUtility.GetChildrenTrans(transform, "RHGripPoint");
        lh_GripPoint = MyUtility.GetChildrenTrans(transform, "LHGripPoint");
        if (rh_GripPoint != null) isRHSet = true;
        if (lh_GripPoint != null) isLHSet = true;

        if(isRHSet || isLHSet)
            isIKSet = true;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!isIKSet) return;

        RightHandIK();
        LeftHandIK();
    }

    private void RightHandIK()
    {
        if (!isRHSet) return;

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rh_GripPoint.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rh_GripPoint.rotation);
    }

    private void LeftHandIK()
    {
        if (!isLHSet) return;

        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);
        anim.SetIKPosition(AvatarIKGoal.LeftHand, lh_GripPoint.position);
        anim.SetIKRotation(AvatarIKGoal.LeftHand, lh_GripPoint.rotation);
    }

    public void SetAnim(string animName)
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

    public void SetMoveState(bool isOn)
    {
        anim.SetBool(AnimHash.GetAnimHash("IsMove"), isOn);
    }
}

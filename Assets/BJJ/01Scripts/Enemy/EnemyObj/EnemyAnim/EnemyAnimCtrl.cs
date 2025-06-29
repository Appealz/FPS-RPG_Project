using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimCtrl : MonoBehaviour, IAnimCtrl, IAnimHandle
{
    private Animator anim;

    public event Action OnAttackEvent;

    // todo 애니메이션 이벤트 구현 후 가져와서 처리하기

    public void AnimUpdate()
    {
        
    }

    public void ExecuteAnimEvent()
    {
        
    }

    public void Init()
    {
        if (!TryGetComponent<Animator>(out anim))
            Debug.Log($"{gameObject.name} EnemyAnimCtrl.cs - Init() - Animator Can't Referenece");


    }

    public void SetAnim(string animName)
    {
        anim.SetTrigger(AnimHash.GetAnimHash(animName));
    }

    public void SetMoveState(bool isOn)
    {
        anim.SetBool(AnimHash.GetAnimHash("IsMove"), isOn);
    }
}

using UnityEngine;

public class RangeRobotAnimCtrl : EnemyAnimCtrl
{
    private bool isIKSet = false;
    private Transform rh_GripPoint;

    [SerializeField] private Vector3 shootOffset = new Vector3 (0,90f, 0f);

    public override void Init()
    {
        base.Init();
        SetIK();
    }

    private void SetIK()
    {
        isIKSet = false;
        rh_GripPoint = MyUtility.GetChildrenTrans(transform, "RHGripPoint");
        if (rh_GripPoint != null) isIKSet = true;
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!isIKSet) return;

        RightHandIK();
    }

    private void RightHandIK()
    {
        Quaternion rot = rh_GripPoint.rotation;
        if (isAnimEvent)
            rot *= Quaternion.Euler(shootOffset);

        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);
        anim.SetIKPosition(AvatarIKGoal.RightHand, rh_GripPoint.position);
        anim.SetIKRotation(AvatarIKGoal.RightHand, rot);
    }
}

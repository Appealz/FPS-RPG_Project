using UnityEngine;

public class ArmorManager
{
    // ��ź�� �������̽� ��������� ���⼭ ������ ����
    private float curArmor;
    private float reduction; // �ӽ�

    public ArmorManager()
    {
        curArmor = 0;
        reduction = 0;
    }

    /// <summary>
    /// ���ο� ��ź���� ������ ���
    /// </summary>
    public void SetArmor(float value, float newReduction) // todo ��ź���� ��������� �ش� ��ź���� �޴� ������ ���鿹��
    {
        curArmor = value;
        reduction = newReduction;
        // todo UI ������Ʈ
    }

    /// <summary>
    /// ��ź���� ������ ���
    /// </summary>
    public void RepairArmor()
    {
        // todo ��ź���� ��������� curArmor�� ���� ������ �ִ� ��ź���� �ִ� ��ġ���� ������ ����
        // todo UI ������Ʈ
    }

    public int GetArmor() => Mathf.RoundToInt(curArmor);

    public float ApplyDamage(float damage)
    {
        float curDamage = damage * (1 - reduction);
        float absorb = curDamage - curArmor;
        curArmor -= curDamage;
        if(curArmor <= 0)
        {
            curArmor = 0;
            // todo ��ź�� UI ������ ����
        }

        return Mathf.Max(0, absorb);
    }

}

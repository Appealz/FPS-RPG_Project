using UnityEngine;

public interface ISentryGunAI
{
    void Init(IUnitFSM newFSM);

    void AIUpdate();
}

public class SentryGunAI : MonoBehaviour, ISentryGunAI
{
    private IUnitFSM fsm;
    private ISentryGunWriteableContext context;

    public void AIUpdate()
    {
        
    }

    public void Init(IUnitFSM newFSM)
    {
        fsm = newFSM;
        if(!TryGetComponent<ISentryGunWriteableContext>(out context))
        {
            Debug.Log("SentryGunAI.cs - Init() - ISentryGunContext Can't Find");
        }
    }
}

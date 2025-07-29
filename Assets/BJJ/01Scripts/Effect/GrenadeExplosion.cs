using UnityEngine;

public class GrenadeExplosion : MonoBehaviour, IPoolLabel, IEffectObject
{
    private Pool ownerPool;
    private ParticleSystem ps;
    private ParticleSystem[] particleSystems;

    public void Create(Pool onwerPool)
    {
        this.ownerPool = onwerPool;
        particleSystems = GetComponentsInChildren<ParticleSystem>();

        ps = particleSystems[0];

        foreach (var p in particleSystems)
        {
            var main = p.main;
            main.loop = false;
            main.playOnAwake = false;
            if (p != ps)
            {
                main.stopAction = ParticleSystemStopAction.None;
            }
            else main.stopAction = ParticleSystemStopAction.Callback;
        }
        gameObject.SetActive(false);
    }

    public void EffectStart()
    {
        if(ps == null)
        {
            Debug.Log("GrenedeExplosion.cs - EffectStart() - Not Init");
            return;
        }

        ps.Play(true);
    }

    private void OnParticleSystemStopped()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        foreach (var p in particleSystems)
        {
            p.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        ownerPool.ReturnToPool(gameObject);
    }
}

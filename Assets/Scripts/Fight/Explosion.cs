using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour, IPooledObject
{
    public PooledObjectType Type;

    private ParticleSystem _particleSystem;
    private bool IsAbleToCheck;

    public void OnObjectSpawn()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        IsAbleToCheck = true;
    }

    public void OnObjectDespawn()
    {
        IsAbleToCheck = false;
    }

    public void Despawn()
    {
        ObjectPooler.Instance.Despawn(Type,this.gameObject);
    }

    private void Update()
    {
        if (IsAbleToCheck && !_particleSystem.isPlaying)
        {
            Despawn();
        }
    }

}

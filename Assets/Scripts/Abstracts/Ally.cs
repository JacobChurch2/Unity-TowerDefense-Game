using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Ally : MonoBehaviour
{
    private protected abstract float FireRate { get; set; }
    private protected abstract Transform CurrentTarget { get; set; }
    private protected abstract float damage { get; set; }

    public virtual void Fire()
    {
        Debug.Log("Fire");
        DealDamage();
    }

    public virtual void DealDamage()
    {
        Debug.Log("Deal Damage..");
    }
}

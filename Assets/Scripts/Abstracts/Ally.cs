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

    public virtual float Radius
    {
        get => _radius;
        set => _radius = value;
    }

    [SerializeField] private float _radius = 10;
    [SerializeField] private Transform _currentTarget;

    public virtual void Start()
    {
        //InvokeRepeating("UpdateTarget",0,.5f);
    }

    void Update()
    {
        UpdateTarget();
    }

    public virtual void UpdateTarget()
    {
        int layermask = 1 << LayerMask.NameToLayer("Enemy");

        Collider[] enemies = Physics.OverlapSphere(
            transform.position,
            _radius,
            layermask);


        Debug.Log(enemies + "  " + enemies.Length);
        float dist = Mathf.Infinity;

        foreach (var col in enemies)
        {
            float tempDist = CalculateDistance(col.transform);
            Debug.Log(tempDist);
            if (tempDist < dist)
            {
                dist = tempDist;
                CurrentTarget = col.transform;
            }

        }

        UpdateRotation();

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    private void UpdateRotation()
    {
        if (CurrentTarget == null) return;
        else if (CalculateDistance(CurrentTarget) > Mathf.Pow(Radius, 2))
        {
            CurrentTarget = null;
            return;
        }

        Vector3 dir = (transform.position - CurrentTarget.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(-dir);
        Vector3 rotation = lookRotation.eulerAngles;

        Quaternion targetRotation = Quaternion.Lerp(
            transform.GetChild(0).rotation,
            Quaternion.Euler(0, rotation.y, 0),
            2 * Time.deltaTime
        );

        transform.GetChild(0).rotation = targetRotation;
    }

    private float CalculateDistance(Transform target)
    {
        return (transform.position - target.position).sqrMagnitude;
    }
}

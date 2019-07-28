using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Turret : Ally
{
    private protected override Transform CurrentTarget
    {
        get => _currentTarget;

        set => _currentTarget = value;
    }
    private protected override float FireRate { get; set; }
    private protected override float damage { get; set; }

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
            _radius , 
            layermask);


        Debug.Log(enemies+ "  "+enemies.Length);
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

    public override void DealDamage()
    {
        base.DealDamage();
    }

    public override void Fire()
    {
        base.Fire();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

    private void UpdateRotation()
    {
        if (CurrentTarget == null) return;
        else if (CalculateDistance(CurrentTarget)>Mathf.Pow(Radius,2))
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

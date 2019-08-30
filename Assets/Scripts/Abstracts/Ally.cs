using UnityEngine;

[System.Serializable]
public abstract class Ally : Actor
{
    private protected abstract Transform CurrentTarget { get; set; }

    public virtual float Radius
    {
        get => _radius;
        set => _radius = value;
    }

    [SerializeField] private float _radius = 10;
    [SerializeField] private Transform _currentTarget;
    private float _rotatingSpeed = 8f;

    public  virtual void Update()
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


        float dist = Mathf.Infinity;

        foreach (var col in enemies)
        {
            float tempDist = CalculateDistance(col.transform);
            if (tempDist < dist)
            {
                dist = tempDist;
                CurrentTarget = col.transform;
            }

        }

        UpdateRotation();

    }

    private void OnDrawGizmos()
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
            _rotatingSpeed * Time.deltaTime
        );

        transform.GetChild(0).rotation = targetRotation;
    }

    private float CalculateDistance(Transform target)
    {
        return (transform.position - target.position).sqrMagnitude;
    }
}

using UnityEngine;

public class Turret : Ally
{
    private protected override Transform CurrentTarget { get; set; }
    [SerializeField] private protected Transform _firingPoint;

    [SerializeField]
    private float _damage = 1f;

    private protected virtual float _fireRate
    {
        get { return FireRate; }
        set { FireRate = value; }
    }

    public float FireRate;

    public float DefaultFireCoolDown;

    [SerializeField]
    public virtual float FireCoolDown
    {
        get { return _fireCoolDown; }
        set { _fireCoolDown = value; }
    }

    private float _fireCoolDown;

    public virtual void Start()
    {
        FireCoolDown = DefaultFireCoolDown;
    }

    public override void Update()
    {
        base.Update();
            
        if (CurrentTarget != null)
        {
            if (FireCoolDown <= 0f)
            {
                Fire();
                FireCoolDown = DefaultFireCoolDown / _fireRate;
            }
            else
            {
                FireCoolDown -= Time.deltaTime;
            }

        }
    }

    public virtual void Fire()
    {
        GameObject go = ObjectPooler.Instance.SpawnFromPool(
            PooledObjectType.SpearBullet, _firingPoint.position,
            Quaternion.identity);

        go.GetComponent<Projectile>().SetProjectile(CurrentTarget, _damage ,
            _firingPoint , Radius);
    }

}

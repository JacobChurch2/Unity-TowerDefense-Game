using UnityEngine;

public class Turret : Ally
{
    private protected override Transform CurrentTarget { get; set; }
    [SerializeField] private protected Transform _firingPoint;

    [SerializeField]
    protected float _damage = 1f;

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

    [SerializeField]
    public int Cost;

    public virtual void Start()
    {
        FireCoolDown = DefaultFireCoolDown;
    }

    public override void Update()
    {
        base.Update();

        if (CurrentTarget != null)
        {
            // Check if there are enemies within range before firing
            Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, Radius);

            if (enemiesInRange.Length > 0) // Only fire if an enemy is in range
            {
                if (FireCoolDown <= 0f)
                {
                    Fire();
                    FireCoolDown = DefaultFireCoolDown / _fireRate;  // Reset cooldown after firing
                }
                else
                {
                    FireCoolDown -= Time.deltaTime; // Reduce cooldown over time
                }
            }
        }
    }

    public virtual void Fire()
    {
        // Check if an enemy is within range
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, Radius);

        if (enemiesInRange.Length > 0) // Only fire if an enemy is in range
        {
            PooledObjectType projectileType = (this is BombTurret) ? PooledObjectType.BombBullet : PooledObjectType.SpearBullet;

            GameObject go = ObjectPooler.Instance.SpawnFromPool(
                projectileType, _firingPoint.position, Quaternion.identity);

            go.GetComponent<Projectile>().SetProjectile(CurrentTarget, _damage, _firingPoint, Radius);
        }
    }
}
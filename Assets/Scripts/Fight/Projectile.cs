using UnityEngine;

public class Projectile : MonoBehaviour, IPooledObject
{
    private Transform _target;
    private Transform _firingPoint;
    public float MoveSpeed = 2f;
    private Rigidbody _rb;
    private float _allyRadius;

    [HideInInspector] public float Damage = 1f;

    public PooledObjectType Type;
    private float distance;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        Move();
    }

    public void Hit()
    {
        if(_target == null) return;
        Health tempHealth = _target.GetComponent<Health>();
        if (tempHealth != null)
        {
            tempHealth.TakeDamage(Damage);
        }
    }

    public void Move()
    {
        if (_target.gameObject.activeSelf)
        {
            distance = (transform.position - _firingPoint.position).sqrMagnitude;
            transform.LookAt(_target);
            _rb.linearVelocity = _firingPoint.forward * MoveSpeed * 2 ;


            if (distance > _allyRadius *12)
            {
                Despawn();
            }
        }
        else
        {
            Despawn();
        }

    }

    public void OnObjectSpawn()
    {
        if (!_rb)
            _rb = GetComponent<Rigidbody>();
    }

    public void OnObjectDespawn()
    {
        // 
        distance = 0;
        _target = null;
        _firingPoint = null;
        _rb.linearVelocity = Vector3.zero;
        _allyRadius = 0;
    }

    public void Despawn()
    {
        ObjectPooler.Instance.Despawn(Type, this.gameObject);
    }

    public void SetProjectile(Transform tr, float dg, Transform firingPos, float rad)
    {
        _target = tr;
        Damage = dg;
        _firingPoint = firingPos;
        _rb.AddForce(_firingPoint.forward * 100 * MoveSpeed);
        _allyRadius = rad;
    }

    private void Explode()
    {
        float explosionRadius = 10f; // Adjust explosion radius as needed
        float explosionForce = 500f; // Adjust force if physics is applied

        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider col in hitEnemies)
        {
            Health enemyHealth = col.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(Damage);
            }

            Rigidbody enemyRb = col.GetComponent<Rigidbody>();
            if (enemyRb != null)
            {
                enemyRb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }

        // (Optional) Spawn explosion effect
        GameObject explosionEffect = ObjectPooler.Instance.SpawnFromPool(
            PooledObjectType.BlueExplosion, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        int enemyLayer = 1 << LayerMask.NameToLayer("Enemy");
        int otherLayer = 1 << other.gameObject.layer;

        if (otherLayer == enemyLayer)
        {
            if (Type == PooledObjectType.BombBullet)
            {
                Explode();
            }
            else
            {
                Hit();
            }

            Despawn();
        }
    }



}

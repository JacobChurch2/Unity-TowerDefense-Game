using UnityEngine;

public class SniperTurret : Turret
{
	public override float FireCoolDown { get; set; }

	[SerializeField]
	private float bulletSpeed;

	public override void Start()
	{
		base.Start();
	}

	public override void Update()
	{
		base.Update();
	}

	public override void Die()
	{
		base.Die();
	}

	public override void Fire()
	{
		GameObject go = ObjectPooler.Instance.SpawnFromPool(
			PooledObjectType.SpearBullet, _firingPoint.position,
			Quaternion.identity);

		go.GetComponent<Projectile>().SetProjectile(CurrentTarget, _damage,
			_firingPoint, Radius);

		go.GetComponent<Projectile>().MoveSpeed = bulletSpeed;
	}

	public override void UpdateTarget()
	{
		base.UpdateTarget();
	}
}

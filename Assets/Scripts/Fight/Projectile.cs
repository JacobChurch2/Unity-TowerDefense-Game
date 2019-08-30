using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Transactions;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Projectile : MonoBehaviour, IPooledObject
{
    private Transform _target;
    public float MoveSpeed = 2f;
    private Rigidbody _rb;

    [HideInInspector] public float Damage = 1f;

    public PooledObjectType Type;

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
        //Deal damage here
        Health tempHealth = _target.GetComponent<Health>();
        tempHealth.TakeDamage(Damage);
    }

    public void Move()
    {
        transform.position = Vector3.Lerp(transform.position + transform.forward,
            _target.position,
            MoveSpeed *Time.deltaTime);
        transform.LookAt(_target);
    }

    public void OnObjectSpawn()
    {
        if (!_rb)
            _rb = GetComponent<Rigidbody>();
    }

    public void OnObjectDespawn()
    {
        // 
    }

    public void Despawn()
    {
        ObjectPooler.Instance.Despawn(Type,this.gameObject);
    }

    public void SetProjectile(Transform tr, float dg)
    {
        _target = tr;
        Damage = dg;
    }

    private void OnTriggerEnter(Collider other)
    {

        int enemy = 1 << LayerMask.NameToLayer("Enemy");
        int temp = 1 << other.gameObject.layer;

        if (temp == enemy)
        {
            Hit();
            Despawn();
        }

    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Transactions;
using TreeEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

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
        Health tempHealth = _target.GetComponent<Health>();
        tempHealth.TakeDamage(Damage);
    }

    public void Move()
    {
        if (_target.gameObject.activeSelf)
        {
            distance = (transform.position - _firingPoint.position).sqrMagnitude;
            transform.LookAt(_target);
            _rb.velocity = _firingPoint.forward * MoveSpeed * 2 ;


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
        _rb.velocity = Vector3.zero;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Actor,IPooledObject
{
    [SerializeField]
    public virtual float speed { get;  set; }

    private Vector3 _explosionPoint;

    public abstract Transform target { get;  set; }
    public abstract int WaypointIndex { get;  set; }
    public abstract bool DoOnce { get; set; }
    public abstract PathManager manager { get; set; }

    public abstract PooledObjectType type { get; set; }
     
    public virtual void Start()
    {
        WaypointIndex = 0;
        manager = PathManager.Instance;
        target = manager._waypoints[WaypointIndex];
    }

    public virtual void Update()
    {
        target = manager._waypoints[WaypointIndex];
        Vector3 dir = target.position - transform.position;
        transform.Translate(
            dir.normalized * speed * Time.deltaTime
            , Space.World);

        float dist = (transform.position - target.position).sqrMagnitude;

        if (dist<.1f&&!DoOnce && WaypointIndex!= manager._waypoints.Count-1)
        {
            WaypointIndex++;
            DoOnce = true;
        }
        else
        {
            DoOnce=false;
        }

    }

    public virtual void Despawn()
    {
        _explosionPoint = transform.position;
        ObjectPooler.Instance.Despawn(type, this.gameObject);
    }

    public void OnObjectSpawn()
    {
        WaypointIndex = 0;
    }

    public void OnObjectDespawn()
    {
       GameObject go = ObjectPooler.Instance.SpawnFromPool(
           PooledObjectType.BlueExplosion,
           _explosionPoint, 
           Quaternion.identity);

       

    }
}

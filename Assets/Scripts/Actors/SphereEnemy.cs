using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereEnemy : Enemy
{
    public override float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }


    public override bool DoOnce { get; set; }
    public override int WaypointIndex { get; set; }
    public override Transform target { get; set; }
    public override PathManager manager { get; set; }

    public float _speed = 10f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}

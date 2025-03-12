using UnityEngine;

public class RedSphereEnemy : Enemy
{
    public override float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [SerializeField] private PooledObjectType _type;
    public override PooledObjectType type { get; set; }
    public override bool DoOnce { get; set; }
    public override int WaypointIndex { get; set; }
    public override Transform target { get; set; }
    public override PathManager manager { get; set; }

    public float _speed = 10f;

    private void Awake()
    {
        type = _type;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

}

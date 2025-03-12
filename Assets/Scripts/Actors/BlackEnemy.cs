using UnityEngine;

public class BlackEnemy : Enemy
{
    public override float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public override int prizeMoney
    {
        get { return _prizeMoney; }
        set { _prizeMoney = value; }
    }

    public override int damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    [SerializeField] private PooledObjectType _type;
    public override PooledObjectType type { get; set; }
    public override bool DoOnce { get; set; }
    public override int WaypointIndex { get; set; }
    public override Transform target { get; set; }
    public override PathManager manager { get; set; }

    public float _speed = 10f;
    public int _prizeMoney = 1;
    public int _damage = 1;

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

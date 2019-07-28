using UnityEngine;

[System.Serializable]
public struct Wave
{
    public PooledObjectType EnemyType;
    public int Amount;
    public Transform StartingPoint;
    public Transform EndingPoint;
}

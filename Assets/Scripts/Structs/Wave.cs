using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Wave
{
    public List<EnemySpawnData> EnemyData;  // List of enemy type and its quantity
    public Transform StartingPoint;
    public Transform EndingPoint;
}

[System.Serializable]
public struct EnemySpawnData
{
    public PooledObjectType EnemyType;  // The type of the enemy
    public int Amount;  // How many of this enemy to spawn
}
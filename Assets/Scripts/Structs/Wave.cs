using UnityEngine;

[System.Serializable]
public struct Wave
{
    [SerializeField] [Range(0,100)] private int _waveIndex;
    public PooledObjectType EnemyType;
    public int Amount;
}

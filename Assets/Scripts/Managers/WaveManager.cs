using System;
using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Pool Interactions")]
    public WaveStruct Wave;

    [Header("Timing")]
    public float CountDownTimer = 10f;
    public float InitialCountDown = 3f;
    public float WaveDelay = 2f;
    [SerializeField]
    private float _spawnDelay = 1f;

    [Header("Wave Details")]
    private int _waveIndex = 0;
    private float _delay;
    private float _countDownTimer;
    private bool _isWaveSpawned;

    [NonSerialized]
    public bool LastWave;

    private WaitForSeconds _enemySpawnDelay;

    public void IncreaseDifficulty()
    {
        //Lower the time between waves

        CountDownTimer = Mathf.Max(CountDownTimer * 0.9f, 2f);
        //InitialCountDown = Mathf.Max(InitialCountDown * 0.9f, 1f);
        WaveDelay = Mathf.Max(WaveDelay * 0.85f, 0.5f);
        _spawnDelay = Mathf.Max(_spawnDelay * 0.9f, 0.25f);
    }

    private void Awake()
    {
        ResetEnemySpawnDelay();
        CountDownTimer = InitialCountDown;
        ResetDelay();
    }

    private void Update()
    {
        WaveSystem();
    }

    private void WaveSystem()
    {
        if (_waveIndex != Wave.Waves.Count)
        {
            LastWave = false;
            if (_countDownTimer <= 0 && !_isWaveSpawned)
            {
                SpawnWave();
                _isWaveSpawned = true;
            }
            else
            {
                _countDownTimer -= Time.deltaTime;
            }

            if (_isWaveSpawned)
            {
                if (_delay <= 0)
                {
                    _countDownTimer = CountDownTimer;
                    ResetTimer();
                    ResetDelay();
                    _isWaveSpawned = false;
                }
                else
                {
                    _delay -= Time.deltaTime;
                }
            }
        }
        else
        {
            //NO MORE WAVES
            Debug.Log("All the waves has finished .");
            LastWave = true;
        }
    }

    private void SpawnWave()
    {
        Debug.Log("New wave spawned with the index of  " + _waveIndex);
        //wave spawned
        IncreaseDifficulty();
        StartCoroutine(DelayedSpawn());
    }

    private IEnumerator DelayedSpawn()
    {
        float stopwatch = 0f;
        ObjectPooler pooler = ObjectPooler.Instance;
        Wave currentWave = Wave.Waves[_waveIndex];

        for (int i = 0; i < currentWave.EnemyData.Count; i++)
        {
            // Access the enemy data (type and amount)
            var enemySpawnData = currentWave.EnemyData[i];

            // Spawn the specified amount of the current enemy type
            for (int j = 0; j < enemySpawnData.Amount; j++)
            {
                pooler.SpawnFromPool(enemySpawnData.EnemyType,
                    currentWave.StartingPoint.position,
                    currentWave.StartingPoint.rotation);

                stopwatch += Time.deltaTime * 15;  // Adjust this value for spawn pacing
                yield return _enemySpawnDelay;    // Wait before spawning the next enemy
            }
        }

        //WaveDelay += stopwatch * _waveIndex;

    }

    private void ResetEnemySpawnDelay()
    {
        _enemySpawnDelay = new WaitForSeconds(_spawnDelay);
    }

    private void ResetTimer()
    {
        _countDownTimer = CountDownTimer;
    }

    private void ResetDelay()
    {
        _delay = WaveDelay;
    }
}

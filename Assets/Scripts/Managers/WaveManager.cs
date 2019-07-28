using System.Collections;
using System.Collections.Generic;
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

    private WaitForSeconds _enemySpawnDelay;

    private void Awake()
    {
        ResetEnemySpawnDelay();
        CountDownTimer = InitialCountDown;
        ResetDelay();
    }

    private void Update()
    {
        if (_waveIndex != Wave.Waves.Count)
        {
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
            Debug.Log("No more waves ... ");
        }


    }

    private void SpawnWave()
    {
        Debug.Log("Wave spawned ...");
        StartCoroutine(DelayedSpawn());
    }

    private IEnumerator DelayedSpawn()
    {
        float stopwatch = 0f;
        ObjectPooler pooler = ObjectPooler.Instance;


        for (int j = 0; j < Wave.Waves[_waveIndex].Amount; j++)
        {
            pooler.SpawnFromPool(Wave.Waves[_waveIndex].EnemyType,
                Wave.Waves[_waveIndex].StartingPoint.position,
                Wave.Waves[_waveIndex].StartingPoint.rotation);

            stopwatch += Time.deltaTime * 15;
            yield return _enemySpawnDelay;
        }

        _waveIndex++;
        WaveDelay += stopwatch*_waveIndex;

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

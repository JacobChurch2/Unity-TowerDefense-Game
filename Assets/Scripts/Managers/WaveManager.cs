using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Pool Interactions")]
    public WaveStruct Wave;

    [Header("Timing")]
    public float CountDownTimer = 10f;
    public float Delay = 2f;
    [Header("Wave Details")]
    public int WaveAmount = 1;
    private int _waveIndex = 0;
    private float _delay;
    private float _countDownTimer;

    private bool _isWaveSpawned;

    private void Awake()
    {
        ResetTimer();
        ResetDelay();
    }

    private void Update()
    {
        if (_waveIndex!=WaveAmount-1)
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
        _waveIndex++;
    }

    private void ResetTimer()
    {
        _countDownTimer = CountDownTimer;
    }

    private void ResetDelay()
    {
        _delay = Delay;
    }
}

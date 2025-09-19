using MGMG.Core;
using MGMG.Enemies;
using System;
using System.Collections.Generic;
using UnityEngine;

public struct WaveInfo
{
    public int enemyCount;
}

public class WaveManager : MonoSingleton<WaveManager>
{
    public int WaveCount => _waveCount;
    private int _waveCount = 1;
    public List<Enemy> enemyList { get; set; }

    private int _currentWaveTotalEnemyCount = 0;
    public event Action OnWaveStart;

    protected override void Awake()
    {
        base.Awake();
        enemyList = new List<Enemy>();
    }

    public void SetWaveEnemyCount(int count)
    {
        _currentWaveTotalEnemyCount = count;
    }
    public void RemoveEnemyCount()
    {
        _currentWaveTotalEnemyCount--;
        if (_currentWaveTotalEnemyCount < 0) _currentWaveTotalEnemyCount = 0;
    }
    public bool IsClearWave()
        => _currentWaveTotalEnemyCount == 0;

    public bool CheckEnemyWave()
    {
        if (enemyList.Count == 0)
            return true;
        else
            return false;
    }


    public WaveInfo CreateWave()
    {
        WaveInfo wave = new WaveInfo();
        wave.enemyCount = WaveCount * 4;
        _waveCount++;
        OnWaveStart?.Invoke();
        return wave;
    }
}

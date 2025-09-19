using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MGMG.Enemies;
using MGMG.Core.ObjectPooling;

namespace MGMG.Combat
{
    public class Spawner : MonoBehaviour
    {
        private Transform[] _spawnPoints;
        private List<EnemyPoolingType> _enemyList;
        private List<int> _spawnPointIdxs;
        [SerializeField] private float _waveDelayTime = 10f;
        [SerializeField] private bool waitForClearWave = true;

        private void Awake()
        {
            _spawnPoints = GetComponentsInChildren<Transform>();
            _enemyList = new List<EnemyPoolingType>();
            _spawnPointIdxs = new List<int>();
        }

        private void Start()
        {
            StartCoroutine(WaveLoop());
        }

        private IEnumerator WaveLoop()
        {
            SetWave();
            StartCoroutine(SpawnWave());

            while (true)
            {
                yield return new WaitForSeconds(_waveDelayTime);

                if (waitForClearWave)
                {
                    while (!WaveManager.Instance.IsClearWave())
                        yield return null;
                }

                SetWave();
                StartCoroutine(SpawnWave());
            }
        }

        private void SetWave()
        {
            _enemyList.Clear();
            WaveInfo wave = WaveManager.Instance.CreateWave();
            WaveManager.Instance.SetWaveEnemyCount(wave.enemyCount);

            var enumLen = System.Enum.GetValues(typeof(EnemyPoolingType)).Length;
            for (int i = 0; i < wave.enemyCount; ++i)
            {
                EnemyPoolingType type = (EnemyPoolingType)Random.Range(0, 0);
                _enemyList.Add(type);
            }
        }

        private IEnumerator SpawnWave()
        {
            int batchCount = 10;
            int current = 0;
            for (int i = 0; i < _enemyList.Count; i++)
            {
                current++;
                if (current >= batchCount)
                {
                    current = 0;
                    float wait = Mathf.Clamp(7f - Mathf.Log(WaveManager.Instance.WaveCount, 1.3f), 0f, 10f) / 1.5f;
                    yield return new WaitForSeconds(wait);
                }

                int pointIdx = Random.Range(1, _spawnPoints.Length);
                Enemy enemy = PoolManager.Instance.Pop(_enemyList[i]) as Enemy;
                if (enemy == null) continue;

                enemy.transform.position = _spawnPoints[pointIdx].position;
                WaveManager.Instance.enemyList.Add(enemy);

                yield return new WaitForSeconds(0.05f);
            }
        }

        private IEnumerator WaitForUnscaledSeconds(float seconds)
        {
            float end = Time.unscaledTime + seconds;
            while (Time.unscaledTime < end)
                yield return null;
        }
    }
}

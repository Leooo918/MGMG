using MGMG.Core.ObjectPooling;
using MGMG.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MGMG.Magic
{
    public class SatelliteMagic : PlayerMagic
    {
        private List<Satellite> _satelliteList;
        private Dictionary<Satellite, float> _rotationDictionary;
        private float _speedUpValue = 1.0f;
        private float _rotation;

        private SatelliteMagicData _satelliteData => _magicData as SatelliteMagicData;

        public override void Initialize(Entity owner, MagicData magicData)
        {
            base.Initialize(owner, magicData);
            _satelliteList = new List<Satellite>();
            _rotationDictionary = new Dictionary<Satellite, float>();

            if (_satelliteData == null)
            {
                Debug.LogError($"Something went wrong the {magicData.name} Magic and MagicData not match");
                return;
            }

            SetSatellite();
        }

        public override void OnUpdate()
        {
            foreach (var satellite in _satelliteList)
            {
                float rotation = _rotationDictionary[satellite] + (_satelliteData.satelliteRotateSpeed * _speedUpValue * Time.deltaTime);
                _rotationDictionary[satellite] = rotation;
                satellite.SetRotation(rotation);
            }
        }

        // 일정 시간 마다 자동으로 사용됨
        public override void OnUseSkill()
        {
            _owner.StartCoroutine(UseSkillRoutine());
        }

        private IEnumerator UseSkillRoutine()
        {
            _speedUpValue = _satelliteData.satelliteSpeedUpValue;
            yield return new WaitForSeconds(_satelliteData.satelliteSpeedUpDuration);
            _speedUpValue = 1.0f;
        }

        public override void OnLevelUp()
        {
            base.OnLevelUp();
            SetSatellite();
        }


        private void SetSatelliteDamage()
        {
            int damage = Mathf.RoundToInt(_attackStat.Value * _satelliteData.satelliteDamagePerLevel[CurrentLevel]);
            _satelliteList.ForEach(satellite => satellite.SetDamage(damage));
        }
        private void SetSatellite()
        {
            int satelliteCount = _satelliteData.satelliteCountPerLevel[CurrentLevel];
            if (satelliteCount > _satelliteList.Count)
            {
                int count = satelliteCount - _satelliteList.Count;

                for (int i = 0; i < count; i++)
                {
                    Satellite satellite = PoolManager.Instance.Pop(SkillPoolingType.Satellite) as Satellite;
                    satellite.transform.SetParent(_owner.transform);
                    satellite.transform.localPosition = Vector3.zero;
                    _satelliteList.Add(satellite);
                }
            }
            else if (satelliteCount < _satelliteList.Count)
            {
                int count = _satelliteList.Count - satelliteCount;

                for (int i = 0; i < count; i++)
                {
                    _rotationDictionary.Remove(_satelliteList[^1]);
                    PoolManager.Instance.Push(_satelliteList[^1], true);
                    _satelliteList.RemoveAt(_satelliteList.Count - 1);
                }
            }

            SetSatelliteRotation(satelliteCount);
            SetSatelliteDamage();
        }

        private void SetSatelliteRotation(int satelliteCount)
        {
            if (satelliteCount != 0)
            {
                float interval = 360 / satelliteCount;
                for (int i = 0; i < satelliteCount; i++)
                {
                    _satelliteList[i].SetRotation(interval * i);
                    _satelliteList[i].SetDistance(_satelliteData.satelliteDistance);

                    if (_rotationDictionary.ContainsKey(_satelliteList[i]))
                    {

                        _rotationDictionary[_satelliteList[i]] = (interval * i);

                    }
                    else
                    {
                        _rotationDictionary.Add(_satelliteList[i], (interval * i));
                    }
                }
            }
        }

        #region Getter

        public override PlayerMagic GetInstance()
        {
            SatelliteMagic magic = new SatelliteMagic();
            return magic;
        }

        #endregion
    }

    [Serializable]
    public class SatelliteMagicData : MagicData
    {
        public int[] satelliteCountPerLevel;
        public float[] satelliteDamagePerLevel;
        public float satelliteDistance;
        public float satelliteRotateSpeed;
        public float satelliteSpeedUpValue;
        public float satelliteSpeedUpDuration;
    }
}

using MGMG.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGMG.Magic
{
    public class SatelliteMagic : PlayerMagic
    {
        private List<Satellite> _satelliteList;
        private Dictionary<Satellite, float> _rotationDictionary;
        private float _rotation;

        private SatelliteMagicData _satelliteData;

        public override void Initialize(Entity owner, MagicData magicData)
        {
            base.Initialize(owner, magicData);
            _satelliteList = new List<Satellite>();
            _rotationDictionary = new Dictionary<Satellite, float>();
            _satelliteData = (SatelliteMagicData)magicData;

            if (_satelliteData == null)
            {
                Debug.LogError($"Something went wrong the {magicData.name} Magic and MagicData not match");
                return;
            }

            SetSatellite();
        }

        public override void OnUpdate()
        {
            foreach(var satellite in _satelliteList)
            {
                satellite.SetRotation();
            }
        }

        // 일정 시간 마다 자동으로 사용됨
        public override void OnUseSkill()
        {

        }

        public override void OnLevelUp()
        {
            base.OnLevelUp();
            SetSatellite();
        }

        private void SetSatellite()
        {
            int satelliteCount = _satelliteData.satelliteCountPerLevel[CurrentLevel];
            if (satelliteCount > _satelliteList.Count)
            {
                int count = satelliteCount - _satelliteList.Count;

                for (int i = 0; i < count; i++)
                {
                    Satellite satellite = PoolManager.Instance.Pop<Satellite>("satellite");
                    satellite.transform.localPosition = Vector3.zero;
                    satellite.transform.SetParent(_owner.transform);
                    _satelliteList.Add(satellite);
                }
            }
            else if (satelliteCount < _satelliteList.Count)
            {
                int count = _satelliteList.Count - satelliteCount;

                for (int i = 0; i < count; i++)
                {
                    _rotationDictionary.Remove(_satelliteList[^1]);
                    PoolManager.Instance.Push<Satellite>("satellite", _satelliteList[^1]);
                    _satelliteList.RemoveAt(_satelliteList.Count - 1);
                }
            }

            if (satelliteCount != 0)
            {
                float interval = 360 / satelliteCount;
                for (int i = 0; i < satelliteCount; i++)
                {
                    if (_rotationDictionary.ContainsKey(_satelliteList[i]) == false) continue;

                    _satelliteList[i].SetRotation(interval * i);
                    _satelliteList[i].SetDistance(_satelliteData.satelliteDistance);
                }
            }
        }

        public override float GetCoolTime()
        {
            return _satelliteData.coolDownPerLevel[CurrentLevel];
        }

        public override PlayerMagic GetInstance()
        {
            SatelliteMagic magic = new SatelliteMagic();
            return magic;
        }
    }

    [Serializable]
    public class SatelliteMagicData : MagicData
    {
        public float satelliteDistance;
        public int[] satelliteCountPerLevel;
        public int[] satelliteDamagePerLevel;
    }
}

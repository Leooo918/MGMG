using MGMG.Core.ObjectPooling;
using System;
using UnityEngine;

namespace MGMG.Magic 
{
    public class MeteorMagic : PlayerMagic
    {
        protected MeteorMagicData _metorMagicData => _magicData as MeteorMagicData;

        public override void OnUseSkill()
        {
            for(int i = 0; i < _metorMagicData.meteorMagicCount[_currentLevel]; i++)
            {
                Meteor meteor = (Meteor)PoolManager.Instance.Pop(SkillPoolingType.Meteor);

                Vector2 meteorPosition = (Vector2)_owner.transform.position + (UnityEngine.Random.insideUnitCircle.normalized
            * UnityEngine.Random.Range(_metorMagicData.minSpawnRange, _metorMagicData.maxSpawnRange));
                meteor.Fire(meteorPosition, _metorMagicData.fallingDuration);
            }
        }

        public override PlayerMagic GetInstance()
        {
            PlayerMagic magic = new MeteorMagic();
            return magic;
        }
    }

    [Serializable]
    public class MeteorMagicData : MagicData
    {
        public float minSpawnRange = 2, maxSpawnRange = 7;
        public float fallingDuration = 1.0f;
        public int[] meteorMagicCount;
    }
}
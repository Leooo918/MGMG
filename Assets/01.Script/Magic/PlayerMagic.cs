using MGMG.Entities;
using MGMG.Entities.Component;
using MGMG.StatSystem;
using System;
using UnityEngine;

namespace MGMG.Magic
{
    public abstract class PlayerMagic
    {
        protected Entity _owner;
        protected EntityStat _stat;
        protected StatElement _attackStat;
        protected MagicData _magicData;

        protected int _currentLevel = 0;

        public int CurrentLevel => _currentLevel;

        public virtual void Initialize(Entity owner, MagicData magicData)
        {
            _owner = owner;
            _magicData = magicData;
            _stat = _owner.GetCompo<EntityStat>();
            _attackStat = _stat.StatDictionary[_magicData.attackStat];
        }

        public virtual void OnLevelUp()
        {
            if ((_magicData.maxLevel - 1) <= _currentLevel) return;
            _currentLevel++;
        }
        public virtual void OnUpdate() { }
        public virtual void OnUseSkill() { }

        public virtual float GetCoolTime() => _magicData.coolDownPerLevel[CurrentLevel];
        public abstract PlayerMagic GetInstance();
    }

    [Serializable]
    public abstract class MagicData
    {
        public string name;
        public string description;
        public Sprite icon;
        public EMagicSchool magicSchool;
        public int maxLevel;
        public StatElementSO attackStat;
        [Space]
        public int[] coolDownPerLevel;
    }
}

using MGMG.Entities;
using System;
using UnityEngine;

namespace MGMG.Magic
{
    public abstract class PlayerMagic
    {
        protected Entity _owner;
        protected MagicData _magicData;
        protected int _currentLevel = 0;

        public int CurrentLevel => _currentLevel;

        public virtual void Initialize(Entity owner, MagicData magicData)
        {
            _owner = owner;
            _magicData = magicData;
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
        [Space]
        public int maxLevel;
        public int[] coolDownPerLevel;
    }
}

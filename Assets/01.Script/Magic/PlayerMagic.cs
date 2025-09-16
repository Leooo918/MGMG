using MGMG.Entities;
using System;
using UnityEngine;

namespace MGMG.Magic
{
    public abstract class PlayerMagic
    {
        protected Entity _owner;
        protected int _currentLevel = 0;

        public int CurrentLevel => _currentLevel;

        public virtual void Initialize(Entity owner, MagicData magicData)
        {
            _owner = owner;

        }
        
        public virtual void OnLevelUp() { _currentLevel++; }
        public virtual void OnUpdate() {  }
        public virtual void OnUseSkill() {  }

        public abstract float GetCoolTime();
        public abstract PlayerMagic GetInstance();
    }

    [Serializable]
    public abstract class MagicData
    {
        public string name;
        public string description;
        public Sprite icon;
        public EMagicSchool magicSchool;
        public int[] coolDownPerLevel;
    }
}

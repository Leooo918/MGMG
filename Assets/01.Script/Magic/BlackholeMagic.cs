using MGMG.Core.ObjectPooling;
using UnityEngine;

namespace MGMG.Magic
{
    public class BlackholeMagic : PlayerMagic
    {
        protected BlackholeMagicData _blackholeMagicData => _magicData as BlackholeMagicData;

        public override void OnUseSkill()
        {
            base.OnUseSkill();
            int damage = Mathf.RoundToInt(_attackStat.Value * _blackholeMagicData.damagePerLevel[CurrentLevel]);

            Blackhole blackhole = (Blackhole)PoolManager.Instance.Pop(SkillPoolingType.Blackhole);
            blackhole.Initialize(_owner, damage, _blackholeMagicData.gravityPerLevel[CurrentLevel], _blackholeMagicData.sizePerLevel[CurrentLevel], _blackholeMagicData.lifeTimePerLevel[CurrentLevel]);
            blackhole.transform.position = _owner.transform.position;
        }

        public override PlayerMagic GetInstance()
        {
            return new BlackholeMagic();
        }
    }

    public class BlackholeMagicData : MagicData
    {
        public float[] gravityPerLevel;
        public float[] sizePerLevel;
        public float[] lifeTimePerLevel;
        public float[] damagePerLevel;
    }
}

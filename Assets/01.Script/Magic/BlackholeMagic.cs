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
            int damage = _attackStat.IntValue * _blackholeMagicData.damagePerLevel[CurrentLevel];

            Blackhole blackhole = (Blackhole)PoolManager.Instance.Pop(SkillPoolingType.Blackhole);
            blackhole.transform.position = _owner.transform.position;
            blackhole.Initialize(_blackholeMagicData.gravityPerLevel[CurrentLevel], damage, _blackholeMagicData.sizePerLevel[CurrentLevel], _blackholeMagicData.lifeTimePerLevel[CurrentLevel]);
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
        public int[] damagePerLevel;
    }
}

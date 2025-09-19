using MGMG.Core.ObjectPooling;
using UnityEngine;

namespace MGMG.Magic
{
    public class ElectricZoneMagic : PlayerMagic
    {
        protected ElectricZoneMagicData _electricZoneMagic => _magicData as ElectricZoneMagicData;

        public override void OnUseSkill()
        {
            base.OnUseSkill();
            int damage = Mathf.RoundToInt(_attackStat.Value * _electricZoneMagic.damagePerLevel[CurrentLevel]);

            ElectricZone electricZone = (ElectricZone)PoolManager.Instance.Pop(SkillPoolingType.ElectricZone);
            electricZone.transform.SetParent(_owner.transform);
            electricZone.transform.localPosition = Vector3.zero;
            electricZone.Initialize(_owner, damage, _electricZoneMagic.rangePerLevel[CurrentLevel], _electricZoneMagic.damagePerLevel[CurrentLevel]);
        }

        public override PlayerMagic GetInstance()
        {
            return new ElectricZoneMagic();
        }
    }
    public class ElectricZoneMagicData : MagicData
    {
        public float[] rangePerLevel;
        public float[] lifeTimePerLevel;
        public float[] damagePerLevel;
    }
}

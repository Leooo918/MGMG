using MGMG.Core.ObjectPooling;
using MGMG.Entities;
using UnityEngine;

namespace MGMG.Magic
{
    public class CycloneBladeMagic : PlayerMagic
    {
        protected CycloneBladeMagicData _cycloneBladeMagicData => _magicData as CycloneBladeMagicData;

        public override void OnUseSkill()
        {
            int damage = Mathf.RoundToInt(_attackStat.Value * _cycloneBladeMagicData.damagePerLevel[CurrentLevel]);
            CycloneBlade blade = (CycloneBlade)PoolManager.Instance.Pop(SkillPoolingType.CycloneBlade);
            blade.Initialize(_owner, damage, _cycloneBladeMagicData.bladeCountPerLevel[CurrentLevel]);
            blade.transform.SetParent(_owner.transform);
            blade.transform.localPosition = Vector3.zero;
        }

        public override PlayerMagic GetInstance()
        {
            return new CycloneBladeMagic();
        }
    }

    public class CycloneBladeMagicData : MagicData
    {
        public int[] bladeCountPerLevel;
        public float[] damagePerLevel;
    }
}

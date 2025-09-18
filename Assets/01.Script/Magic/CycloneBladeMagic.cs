using MGMG.Core.ObjectPooling;
using UnityEngine;

namespace MGMG.Magic
{
    public class CycloneBladeMagic : PlayerMagic
    {
        protected CycloneBladeMagicData _cycloneBladeMagicData => _magicData as CycloneBladeMagicData;
        public override void OnUseSkill()
        {
            CycloneBlade blade =  (CycloneBlade)PoolManager.Instance.Pop(SkillPoolingType.CycloneBlade);
            blade.Initialize(_cycloneBladeMagicData.bladeCountPerLevel[CurrentLevel]);
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
    }
}

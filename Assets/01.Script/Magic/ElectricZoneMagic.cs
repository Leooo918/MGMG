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
            ElectricZone electricZone = (ElectricZone)PoolManager.Instance.Pop(SkillPoolingType.ElectricZone);
            electricZone.transform.SetParent(_owner.transform);
            electricZone.transform.localPosition = Vector3.zero;
            electricZone.Initialize(_electricZoneMagic.levelPerRange[CurrentLevel], _electricZoneMagic.levelPerLifetime[CurrentLevel]);
        }

        public override PlayerMagic GetInstance()
        {
            return new ElectricZoneMagic();
        }
    }
    public class ElectricZoneMagicData : MagicData
    {
        public float[] levelPerRange;
        public float[] levelPerLifetime;
    }
}

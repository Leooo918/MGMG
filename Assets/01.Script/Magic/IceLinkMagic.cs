using MGMG.Core.ObjectPooling;
using UnityEngine;

namespace MGMG.Magic
{
    public class IceLinkMagic : PlayerMagic
    {
        protected IceLinkMagicData _iceLinkMagic => _magicData as IceLinkMagicData;

        public override void OnUseSkill()
        {
            base.OnUseSkill();
            int damage = Mathf.RoundToInt(_attackStat.Value * _iceLinkMagic.damagePerLevel[CurrentLevel]);

            IceLink iceLink = (IceLink)PoolManager.Instance.Pop(SkillPoolingType.IceLink);
            iceLink.transform.SetParent(_owner.transform);
            iceLink.transform.localPosition = Vector3.zero;
            iceLink.Initialize(_owner, damage, _iceLinkMagic.rangePerLevel[CurrentLevel], _iceLinkMagic.lifeTimePerLevel[CurrentLevel]);
        }

        public override PlayerMagic GetInstance() => new IceLinkMagic();
    }

    public class IceLinkMagicData : MagicData
    {
        public float[] rangePerLevel;
        public float[] damagePerLevel;
        public float[] lifeTimePerLevel;
    }
}


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
            IceLink iceLink = (IceLink)PoolManager.Instance.Pop(SkillPoolingType.IceLink);
            iceLink.transform.SetParent(_owner.transform);
            iceLink.transform.localPosition = Vector3.zero;
            iceLink.Initialize(_iceLinkMagic.levelPerRange[CurrentLevel], _iceLinkMagic.speedDownValue, _iceLinkMagic.levelPerLifetime[CurrentLevel]);
        }

        public override PlayerMagic GetInstance() => new IceLinkMagic();
    }

    public class IceLinkMagicData : MagicData
    {
        public float[] levelPerRange;
        public float[] levelPerLifetime;
        public float speedDownValue;
    }
}


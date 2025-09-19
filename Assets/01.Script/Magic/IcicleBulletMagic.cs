using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using UnityEngine;

namespace MGMG.Magic
{
    public class IcicleBulletMagic : PlayerMagic
    {
        private Collider2D[] _collider = new Collider2D[1];
        protected IcicleBulletMagicData _icicleBulletData => (IcicleBulletMagicData)_magicData;

        public override void OnUseSkill()
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(_icicleBulletData.whatIsEnemy);
            int count = Physics2D.OverlapCircle(_owner.transform.position, _icicleBulletData.detectRange, contactFilter, _collider);
            int damage = Mathf.RoundToInt(_attackStat.Value * _icicleBulletData.damagePerLevel[CurrentLevel]);

            if (count > 0)
            {
                IcicleBullet bullet = (IcicleBullet)PoolManager.Instance.Pop(SkillPoolingType.IcicleBullet);
                bullet.transform.position = _owner.transform.position;
                bullet.transform.up = (_collider[0].transform.position - _owner.transform.position).normalized;
                bullet.Initialize(_owner, _icicleBulletData.bulletSpeed, damage, _icicleBulletData.explosionRange);
            }
        }

        public override PlayerMagic GetInstance()
        {
            return new IcicleBulletMagic();
        }
    }

    public class IcicleBulletMagicData : MagicData
    {
        public float[] damagePerLevel;
        public LayerMask whatIsEnemy;
        public float detectRange;
        public float explosionRange;
        public float bulletSpeed;
    }
}

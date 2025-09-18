using MGMG.Enemies;
using MGMG.Entities.Component;
using UnityEngine;

namespace MGMG.Magic
{
    public class PulseMagic : PlayerMagic
    {
        private Collider2D[] _collider = new Collider2D[10];

        protected PulseMagicData _pulseMagicData => _magicData as PulseMagicData;

        public override void OnUseSkill()
        {
            //이펙트 재생
            //PoolManager.Instance.Pop(EffectPoolingType.Pulse);

            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(_pulseMagicData._whatIsEnemy);

            int count = Physics2D.OverlapCircle(_owner.transform.position, _pulseMagicData._rangePerLevel[CurrentLevel], contactFilter, _collider);
            for (int i = 0; i < count; i++)
            {
                if (_collider[i].TryGetComponent(out Enemy enemy))
                {
                    //넉백, 데미지 주기
                    Vector2 force = (enemy.transform.position - _owner.transform.position).normalized * _pulseMagicData._knockBackPowerPerLevel[CurrentLevel];
                    int damage = Mathf.RoundToInt(_attackStat.IntValue * _pulseMagicData._damagePerLevel[CurrentLevel]);

                    enemy.GetCompo<EntityMover>().AddForceToEntity(force);
                    enemy.GetCompo<EntityHealth>().ApplyDamage(_stat, damage);
                }
            }
        }

        public override PlayerMagic GetInstance()
        {
            return new PulseMagic();
        }
    }

    public class PulseMagicData : MagicData
    {
        public LayerMask _whatIsEnemy;
        public float[] _damagePerLevel;
        public float[] _rangePerLevel;
        public float[] _knockBackPowerPerLevel;
    }
}

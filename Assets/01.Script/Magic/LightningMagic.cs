using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using MGMG.Entities.Component;
using System.Collections;
using UnityEngine;

namespace MGMG.Magic
{
    public class LightningMagic : PlayerMagic
    {
        private float _delay = 0.1f;
        private Collider2D[] _collider = new Collider2D[5];
        protected LightningMagicData _lightningMagicData => _magicData as LightningMagicData;

        public override void OnUseSkill()
        {
            base.OnUseSkill();

            //좀 시간차 둬서 발사하기?
            _owner.StartCoroutine(Fire());
        }

        private IEnumerator Fire()
        {
            for (int i = 0; i < _lightningMagicData.damagePerLevel[CurrentLevel]; i++)
            {
                ContactFilter2D filter = new ContactFilter2D();
                filter.useLayerMask = true;
                filter.useTriggers = true;
                filter.SetLayerMask(_lightningMagicData.whatIsEnemy);

                int count = Physics2D.OverlapCircle(_owner.transform.position, _lightningMagicData.range, filter, _collider);
                count = Mathf.Min(count, _lightningMagicData.magicCountPerLevel[CurrentLevel]);

                for(int j = 0; j < count; j++)
                {
                    //이펙트 재생
                    if(_collider[j].TryGetComponent(out Enemy enemy))
                    {
                        int damage = Mathf.RoundToInt(_attackStat.Value * _lightningMagicData.damagePerLevel[CurrentLevel]);
                        enemy.GetCompo<EntityHealth>().ApplyDamage(_owner.GetCompo<EntityStat>(), damage);
                    }

                    yield return new WaitForSeconds(_delay);
                }
            }
        }

        public override PlayerMagic GetInstance()
        {
            LightningMagic lightningMagic = new LightningMagic();
            return lightningMagic;
        }
    }

    public class LightningMagicData : MagicData
    {
        public float range;
        public LayerMask whatIsEnemy;
        public int[] magicCountPerLevel;
        public float[] damagePerLevel;
    }
}

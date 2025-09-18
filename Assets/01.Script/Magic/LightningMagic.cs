using MGMG.Core.ObjectPooling;
using System.Collections;
using UnityEngine;

namespace MGMG.Magic
{
    public class LightningMagic : PlayerMagic
    {
        private float _delay = 0.1f;
        private Collider2D[] _collider = new Collider2D[2];
        protected LightningMagicData _lightningMagicData => _magicData as LightningMagicData;

        public override void OnUseSkill()
        {
            base.OnUseSkill();

            //좀 시간차 둬서 발사하기?
            _owner.StartCoroutine(Fire());
        }

        private IEnumerator Fire()
        {
            for (int i = 0; i < _lightningMagicData.levelPerMagicCount[CurrentLevel]; i++)
            {
                Lightning lightning = (Lightning)PoolManager.Instance.Pop(SkillPoolingType.Lightning);
                ContactFilter2D filter = new ContactFilter2D();
                filter.SetLayerMask(_lightningMagicData.whatIsEnemy);

                int count = Physics2D.OverlapCircle(_owner.transform.position, _lightningMagicData.range, filter, _collider);
                if (count > 0)
                {
                    if (_collider[0] != null)
                        lightning.Fire(_collider[0].transform.position);

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
        public int[] levelPerMagicCount;
    }
}

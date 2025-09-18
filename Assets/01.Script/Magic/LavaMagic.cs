using MGMG.Core.ObjectPooling;
using MGMG.Enemies;
using TMPro.EditorUtilities;
using UnityEngine;

namespace MGMG.Magic
{
    public class LavaMagic : PlayerMagic
    {
        protected LavaMagicData _lavaMagicData => _magicData as LavaMagicData;

        public override void OnUseSkill()
        {
            for (int i = 0; i < _lavaMagicData.countPerLevel[CurrentLevel]; i++)
            {
                Vector2 lavaPos = (Vector2)_owner.transform.position + (Random.insideUnitCircle.normalized
                * Random.Range(_lavaMagicData.minSpawnRange, _lavaMagicData.maxSpawnRange));

                //ÀÌÆåÆ® Àç»ý
                Lava lava = (Lava)PoolManager.Instance.Pop(SkillPoolingType.Lava);
                lava.transform.position = lavaPos;
                lava.Initialize(_lavaMagicData.rangePerLevel[CurrentLevel], _lavaMagicData.lifeTimePerLevel[CurrentLevel], _lavaMagicData.damagePerLevel[CurrentLevel]);
            }
        }

        public override PlayerMagic GetInstance()
        {
            return new LavaMagic();
        }
    }

    public class LavaMagicData : MagicData
    {
        public int[] countPerLevel;
        public float[] rangePerLevel;
        public float[] lifeTimePerLevel;
        public float[] damagePerLevel;
        public float minSpawnRange, maxSpawnRange;
    }
}

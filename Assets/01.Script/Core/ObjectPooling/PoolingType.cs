using UnityEngine;
namespace MGMG.Core.ObjectPooling
{
    public enum ObjectPoolingType
    {
        RangedEnemyProjectile,
        AreaDepolyment
    }
    public enum SkillPoolingType
    {
        Satellite = 0,
        Meteor = 1,
        Lightning = 2,
        IceLink = 3,
        Blackhole = 4,
        CycloneBlade = 5,
        ElectricZone = 6,
        Lava = 7,
        IcicleBullet = 8,
    }
    public enum EnemyPoolingType
    {
        CombatEnemy, RangedEnemy
    }
    public enum UIPoolingType
    {
    }
    public enum EffectPoolingType
    {
        LightingEffect,
        Pulse,
    }
}

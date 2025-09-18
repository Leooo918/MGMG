using MGMG.Entities;
using MGMG.Entities.Component;
using UnityEngine;

public class CrashDamageCaster : MonoBehaviour
{
    [SerializeField] private int _damage;
    private Entity _owner;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void SetOwner(Entity owner) => _owner = owner;
    public void SetDamage(int damage) => _damage = damage;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Entity entity))
        {
            if (entity != _owner)
            {
                entity.GetCompo<EntityHealth>().ApplyDamage(_owner ? _owner.GetCompo<EntityStat>() : null, _damage);
            }
        }
    }
}

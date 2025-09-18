using MGMG.Entities;
using UnityEngine;

public class MagicBook : Card
{
    [SerializeField] private float _coolDown = 30f;
    private PlayerMagicController _magicController;

    public override void Initialize(Entity owner, int index)
    {
        base.Initialize(owner, index);
        _magicController = owner.GetCompo<PlayerMagicController>();
        _magicController.SetCoolDown("MagicBook", _coolDown);
    }

    public override void Release()
    {
        _magicController.RemoveCoolDown("MagicBook");
    }

    public override Card GetInstance()
    {
        MagicBook book = new MagicBook();
        book._coolDown = _coolDown;
        return book;
    }
}

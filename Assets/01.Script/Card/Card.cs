using MGMG.Entities;

public abstract class Card
{
    protected Entity _owner;
    protected int _index;

    public virtual void OnUpdate() { }
    public virtual void Initialize(Entity owner, int index)
    {
        _owner = owner;
        _index = index;
    }
    public abstract void Release();

    public abstract Card GetInstance();
}

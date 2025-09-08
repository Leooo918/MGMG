using UnityEngine;

public interface IEntityComponent 
{
    public void Initialize(Entity entity);
    public void AfterInitialize(Entity entity);

    public void OnDispose();
}

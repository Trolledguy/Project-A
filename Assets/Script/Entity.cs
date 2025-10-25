using UnityEngine;

public abstract class Entity : Identity
{
    public virtual void Move(float MoveZ, float MoveX) { }

    public virtual void MonsterMove() { }
    public abstract void Interact();

}

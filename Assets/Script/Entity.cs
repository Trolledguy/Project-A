using UnityEngine;


public abstract class Entity : Identity
{

    public virtual void Move() { }
    public virtual void Move(float MoveZ, float MoveX) { }


}

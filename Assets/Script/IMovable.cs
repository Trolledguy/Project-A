using UnityEngine;

public interface IMovable
{
    public virtual void Move() { }
    public virtual void Move(float MoveZ, float MoveX) { }
}

using UnityEngine;

public abstract class Entity : Identity
{

    public virtual void Interact() { }
    public virtual void Interact(Player _player) { }
    public abstract void Interact(Item _item);
    public abstract void Interact(HideSpot _hidespot);

}

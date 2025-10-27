using UnityEngine;

public abstract class Entity : Identity
{

    public abstract void Interact();
    public abstract void Interact(Item _item);
    public abstract void Interact(HideSpot _hidespot);

}

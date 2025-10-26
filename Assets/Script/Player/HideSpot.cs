using UnityEngine;


[RequireComponent(typeof(Collider))]
public abstract class HideSpot : Identity , IInteractable
{
    public Collider checkBox;
    
    public abstract void ObjectInteract();
}

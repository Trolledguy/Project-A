using UnityEngine;

public interface IInteractable
{
    public SpriteRenderer sprite { get; set; }

    public abstract void ControlSprite();
}

using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Monster : Entity, IInteractable
{
    public SpriteRenderer sprite { get; set; }
    public Animator anim;


    //Call this Method on updated
    public virtual void ControlSprite()
    {
        transform.LookAt(player.cam.transform);
    }
    protected abstract void OnPlayerHit();
    protected override void Initialized()
    {
        base.Initialized();
        anim.GetComponent<Animator>();
    }
}

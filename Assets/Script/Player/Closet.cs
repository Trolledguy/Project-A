using UnityEngine;

public class Closet : HideSpot
{
    public Animator anim;
    public override void ObjectInteract(Player player)
    {
        base.ObjectInteract(player);
        anim.SetTrigger("PlayerInteract");
        anim.ResetTrigger("PlayerInteract");
    }
    public override void UnHidePlayer()
    {
        base.UnHidePlayer();
        anim.SetTrigger("PlayerInteract");
        anim.ResetTrigger("PlayerInteract");
    }
}

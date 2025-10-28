using UnityEngine;

public abstract class HideSpot : Identity , IInteractable
{
    public Player hiddingPlayer;
    public Transform hidePosotion;
    public Transform dropPostitio;
    public virtual void ObjectInteract(Player player)
    {
        if (hiddingPlayer != null || player.IsPlayerTranslating())
        {
            Debug.Log("Oject Ineract Fail");
            return; 
        }

        Vector3 hidepos = hidePosotion.position;

        StartCoroutine(player.TranslatePlayerPos(hidepos,3f));
        player.transform.SetParent(hidePosotion);
        player.TriggerFreeze();

        hiddingPlayer = player;
    }

    public virtual void UnHidePlayer()
    {
        if (hiddingPlayer.IsPlayerTranslating())
        {
            Debug.Log("unhide Fail");
            return; 
        }
        StartCoroutine(hiddingPlayer.TranslatePlayerPos(dropPostitio.position, 3f));
        hiddingPlayer.transform.SetParent(null);
        hiddingPlayer.TriggerFreeze();

        hiddingPlayer = null;
    }
}

using UnityEngine;


public class QuestItem : Item
{
    public override void ObjectInteract(Player player)
    {
        ObjectiveManager.intance.CompleteObjective(this.itemID);
        Debug.Log("Quest Object Interact");
    }

} 
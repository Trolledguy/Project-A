using UnityEngine;

public class Doll : QuestItem
{
    public Quest huntQuest;
    public override void ObjectInteract(Player player)
    {
        base.ObjectInteract(player);
        if (ObjectiveManager.intance.activeQuest == huntQuest)
        {
            foreach (var mon in GameManager.intance.allmons)
            {
                Transform spawnPoint = GameManager.intance.GetDestination();
                Monster newMon = Instantiate(mon,spawnPoint);
            }
        }
    }
}